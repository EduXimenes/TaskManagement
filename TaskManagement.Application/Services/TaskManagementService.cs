using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Infrastructure.Persistence.Repositories;

namespace TaskManagement.Application.Services
{
    public class TaskManagementService
    {
        private readonly ITaskManagementRepository _repository;

        public TaskManagementService(ITaskManagementRepository repository)
        {
            _repository = repository;
        }

        //public async Task<Project> GetAllProjects()
        //{

        //}
    }
}
