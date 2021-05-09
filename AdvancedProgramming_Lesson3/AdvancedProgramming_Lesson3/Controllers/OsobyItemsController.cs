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
    public class OsobyItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public OsobyItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OsobyItemDTO>>> GetTodoItems()
        {
            return await _context.OsobyItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OsobyItemDTO>> GetTodoItem(long id)
        {
            var osobyItem = await _context.OsobyItems.FindAsync(id);
            if (osobyItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(osobyItem);
        }

        [HttpPost]
        [Route("UpdateTodoItem")]
        public async Task<object> UpdateTodoItem(OsobyItemDTO osobyItemDTO)
        {
            var osobyItem = await _context.OsobyItems.FindAsync(osobyItemDTO.Id);
            if (osobyItem == null)
            {
                return NotFound();
            }
            osobyItem.Name = osobyItemDTO.Name;
            osobyItem.Lastname = osobyItemDTO.Lastname;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!OsobyItemExists(osobyItemDTO.Id))
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(UpdateTodoItem),
                new { id = osobyItem.Id },
                ItemToDTO(osobyItem));
        }

        [HttpPost]
        [Route("CreateTodoItem")]
        public async Task<ActionResult<OsobyItemDTO>> CreateTodoItem(OsobyItemDTO osobyItemDTO)
        {
            var osobyItem = new OsobyItem
            {
                Name = osobyItemDTO.Name,
                Lastname = osobyItemDTO.Lastname
            };

            _context.OsobyItems.Add(osobyItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = osobyItem.Id },
                ItemToDTO(osobyItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OsobyItem>> DeleteTodoItem(long id)
        {
            var osobyItem = await _context.OsobyItems.FindAsync(id);
            if (osobyItem == null)
            {
                return NotFound();
            }
            _context.OsobyItems.Remove(osobyItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool OsobyItemExists(long id) =>
            _context.OsobyItems.Any(e => e.Id == id);

        private static OsobyItemDTO ItemToDTO(OsobyItem osobyItem) =>
            new OsobyItemDTO
            {
                Id = osobyItem.Id,
                Name = osobyItem.Name,
                Lastname = osobyItem.Lastname,
            };
    }
}
