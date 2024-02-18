using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.ViewModels;
using TaskManagement.Infrastructure.Persistence.Repositories;
using static TaskManagement.Core.Enums.TaskStatusEnum;

namespace TaskManagement.Application.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IUserRepository _users;
        private readonly ITaskManagementRepository _tasks;
        private readonly IMapper _mapper;
        public PerformanceService(IUserRepository users, IMapper mapper, ITaskManagementRepository tasks)
        {
            _users = users;
            _tasks = tasks;
            _mapper = mapper;
        }

        public async Task<List<ReportViewModel>> GetReport(Guid idUser)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-30);

            var followupList = await _tasks.GetAllFollowUp();
            var followup = followupList
                .Where(f => f.ChangeDate >= startDate && f.ChangeDate <= endDate && f.Status == TaskStatusCode.Concluida)
                .ToList();

            var userTasksCount = followup.GroupBy(f => f.UserId)
                .Select(g => new ReportViewModel
                {
                    idUser = g.Key,
                    AverageTasksCompleted = g.Count()
                })
                .ToList();

            return userTasksCount;
        }
    }
}