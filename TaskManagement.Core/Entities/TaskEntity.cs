using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Enums;
using static TaskManagement.Core.Enums.TaskPriorityEnum;
using static TaskManagement.Core.Enums.TaskStatusEnum;

namespace TaskManagement.Core.Entities
{
    public class TaskEntity 
    {
        public TaskEntity(string title, string description, DateTime expirationDate, TaskStatusCode status)
        {
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
        }

        public TaskEntity(string title, string description, DateTime expirationDate, TaskStatusCode status, TaskPriority priority)
        {
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
            Priority = priority;
            IdTask = Guid.NewGuid();
        }
        
        public Guid Id { get; set; }
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatusCode Status {get; set;}
        public TaskPriority Priority { get; set; }
        public string Comments { get; set; } = string.Empty;
        public bool isDeleted { get; set; } = false;
        public List<TaskFollowUp> TaskFollowUp { get; set; } = new List<TaskFollowUp>();

        public void UpdateInput(TaskEntity entity)
        {
            Title = entity.Title;
            Description = entity.Description;
            ExpirationDate = entity.ExpirationDate;
            Status = entity.Status;
            Comments = entity.Comments;
        }

    }
}
