using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.InputModels;

namespace TaskManagement.Test.Application
{
    public class TaskManagementApplicationTest
    {
        [Fact]
        public async Task Should_return_200_ok_When_send_eventAsync()
        {
            var input = new Fixture().Create<ProjectInputModel>();
            var api = new TaskManagementFactory();
            var httpClient = api.CreateClient();

            var response = await httpClient.PostAsJsonAsync("/api/task-management/CreateProject", input);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task Should_Create_Task_When_Send_input()
        {


            Assert.Equal();
        }
    }
}
