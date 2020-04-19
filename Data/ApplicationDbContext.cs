using Data.Builders;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<ToDo> ToDo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new UserBuilder(builder.Entity<User>());
            new ToDoBuilder(builder.Entity<ToDo>());

            builder.Entity<User>().HasData(new Entity.User
            {
                id = 1,
                CreatedAt = DateTime.UtcNow,
                Email = "aykutonen@gmail.com",
                FirstName = "Aykut",
                LastName = "Önen",
                Password = "123",
                Status = 1,
                UpdatedAt = DateTime.UtcNow
            });
        }
    }
}
