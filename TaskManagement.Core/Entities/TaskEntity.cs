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
    public class TaskEntity  : EntityBase
    {
        public TaskEntity(string title, string description, DateTime expirationDate, TaskStatusCode status)
        {
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
        }

        public TaskEntity(string title, string description, DateTime expirationDate, TaskStatusCode status, TaskPriority priority, Guid idProject) : base()
        {
            IdProject = IdProject;
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
            Priority = priority;
            IdTask = Id;
        }
        
        public Guid IdProject { get; set; }
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatusCode Status {get; set;}
        public TaskPriority Priority { get; set; }
        public bool isDeleted { get; set; } = false;
        public List<TaskComment> Comments { get; set; } = new List<TaskComment>();
        //blic List<TaskFollowUp> TaskFollowUp { get; set; } = new List<TaskFollowUp>();

        public void UpdateInput(TaskEntity entity)
        {
            Title = entity.Title;
            Description = entity.Description;
            ExpirationDate = entity.ExpirationDate;
            Status = entity.Status;
        }

    }
}
