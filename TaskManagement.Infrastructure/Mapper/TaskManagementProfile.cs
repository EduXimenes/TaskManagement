using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Mapper
{
    public class TaskManagementProfile : Profile
    {
        public TaskManagementProfile() 
        {
            CreateMap<TaskEntity, TaskViewModel>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<TaskEntity, TaskInputModel>();
            CreateMap<Project, ProjectInputModel>();
            CreateMap<TaskInputModel, TaskEntity>();
            CreateMap<TaskUpdateInputModel, TaskEntity>();
            CreateMap<TaskEntity, TaskUpdateInputModel>();
            CreateMap<TaskEntity, TaskEntityViewModel>();
            CreateMap<TaskEntityViewModel, TaskEntity>();
            CreateMap<TaskUpdateInputModel, TaskViewModel>();
            CreateMap<ProjectInputModel, Project>();
            CreateMap<TaskViewModel, TaskEntity>();
            CreateMap<TaskFollowUp, FollowUpViewModel>();
            CreateMap<FollowUpViewModel, TaskFollowUp>();
            CreateMap<TaskComment, CommentsViewModel>();
            CreateMap<CommentsViewModel, TaskComment>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserInputModel, User>();
        }
    }
}
