using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Password> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relation One-to-Many: Application -> Passwords
            modelBuilder.Entity<Password>()
                .HasOne<Application>() // Spécifie la relation avec Application
                .WithMany(a => a.Passwords)
                .HasForeignKey(p => p.ApplicationId) // Utilise ApplicationId comme clé étrangère
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
