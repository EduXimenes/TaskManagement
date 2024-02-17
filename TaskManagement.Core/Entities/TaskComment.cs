using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Entities
{
    public class TaskComment : EntityBase
    {
        public TaskComment(Guid idTask, string description): base()
        {
            idComment = Id;
            this.idTask = idTask;
            Description = description;
        }

        public Guid idComment {  get; set; }
        public Guid idTask { get; set; }
        public string Description { get; set; }
    }
}
