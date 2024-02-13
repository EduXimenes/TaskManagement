using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;
using TaskManagement.Infrastructure.Persistence.Repositories;

namespace TaskManagement.API.Controllers
{
        [Route("api/task-management")]
        [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskManagementRepository _repository;
        private readonly IMapper _mapper;
        public TaskManagementController(ITaskManagementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/GetAllProjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllProjects()
        {
            var projects = _repository.GetAllProjects();

            var viewModel = _mapper.Map<List<ProjectViewModel>>(projects);
            return Ok(viewModel);
        }
        [HttpGet("/GetProject/{idProject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProjectById(Guid idProject)
        {
            var project = _repository.GetProject(idProject);

            if (project == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ProjectViewModel>(project);

            return Ok(viewModel);
        }
        [HttpGet("/GetTask/{idTask}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTask(Guid idTask)
        {
            var task = _repository.GetTask(idTask);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        [HttpPut("/UpdateTask/{idTask}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTask(Guid idTask, TaskInputModel task)
        {
            var updateTask = _repository.GetTask(idTask);
            if (updateTask == null)
            {
                return NotFound();
            }
            _repository.AddFollowUp(idTask, updateTask, Guid.NewGuid());

            var input = _mapper.Map<TaskEntity>(task);
            _repository.UpdateTask(idTask, input);

            return Ok();
        }
        [HttpPost("CreateProject/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PostProject(ProjectInputModel input)
        {
            var project = _mapper.Map<Project>(input);
            _repository.AddProject(project);
            return Created("New Project", project);
        }
        [HttpPost("CreateTask/{idProject}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostTask(Guid idProject, TaskInputModel input)
        {
            var project = _repository.GetProject(idProject);
            if (project == null)
            {
                return NotFound();
            }
            var task = _mapper.Map<TaskEntity>(input);
            task.Id = idProject;
            _repository.AddTask(idProject, task);

            return Created("New Task", task);
        }
        [HttpPost("AddComment/{idTask}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostComment(Guid idTask, string input)
        {
            var task = _repository.GetTask(idTask);
            if (task == null)
            {
                return NotFound();
            }            
            _repository.AddComments(idTask, input);

            return Created("New comment", task);
        }

        [HttpDelete("DeleteTask/{idTask}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTask(Guid idTask)
        {
            var task = _repository.GetTask(idTask);
            if (task == null)
            {
                return NotFound();
            }
            _repository.DeleteTask(idTask);

            return NoContent();

        }
        [HttpDelete("DeleteProject/{idProject}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProject(Guid idProject)
        {
            var project = _repository.GetProject(idProject);
            if (project == null)
            {
                return NotFound();
            }
            _repository.DeleteProject(idProject);

            return NoContent();

        }

    }
}
