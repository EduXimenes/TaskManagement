using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.ViewModels
{
    public class CommentsViewModel
    {
        public Guid idTask { get; set; }
        public string? Comment { get; set; }
    }
}
