using AdvancedProgramming_Lesson3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedProgramming_Lesson3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public BooksItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksItemDTO>>> GetTodoItems()
        {
            return await _context.BooksItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksItemDTO>> GetTodoItem(long id)
        {
            var booksItem = await _context.BooksItems.FindAsync(id);
            if (booksItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(booksItem);
        }

        [HttpPost]
        [Route("UpdateTodoItem")]
        public async Task<object> UpdateTodoItem(BooksItemDTO booksItemDTO)
        {
            var booksItem = await _context.BooksItems.FindAsync(booksItemDTO.Id);
            if (booksItem == null)
            {
                return NotFound();
            }
            booksItem.Name = booksItemDTO.Name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BooksItemExists(booksItemDTO.Id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(UpdateTodoItem),
                new { id = booksItem.Id },
                ItemToDTO(booksItem));
        }

        [HttpPost]
        [Route("CreateTodoItem")]
        public async Task<ActionResult<BooksItemDTO>> CreateTodoItem(BooksItemDTO booksItemDTO)
        {
            var booksItem = new BooksItem
            {
                Name = booksItemDTO.Name
            };

            _context.BooksItems.Add(booksItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = booksItem.Id },
                ItemToDTO(booksItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BooksItem>> DeleteTodoItem(long id)
        {
            var booksItem = await _context.BooksItems.FindAsync(id);
            if (booksItem == null)
            {
                return NotFound();
            }
            _context.BooksItems.Remove(booksItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool BooksItemExists(long id) =>
            _context.BooksItems.Any(e => e.Id == id);

        private static BooksItemDTO ItemToDTO(BooksItem booksItem) =>
            new BooksItemDTO
            {
                Id = booksItem.Id,
                Name = booksItem.Name,
            };
    }
}
