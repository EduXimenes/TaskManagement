using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                .Include(d => d.Tasks)
                .Where(d => !d.isDeleted)
                .ToListAsync();

            return projects;
        }
        public async Task<Project?> GetProject(Guid id)
        {
            var project = await _context.Projects
                    .Include(d => d.Tasks)
                    .SingleOrDefaultAsync(d => d.Id == id);

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
            var project = _context.Projects
                            .SingleOrDefault(d => d.Id == id);
            if (project == null)
            {
                throw new Exception("Identificador do projeto inválido.");
            }
            var taskList = project.Tasks.Count() >= 20;
            if (taskList)
            {
                throw new Exception("O numero máximo de tarefas já foram atribuídas, conclua ou encerre alguma tarefa antes adicionar novas.");
            }
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;

        }
         public async Task<TaskEntity> AddComment(Guid idTask, TaskComment comment)
        {
            var task = await GetTask(idTask);
            if (task == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
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
            var comments = await GetComments(idTask);
            taskUpdate.Comments = comments;

            var followUp = new TaskFollowUp(
            taskUpdate.IdTask,
            taskUpdate.Title,
            taskUpdate.Description,
            taskUpdate.ExpirationDate,
            taskUpdate.Status,
            taskUpdate.Priority,
            taskUpdate.Comments.Any() ? taskUpdate.Comments.Last().Description ?? "" : "",
            taskUpdate.isDeleted,
            userId);

            _context.FollowUp.Add(followUp);
            await _context.SaveChangesAsync();

            return task;
        }
        public async Task UpdateTask(Guid id, TaskEntity task)
        {
            var taskUpdate = await _context.Tasks
                            .SingleOrDefaultAsync(d => d.IdTask == id);
            if (taskUpdate == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }
            taskUpdate.UpdateInput(task);
            _context.Tasks.Update(taskUpdate);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTask(Guid idTask)
        {
            var task = await _context.Tasks
                            .SingleOrDefaultAsync(d => d.IdTask == idTask);
            if (task == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
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
                throw new Exception("Identificador do projeto inválido.");
            }

            var TasksAvailable = project.Tasks.Any(t => !t.isDeleted);
            if (TasksAvailable)
            {
                throw new Exception("Existem tarefas pendentes neste projeto, conclua ou encerre as tarefas antes de remover o projeto.");
            }
            project.isDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
