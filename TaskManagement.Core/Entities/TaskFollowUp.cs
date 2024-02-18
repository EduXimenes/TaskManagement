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
    public class TaskFollowUp : EntityBase
    {
        public TaskFollowUp(Guid idTask, string title, string description, DateTime expirationDate, TaskStatusCode status, TaskPriority priority, string comment, bool deleted, Guid userId) : base()
        {
            IdTask = idTask;
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
            Priority = priority;
            Comment = comment;
            isDeleted = deleted;
            UserId = userId;
            ChangeDate = DateTime.Now;
            idFollowUp = Id;
        }
        public TaskFollowUp()
        {
            
        }
        public Guid idFollowUp {  get; set; }
        public Guid IdTask { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatusCode Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string? Comment { get; set; } 
        public bool isDeleted { get; set; } 
        public Guid UserId { get; set;}
        public DateTime ChangeDate { get; set; }
    }
}
