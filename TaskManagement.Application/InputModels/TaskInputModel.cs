using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Enums;
using static TaskManagement.Core.Enums.TaskPriorityEnum;
using static TaskManagement.Core.Enums.TaskStatusEnum;

namespace TaskManagement.Application.InputModels
{
    public class TaskInputModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatusCode Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
