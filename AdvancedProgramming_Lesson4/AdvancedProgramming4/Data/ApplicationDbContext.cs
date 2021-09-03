using System;
using System.Collections.Generic;
using System.Text;
using AdvancedProgramming4.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AdvancedProgramming4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Message> Messages { get; set; } = default;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Message>()
                .ToTable("Messages");
        }
    }
}