using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagement.Core.Enums.RoleEnum;

namespace TaskManagement.Core.Entities
{
    public class User : EntityBase
    {
        public User(Role role, string name) : base()
        {
            Role = role;
            Name = name;
            isDeleted = false;
            idUser = Id;
        }
        public Guid idUser { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public bool isDeleted {  get; set; }
    }
}
