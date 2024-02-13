using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Entities
{
    public class Project 
    {
        public Project(string title, string description) 
        {
            Title = title; 
            Description = description;
            Tasks = new List<TaskEntity>();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TaskEntity> Tasks { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
