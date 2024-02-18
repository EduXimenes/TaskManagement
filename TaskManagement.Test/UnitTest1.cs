using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TaskManagement.API.Controllers;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.Services;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;
using static TaskManagement.Core.Enums.TaskPriorityEnum;

namespace TaskManagement.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetAllProjects_Returns_OkResult_With_Projects()
        {
            // Arrange
            var mockService = new Mock<ITaskManagementService>();
            var mockMapper = new Mock<IMapper>();

            var projects = new List<ProjectViewModel> { new ProjectViewModel { Id = Guid.NewGuid(), Description = "Project 1" } };
            mockService.Setup(service => service.GetAllProjects()).ReturnsAsync(projects);

            var controller = new TaskManagementController(mockMapper.Object, mockService.Object);

            // Act
            var result = await controller.GetAllProjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProjectViewModel>>(okResult.Value);
            Assert.Equal(projects, model);
        }
        [Fact]
        public async Task GetProjectById_Returns_OkResult_With_ProjectViewModel()
        {
            // Arrange
            var mockService = new Mock<ITaskManagementService>();
            var mockMapper = new Mock<IMapper>();

            var project = new Fixture().Create<Project>();
            var viewModel = new Fixture().Create<ProjectViewModel>();

            mockService.Setup(service => service.GetProject(project.Id)).ReturnsAsync(viewModel);
            mockMapper.Setup(mapper => mapper.Map<ProjectViewModel>(project)).Returns(viewModel);

            var controller = new TaskManagementController(mockMapper.Object, mockService.Object);

            // Act
            var result = await controller.GetProjectById(project.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ProjectViewModel>(okResult.Value);
            Assert.Equal(viewModel, model);
        }
        [Fact]
        public async Task PostProject_Returns_CreatedResult_With_ProjectViewModel()
        {
            // Arrange
            var mockService = new Mock<ITaskManagementService>();
            var mockMapper = new Mock<IMapper>();

            var projectInputModel = new Fixture().Create<ProjectInputModel>();
            var project = new Fixture().Create<Project>();
            var viewModel = new Fixture().Create<ProjectViewModel>();

            mockMapper.Setup(mapper => mapper.Map<Project>(projectInputModel)).Returns(project);
            mockService.Setup(service => service.AddProject(project)).ReturnsAsync(viewModel);
            mockMapper.Setup(mapper => mapper.Map<ProjectViewModel>(project)).Returns(viewModel);

            var controller = new TaskManagementController(mockMapper.Object, mockService.Object);

            // Act
            var result = await controller.PostProject(projectInputModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<ProjectViewModel>(createdResult.Value);
            Assert.Equal(viewModel, model);
        }

        [Fact]
        public async Task PostTask_Returns_CreatedResult_With_TaskViewModel()
        {
            // Arrange
            var mockService = new Mock<ITaskManagementService>();
            var mockMapper = new Mock<IMapper>();

            var projectId = Guid.NewGuid();
            var taskInputModel = new Fixture().Create<TaskInputModel>();
            var task =  new Fixture().Create<TaskEntity>();
            var viewModel = new Fixture().Create<TaskViewModel>();

            mockService.Setup(service => service.GetProject(projectId)).ReturnsAsync(new Fixture().Create<ProjectViewModel>());
            mockMapper.Setup(mapper => mapper.Map<TaskEntity>(taskInputModel)).Returns(task);
            mockService.Setup(service => service.AddTask(projectId, task)).ReturnsAsync(viewModel);
            mockMapper.Setup(mapper => mapper.Map<TaskViewModel>(task)).Returns(viewModel);

            var controller = new TaskManagementController(mockMapper.Object, mockService.Object);

            // Act
            var result = await controller.PostTask(projectId, taskInputModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<TaskViewModel>(createdResult.Value);
            Assert.Equal(viewModel, model);
        }

    }
}

