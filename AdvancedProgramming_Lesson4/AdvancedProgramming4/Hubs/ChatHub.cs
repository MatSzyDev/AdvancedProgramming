using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdvancedProgramming4.Data;
using AdvancedProgramming4.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace AdvancedProgramming4.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ApplicationDbContext dbContext, ILogger<ChatHub> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task SendMessage(string message)
        {
            var userId = Context.User?.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.Users.FindAsync(userId);

            if (user is not null)
            {
                var newMessage = new Message()
                {
                    User = user,
                    Content = message
                };

                _dbContext.Messages.Add(newMessage);
                await _dbContext.SaveChangesAsync();

                // save
                await Clients.All.SendAsync("ReceiveMessage", user.Email, message, newMessage.CreatedAt);
            }
        }
    }
}