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

            CreateMap<TaskInputModel, TaskEntity>();
            CreateMap<ProjectInputModel, Project>();
            CreateMap<TaskEntity, TaskInputModel>();
            CreateMap<Project, ProjectInputModel>();
        }
    }
}
