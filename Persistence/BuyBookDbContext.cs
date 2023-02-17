using System;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class BuyBookDbContext : IdentityDbContext<IdentityUser>, IBuyBookDbContext
    {
        public BuyBookDbContext(DbContextOptions<BuyBookDbContext> options)
          : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "";
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "";
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            var result = base.SaveChanges();

            return result;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}