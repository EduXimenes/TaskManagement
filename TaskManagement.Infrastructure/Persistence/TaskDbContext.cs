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
                ////Declarando que existem muitas Task para um Projeto (List<Tasks>) e que sua chave é a Id
                //e.HasMany(de => de.Tasks)
                //    .WithOne()
                //    .HasForeignKey(de => de.Id);
                //e.HasMany(p => p.Tasks)
                //    .WithOne(t => t.)  // Especifica a propriedade de navegação em TaskEntity que representa o relacionamento
                //    .HasForeignKey(t => t.ProjectId)  // Especifica a chave estrangeira em TaskEntity
                //    .IsRequired();  // Indica que a relação é obrigatória (opcional, se for o caso)

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

        }
        
    }
}
