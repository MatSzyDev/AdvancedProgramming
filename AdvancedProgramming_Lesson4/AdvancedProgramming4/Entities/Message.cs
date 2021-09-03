using System;
using Microsoft.AspNetCore.Identity;

namespace AdvancedProgramming4.Entities
{
    public class Message
    {
        public long Id { get; set; } = default!;
        
        public string UserId { get; set; } = default!;

        public IdentityUser User { get; set; } = default!;
        
        public string Content { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}