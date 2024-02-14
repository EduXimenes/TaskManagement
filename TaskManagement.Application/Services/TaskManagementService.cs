using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;
using TaskManagement.Infrastructure.Persistence.Repositories;

namespace TaskManagement.Application.Services
{
    public class TaskManagementService : ITaskManagementService
    {
        private readonly ITaskManagementRepository _repository;
        private readonly IMapper _mapper;
        public TaskManagementService(ITaskManagementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProjectViewModel>> GetAllProjects()
        {
            var projects = await _repository.GetAllProjects();

            var viewModel = _mapper.Map<List<ProjectViewModel>>(projects);
            return viewModel;
        }
        public async Task<ProjectViewModel?> GetProject(Guid idProject)
        {
            var projects = await _repository.GetProject(idProject);

            var viewModel = _mapper.Map<ProjectViewModel>(projects);
            return viewModel;
        }
        public async Task<TaskViewModel?> GetTask(Guid idTask)
        {
            var task = await _repository.GetTask(idTask);

            var viewModel = _mapper.Map<TaskViewModel>(task);
            return viewModel;
        }

        public async Task<ProjectViewModel> AddProject(Project input)
        {
            var project = await _repository.AddProject(input);

            var viewModel = _mapper.Map<ProjectViewModel>(project);
            return viewModel;
        }
        public async Task<TaskViewModel> AddTask(Guid idProject, TaskEntity input)
        {
            var task = await _repository.AddTask(idProject, input);

            var viewModel = _mapper.Map<TaskViewModel>(task);
            return viewModel;
        }
        public async Task<TaskViewModel> AddComments(Guid idTask, string input)
        {
            var task = await _repository.AddComments(idTask, input);

            var viewModel = _mapper.Map<TaskViewModel>(task);
            return viewModel;
        }
        public async Task<TaskEntity> AddFollowUp(Guid id, TaskUpdateInputModel input, Guid userId)
        {
            var taskInput = _mapper.Map<TaskEntity>(input);
            return await _repository.AddFollowUp(id, taskInput, userId);
        }

        public async Task UpdateTask(Guid id, TaskEntity task)
        {
            await _repository.UpdateTask(id, task);
        }

        public async Task DeleteProject(Guid idProject)
        {
            await _repository.DeleteProject(idProject);
        }
        public async Task DeleteTask(Guid idTask)
        {
            await _repository.DeleteTask(idTask);
        }
    }
}
