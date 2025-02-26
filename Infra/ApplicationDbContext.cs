using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<ApplicationPassword> ApplicationPasswords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationPassword>()
                .HasKey(ap => new { ap.ApplicationId, ap.PasswordId });

            modelBuilder.Entity<ApplicationPassword>()
                .HasOne(ap => ap.Application)
                .WithMany(a => a.ApplicationPasswords)
                .HasForeignKey(ap => ap.ApplicationId);

            modelBuilder.Entity<ApplicationPassword>()
                .HasOne(ap => ap.Password)
                .WithMany(p => p.ApplicationPasswords)
                .HasForeignKey(ap => ap.PasswordId);
        }
    }
}
