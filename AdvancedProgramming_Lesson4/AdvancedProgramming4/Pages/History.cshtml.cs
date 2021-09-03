using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdvancedProgramming4.Data;
using AdvancedProgramming4.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming4.Pages
{
    [Authorize]
    public class History : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public List<Message> MessagesHistory { get; set; } = new List<Message>();
        
        public History(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task OnGetAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId is not null)
            {
                MessagesHistory = await _dbContext.Messages.Where(q => q.UserId == userId).OrderByDescending(q => q.CreatedAt).ToListAsync();
            }
        }
    }
}