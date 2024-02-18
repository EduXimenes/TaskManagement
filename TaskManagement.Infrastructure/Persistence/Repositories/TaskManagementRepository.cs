using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class TaskManagementRepository : ITaskManagementRepository
    {
        public readonly TaskDbContext _context;
        public readonly IMapper _mapper;
        public TaskManagementRepository(TaskDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Project>> GetAllProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => !p.isDeleted)
                .ToListAsync();

            foreach (var project in projects)
            {
                project.Tasks = project.Tasks.Where(t => !t.isDeleted).ToList();
            }

            return projects;
        }
        public async Task<Project?> GetProject(Guid id)
        {
            var project = await _context.Projects
                    .Include(d => d.Tasks)
                    .SingleOrDefaultAsync(d => d.Id == id);
            if (project != null)
                project.Tasks = project.Tasks.Where(t => !t.isDeleted).ToList();

            return project;
        }
        public async Task<List<TaskFollowUp>> GetAllFollowUp()
        {
            var followup = await _context.FollowUp
                .ToListAsync();
            return followup;
        }
        public async Task<TaskFollowUp?> GetFollowUp(Guid id)
        {
            var followup = await _context.FollowUp
                    .SingleOrDefaultAsync(d => d.Id == id);

            return followup;
        }
        public async Task<TaskEntity?> GetTask(Guid id)
        {
            var task = await _context.Tasks
                    .Include(d => d.Comments)
                    .SingleOrDefaultAsync(d => d.IdTask == id);
            return task;
        }
        public async Task<List<TaskComment>> GetComments(Guid idTask)
        {
            var comments = await _context.Comments
                .Where(d => d.idTask == idTask)
                .ToListAsync();
            
            return comments;
        }
        public async Task<Project> AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
        public async Task<TaskEntity> AddTask(Guid id, TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
         public async Task<TaskEntity> AddComment(Guid idTask, TaskComment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            var task = await GetTask(idTask);
            if (task == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }
            return task;
        }
        public async Task<TaskEntity> AddFollowUp(Guid idTask, TaskEntity task, Guid userId)
        {
            var taskUpdate = await _context.Tasks
                    .SingleOrDefaultAsync(d => d.IdTask == idTask);
            if (taskUpdate == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }

            var followUp = new TaskFollowUp(
            taskUpdate.IdTask,
            taskUpdate.Title,
            taskUpdate.Description,
            taskUpdate.ExpirationDate,
            taskUpdate.Status,
            taskUpdate.Priority,
            taskUpdate.Comments.Any() ? taskUpdate.Comments.Last().Comment ?? "" : "",
            taskUpdate.isDeleted,
            userId);

            _context.FollowUp.Add(followUp);
            await _context.SaveChangesAsync();

            return task;
        }
        public async Task UpdateTask(Guid id, TaskEntity task)
        {
            var taskUpdate = await GetTask(id);
            if (taskUpdate == null)
                throw new Exception("Identificador de tarefa invalido.");
                
            taskUpdate.Title = task.Title ?? taskUpdate.Title;
            taskUpdate.Description = task.Description ?? taskUpdate.Description;
            taskUpdate.ExpirationDate = task.ExpirationDate != default ? task.ExpirationDate : taskUpdate.ExpirationDate;
            taskUpdate.Status = task.Status != default ? task.Status : taskUpdate.Status;
            _context.Tasks.Update(taskUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTask(Guid idTask)
        {
            var task = await _context.Tasks
                            .SingleOrDefaultAsync(d => d.IdTask == idTask);
            if (task == null)
            {
                throw new Exception("Identificador de tarefa invalido.");
            }
            task.isDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProject(Guid idProject)
        {
            var project = await _context.Projects
                            .SingleOrDefaultAsync(d => d.Id == idProject);
            if (project == null)
            {
                throw new Exception("Identificador do projeto invalido.");
            }
            project.isDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
