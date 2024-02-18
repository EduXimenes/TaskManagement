using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Entities;


namespace TaskManagement.Infrastructure.Persistence
{
    public class TaskDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=sqlserver;Initial Catalog=TaskManagementDb;User ID=SA;Password=tasks#2024;TrustServerCertificate=True; Persist Security Info=True");
        }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        public TaskDbContext()
        {
            
        }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskFollowUp> FollowUp { get; set; }
        public DbSet<TaskComment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

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
            });
            builder.Entity<Project>()
                    .HasMany(a => a.Tasks)        
                    .WithOne()        
                    .HasForeignKey(b => b.Id); 

            builder.Entity<TaskEntity>()
                    .HasMany(a => a.Comments)
                    .WithOne()
                    .HasForeignKey(b => b.idTask);

            builder.Entity<TaskComment>()
                   .HasKey(d => d.idComment);

            builder.Entity<TaskFollowUp>()
                   .HasKey(d => d.idFollowUp);

            builder.Entity<User>()
                    .HasKey(d => d.idUser);
        }
        
    }
}
