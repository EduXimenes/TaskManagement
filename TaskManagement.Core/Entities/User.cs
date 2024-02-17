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
        public User(Role role, string name, string email, string password) : base()
        {
            Role = role;
            Name = name;
            Email = email;
            Password = password;
            isDeleted = false;
            idUser = Id;
        }
        public Guid idUser { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isDeleted {  get; set; }
    }
}
