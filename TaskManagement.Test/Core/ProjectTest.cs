using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Test.Core
{
    public class ProjectTest
    {
        [Fact]
        public void Task_should_have_priority()
        {
            var task = new Fixture().Create<TaskEntity>();
            

        }
    }
}
