using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        public TaskDbContext()
        {
            
        }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskFollowUp> FollowUp { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>(e =>
            {
                e.HasKey(de => de.Id);
                e.Property(de => de.Title)
                    .IsRequired(true);
                e.Property(de => de.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

            });
            builder.Entity<TaskEntity>(e =>
            {
                e.HasKey(de => de.IdTask);
                e.Property(de => de.Title)
                    .IsRequired(true);
                e.Property(de => de.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");
                e.Property(de => de.Priority)
                    .IsRequired(true);
                e.Property(de => de.Status)
                    .IsRequired(true);
                e.Property(de => de.ExpirationDate)
                    .IsRequired(true);
                e.Property(de => de.Comments)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)")
                    .IsRequired(false);
            });
            builder.Entity<Project>()
                    .HasMany(a => a.Tasks)        
                    .WithOne()        
                    .HasForeignKey(b => b.Id); 

            builder.Entity<TaskEntity>()
                    .HasMany(a => a.TaskFollowUp)
                    .WithOne()
                    .HasForeignKey(b => b.IdTask);
                    
        }
        
    }
}
