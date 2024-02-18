using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagement.Core.Enums.RoleEnum;

namespace TaskManagement.Application.InputModels
{
    public class UserInputModel
    {
        public Role Role { get; set; }
        public string? Name { get; set; }
    }
}
