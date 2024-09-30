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

    }
}
