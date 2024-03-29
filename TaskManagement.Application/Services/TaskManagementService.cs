﻿using AutoMapper;
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
        public async Task<TaskEntityViewModel?> GetTask(Guid idTask)
        {
            var task = await _repository.GetTask(idTask);

            var viewModel = _mapper.Map<TaskEntityViewModel>(task);
            return viewModel;
        }
        public async Task<List<FollowUpViewModel>> GetAllFollowUp()
        {
            var followup = await _repository.GetAllFollowUp();

            var viewModel = _mapper.Map<List<FollowUpViewModel>>(followup);
            return viewModel;
        }
        public async Task<FollowUpViewModel?> GetFollowUp(Guid idFollowUp)
        {
            var followup = await _repository.GetFollowUp(idFollowUp);

            var viewModel = _mapper.Map<FollowUpViewModel>(followup);
            return viewModel;
        }
        public async Task<List<CommentsViewModel>> GetComments(Guid idTask)
        {
            var comments = await _repository.GetComments(idTask);

            var viewModel = _mapper.Map<List<CommentsViewModel>>(comments);
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
        public async Task<TaskEntity> AddComment(Guid idTask, string input)
        {
            var comment = new TaskComment(idTask, input);
            var task = await _repository.AddComment(idTask, comment);

            return task;
        }
        public async Task<TaskEntity> AddFollowUp(Guid idTask, TaskUpdateInputModel input, Guid userId)
        {
            var taskInput = _mapper.Map<TaskEntity>(input);
            return await _repository.AddFollowUp(idTask, taskInput, userId);
        }

        public async Task UpdateTask(Guid id, TaskUpdateInputModel task)
        {
            var taskInput = _mapper.Map<TaskEntity>(task);

            await _repository.UpdateTask(id, taskInput);
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
