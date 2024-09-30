using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }

        // Get all ToDoItems without CompletedDate
        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetIncompleteList()
        {
            var incompleteItem = await _context.ToDoItems.Where(x => x.CompletedDate == null).ToListAsync();
            return incompleteItem;
        }

        // Get by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItemById(int id)
        {
            var items = await _context.ToDoItems.FindAsync(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }



        // Create new ToDoItem
        [HttpPost("create")]
        public async Task<ActionResult<ToDoItem>> CreateNewItem(ToDoItem ToDoItem)
        {
            _context.ToDoItems.Add(ToDoItem);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Update item
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, ToDoItem ToDoItem)
        {
            if (id != ToDoItem.Id)
            {
                return BadRequest();
            }

            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            toDoItem.CompletedDate = DateTime.Now;//completed date as current date time

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Delete ToDoItem by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var deleteItem = await _context.ToDoItems.FindAsync(id);
            if (deleteItem == null)
            {
                return NotFound();
            }
            _context.ToDoItems.Remove(deleteItem);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
