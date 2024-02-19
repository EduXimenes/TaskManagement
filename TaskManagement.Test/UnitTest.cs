using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TaskManagement.API.Controllers;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.Services;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Enums;
using TaskManagement.Infrastructure.Persistence.Repositories;
using static TaskManagement.Core.Enums.TaskPriorityEnum;

namespace TaskManagement.Test
{
    public class UnitTest
    {
        // Teste para o método GetAllProjects
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
        
        // Teste para o método GetProject
        [Fact]
        public async Task GetProject_Returns_ProjectViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var projectId = Guid.NewGuid();
            var project = new Project("testeTitle","testeDesc") { Id = projectId };
            mockRepository.Setup(repo => repo.GetProject(projectId)).ReturnsAsync(project);

            var expectedViewModel = new ProjectViewModel { Id = projectId };
            mockMapper.Setup(mapper => mapper.Map<ProjectViewModel>(project)).Returns(expectedViewModel);

            // Act
            var result = await service.GetProject(projectId);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }
        
        // Teste para o método GetTask
        [Fact]
        public async Task GetTask_Returns_TaskEntityViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var task = new Fixture().Create<TaskEntity>();
            mockRepository.Setup(repo => repo.GetTask(task.Id)).ReturnsAsync(task);

            var expectedViewModel = new TaskEntityViewModel { IdTask = task.Id };
            mockMapper.Setup(mapper => mapper.Map<TaskEntityViewModel>(task)).Returns(expectedViewModel);

            // Act
            var result = await service.GetTask(task.Id);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        // Teste para o método GetAllFollowUp
        [Fact]
        public async Task GetAllFollowUp_Returns_FollowUpViewModelList()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var followUps = new List<TaskFollowUp>();
            mockRepository.Setup(repo => repo.GetAllFollowUp()).ReturnsAsync(followUps);

            var expectedViewModel = new List<FollowUpViewModel>();
            mockMapper.Setup(mapper => mapper.Map<List<FollowUpViewModel>>(followUps)).Returns(expectedViewModel);

            // Act
            var result = await service.GetAllFollowUp();

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        // Teste para o método AddProject
        [Fact]
        public async Task AddProject_Returns_ProjectViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var inputProject = new Fixture().Create<Project>();
            mockRepository.Setup(repo => repo.AddProject(inputProject)).ReturnsAsync(inputProject);

            var expectedViewModel = new ProjectViewModel();
            mockMapper.Setup(mapper => mapper.Map<ProjectViewModel>(inputProject)).Returns(expectedViewModel);

            // Act
            var result = await service.AddProject(inputProject);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }

        // Teste para o método AddTask
        [Fact]
        public async Task AddTask_Returns_TaskViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var projectId = Guid.NewGuid();
            var inputTask = new Fixture().Create<TaskEntity>();
            mockRepository.Setup(repo => repo.AddTask(projectId, inputTask)).ReturnsAsync(inputTask);

            var expectedViewModel = new TaskViewModel();
            mockMapper.Setup(mapper => mapper.Map<TaskViewModel>(inputTask)).Returns(expectedViewModel);

            // Act
            var result = await service.AddTask(projectId, inputTask);

            // Assert
            Assert.Equal(expectedViewModel, result);
        }
        // Teste para o método AddComment
        [Fact]
        public async Task AddComment_Returns_TaskEntity()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var taskId = Guid.NewGuid();
            var inputComment = "This is a new comment";
            var addedComment = new Fixture().Create<TaskEntity>();
            mockRepository.Setup(repo => repo.AddComment(taskId, It.IsAny<TaskComment>())).ReturnsAsync(addedComment);

            // Act
            var result = await service.AddComment(taskId, inputComment);

            // Assert
            Assert.Equal(addedComment, result);
        }

        // Teste para o método AddFollowUp
        [Fact]
        public async Task AddFollowUp_Returns_TaskEntity()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var inputModel = new TaskUpdateInputModel();
            var taskEntity = new Fixture().Create<TaskEntity>();
            mockRepository.Setup(repo => repo.AddFollowUp(taskId, It.IsAny<TaskEntity>(), userId)).ReturnsAsync(taskEntity);

            // Act
            var result = await service.AddFollowUp(taskId, inputModel, userId);

            // Assert
            Assert.Equal(taskEntity, result);
        }

        // Teste para o método UpdateTask
        [Fact]
        public async Task UpdateTask_Calls_Repository_UpdateTask()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var taskId = Guid.NewGuid();
            var inputModel = new TaskUpdateInputModel();
            mockMapper.Setup(mapper => mapper.Map<TaskEntity>(inputModel)).Returns(new Fixture().Create<TaskEntity>());

            // Act
            await service.UpdateTask(taskId, inputModel);

            // Assert
            mockRepository.Verify(repo => repo.UpdateTask(taskId, It.IsAny<TaskEntity>()), Times.Once);
        }

        // Teste para o método DeleteProject
        [Fact]
        public async Task DeleteProject_Calls_Repository_DeleteProject()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var projectId = Guid.NewGuid();

            // Act
            await service.DeleteProject(projectId);

            // Assert
            mockRepository.Verify(repo => repo.DeleteProject(projectId), Times.Once);
        }
        // Teste para o método DeleteTask
        [Fact]
        public async Task DeleteTask_Calls_Repository_DeleteTask()
        {
            // Arrange
            var mockRepository = new Mock<ITaskManagementRepository>();
            var mockMapper = new Mock<IMapper>();

            var service = new TaskManagementService(mockRepository.Object, mockMapper.Object);

            var taskId = Guid.NewGuid();

            // Act
            await service.DeleteTask(taskId);

            // Assert
            mockRepository.Verify(repo => repo.DeleteTask(taskId), Times.Once);
        }

    }
}

