using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.Services;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;

namespace TaskManagement.API.Controllers
{
        [Route("api/task-management")]
        [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskManagementService _context;
        private readonly IMapper _mapper;
        public TaskManagementController(IMapper mapper, TaskManagementService taskManagementService)
        {
            _mapper = mapper;
            _context = taskManagementService;
        }

        [HttpGet("/GetAllProjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _context.GetAllProjects();
            return Ok(projects);
        }
        
        [HttpGet("/GetProject/{idProject}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProjectById(Guid idProject)
        {
            var project = await _context.GetProject(idProject);

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
        public async Task<IActionResult> GetTask(Guid idTask)
        {
            var task = await _context.GetTask(idTask);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        [HttpGet("/GetFollowUps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFollowUp()
        {
            var followup = await _context.GetAllFollowUp();
            return Ok(followup);
        }
        [HttpGet("/GetAllFollowUp/{idFollowUp}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowUp(Guid idFollowUp)
        {
            var followup = await _context.GetFollowUp(idFollowUp);

            if (followup == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<TaskFollowUp>(followup);

            return Ok(viewModel);
        }
        [HttpGet("/GetComments/{idTask}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComments(Guid idTask)
        {
            var comments = await _context.GetComments(idTask);
            return Ok(comments);
        }

        [HttpPut("/UpdateTask/{idTask}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(Guid idTask,Guid idUser, TaskUpdateInputModel task)
        {
            var updateTask = await _context.GetTask(idTask);
            if (updateTask == null)
            {
                return NotFound();
            }
            await _context.UpdateTask(idTask, task);

            await _context.AddFollowUp(idTask, task, idUser);

            return Ok();
        }
        [HttpPost("CreateProject/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostProject(ProjectInputModel input)
        {
            var project = _mapper.Map<Project>(input);
            await _context.AddProject(project);
            var result = _mapper.Map<ProjectViewModel>(project);
            return Created("New Project", result);
        }
        [HttpPost("CreateTask/{idProject}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostTask(Guid idProject, TaskInputModel input)
        {
            var project = await _context.GetProject(idProject);
            if (project == null)
            {
                return NotFound();
            }
            var task = _mapper.Map<TaskEntity>(input);
            task.Id = idProject;
            await _context.AddTask(idProject, task);
            var result = _mapper.Map<TaskViewModel>(task);

            return Created("New Task", result);
        }
        [HttpPost("AddComment/{idTask}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostComment(Guid idTask, Guid idUser, [FromBody] string input)
        {
            var task = await _context.GetTask(idTask);
            if (task == null)
            {
                return NotFound();
            }            
            var taskComment = await _context.AddComment(idTask, input);
            var taskInput = _mapper.Map<TaskUpdateInputModel>(taskComment);
            await _context.AddFollowUp(idTask, taskInput, idUser);

            return Ok(taskComment.Comments);
        }

        [HttpDelete("DeleteTask/{idTask}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid idTask)
        {
            var task = await _context.GetTask(idTask);
            if (task == null)
            {
                return NotFound();
            }
            await _context.DeleteTask(idTask);

            return NoContent();
        }
        [HttpDelete("DeleteProject/{idProject}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProject(Guid idProject)
        {
            var project = await _context.GetProject(idProject);
            if (project == null)
            {
                return NotFound();
            }
            await _context.DeleteProject(idProject);

            return NoContent();

        }

    }
}
