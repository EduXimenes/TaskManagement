using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services;
using static TaskManagement.Core.Enums.RoleEnum;

namespace TaskManagement.API.Controllers
{
    [Route("api/performance")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IPerformanceService _context;
        private readonly IMapper _mapper;


        public PerformanceController(IPerformanceService context, IMapper mapper,IUserService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(Guid idUser) 
        {
            var user = _user.GetUser(idUser);

            if (user.Result == null)
                return NotFound("Usuário não encontrado.");

            if (user.Result.Role == Role.Gerente)
                return BadRequest("Usuário não possui função de gerente.");

            var report = await _context.GetReport(idUser);

            return Ok(report);

        }
    }
}
