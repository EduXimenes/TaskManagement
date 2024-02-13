using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<Project>? GetAllProjects()
        {
            var projects = _context.Projects;
            if (projects == null)
            {
                return null;
            }
            return _context.Projects
                 .Include(d => d.Tasks)
                 .Where(d => !d.isDeleted)
                 .ToList();
        }
        public Project? GetProject(Guid id)
        {

            var project = _context.Projects
                    .Include(d => d.Tasks)
                    .SingleOrDefault(d => d.Id == id);
            return project;
        }
        public TaskEntity? GetTask(Guid id)
        {
            return _context.Tasks
                    .SingleOrDefault(d => d.IdTask == id);
        }
        public Project AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }
        public TaskEntity AddTask(Guid id, TaskEntity task)
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
            _context.SaveChanges();
            return task;

        }
        public TaskEntity AddComments(Guid id, string comments)
        {
            var task = GetTask(id);
            if (task == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }
            task.Comments += ", " + comments;
            _context.SaveChanges();
            return task;
        }
        public void UpdateTask(Guid id, TaskEntity task)
        {
            var taskUpdate = _context.Tasks
                    .SingleOrDefault(d => d.IdTask == id);
            if (taskUpdate == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }
            taskUpdate.Update(task);

            _context.SaveChanges();
        }
        public void DeleteTask(Guid idTask)
        {
            var task = _context.Tasks
                            .SingleOrDefault(d => d.IdTask == idTask);
            if (task == null)
            {
                throw new Exception("Identificador de tarefa inválido.");
            }
            task.isDeleted = true;
            _context.SaveChanges();
        }

        public void DeleteProject(Guid idProject)
        {
            var project = _context.Projects
                            .SingleOrDefault(d => d.Id == idProject);
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
            _context.SaveChanges();
        }
    }
}
