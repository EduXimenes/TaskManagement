using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.InputModels;
using TaskManagement.Application.Services;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;

namespace TaskManagement.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _context;
        private readonly IMapper _mapper;


        public UserController(IUserService context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("/GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("/GetUser/{idUser}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid idUser)
        {
            var user = await _context.GetUser(idUser);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("CreateUser/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostUser(UserInputModel input)
        {
            var user = _mapper.Map<User>(input);
            await _context.AddUser(user);
            var result = _mapper.Map<UserViewModel>(user);
            return Created("New Project", result);
        }

        [HttpDelete("DeleteUser/{idUser}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid idUser)
        {
            var user = await _context.GetUser(idUser);
            if (user == null)
            {
                return NotFound();
            }
            await _context.DeleteUser(idUser);

            return NoContent();

        }

    }
}
