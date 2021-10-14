using Inventory.Models;
using Inventory.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Inventory.Infrastructure
{
    public class InventoryContext : DbContext
    {
        public DbSet<ItemDTO> Items { get; set; }
        public DbSet<AccessDTO> Access { get; set; }

        public InventoryContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Inventory.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seeding of data
            modelBuilder.Entity<AccessDTO>().HasData(
                new AccessDTO { id = Guid.NewGuid(), accessId = "access-admin", secretKey = "d9d44bb0-7fa3-4549-94f5-550649b95d3f", role = Roles.Admin, updatedOn = DateTime.Now, createdOn = DateTime.Now },
                new AccessDTO { id = Guid.NewGuid(), accessId = "access-user", secretKey = "912aba6b-7725-4161-a20b-c1f337db1a19", role = Roles.User, updatedOn = DateTime.Now, createdOn = DateTime.Now }
            );
        }
    }
}

