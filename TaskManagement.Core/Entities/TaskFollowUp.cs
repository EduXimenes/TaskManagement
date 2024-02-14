using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static TaskManagement.Core.Enums.TaskPriorityEnum;
using static TaskManagement.Core.Enums.TaskStatusEnum;

namespace TaskManagement.Core.Entities
{
    public class TaskFollowUp
    {
        public TaskFollowUp(Guid idTask, string title, string description, DateTime expirationDate, TaskStatusCode status, TaskPriority priority, string comments, bool isDeleted, Guid userId)
        {
            IdTask = idTask;
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
            Priority = priority;
            Comments = comments;
            this.isDeleted = isDeleted;
            UserId = userId;
            ChangeDate = DateTime.Now;
            this.idFollowUp = Guid.NewGuid();
        }
        public Guid idFollowUp {  get; set; }
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatusCode Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string Comments { get; set; } 
        public bool isDeleted { get; set; } 
        public Guid UserId { get; set;}
        public DateTime ChangeDate { get; set; }
    }
}
