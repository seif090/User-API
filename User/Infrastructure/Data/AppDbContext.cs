using Microsoft.EntityFrameworkCore;
using User.Core.Entities;

namespace User.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User.Core.Entities.User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Role Entity
            modelBuilder.Entity<Role>(entity =>
            {
                // Primary Key
                entity.HasKey(r => r.Id);

                // Properties
                entity.Property(r => r.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                // Index for Role Name (Unique)
                entity.HasIndex(r => r.Name)
                      .IsUnique();

                // Seed Data
                entity.HasData(
                    new Role { Id = 1, Name = "Admin" },
                    new Role { Id = 2, Name = "User" }
                );
            });
            modelBuilder.Entity<User.Core.Entities.User>(entity =>
            {
                // Primary Key
                entity.HasKey(u => u.Id);

                // Properties
                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.CreatedAt)
                      .IsRequired();

                entity.Property(u => u.UpdatedAt)
                      .IsRequired();

                // Foreign Key Relationship with Role
                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(u => u.Email)
                      .IsUnique();
            });
        }
    }
}
