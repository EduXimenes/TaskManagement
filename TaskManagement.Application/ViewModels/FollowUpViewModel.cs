using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagement.Core.Enums.TaskPriorityEnum;
using static TaskManagement.Core.Enums.TaskStatusEnum;

namespace TaskManagement.Application.ViewModels
{
    public class FollowUpViewModel
    {
        public Guid idFollowUp { get; set; }
        public Guid IdTask { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatusCode Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string? Comment { get; set; }
        public bool isDeleted { get; set; }
        public Guid UserId { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
