using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagement.Core.Enums.RoleEnum;

namespace TaskManagement.Application.ViewModels
{
    public class UserViewModel
    {
        public Guid id {  get; set; }
        public Role Role { get; set; }
        public string? Name { get; set; }
        public bool isDeleted { get; set; }
    }
}
