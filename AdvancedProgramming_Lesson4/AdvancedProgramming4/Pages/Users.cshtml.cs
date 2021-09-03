using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedProgramming4.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming4.Pages
{
    [Authorize]
    public class Users : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public bool IsAdmin { get; set; } = false;

        public List<IdentityUser> UsersList { get; set; } = new List<IdentityUser>();

        public Users(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task OnGetAsync()
        {
            IsAdmin = Utils.IsAdmin(HttpContext);

            if (IsAdmin)
            {
                UsersList = await _dbContext.Users.ToListAsync();
            }
        }
    }
}