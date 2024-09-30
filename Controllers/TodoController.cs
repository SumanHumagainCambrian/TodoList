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


    }
}
