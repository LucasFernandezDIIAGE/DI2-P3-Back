using Business.DTOs;
using Business.Services;
using Domain.Entities;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace DI2_P2_Back.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationService _applicationService;

        public ApplicationController(ApplicationDbContext context, IApplicationService applicationService)
        {
            _context = context;
            _applicationService = applicationService;
        }

        [HttpGet("applications")]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            var applicationsDto = await _applicationService.GetAllApplications();

            return Ok(applicationsDto);
        }

        [HttpGet("applications/{id}")]
        public async Task<ActionResult<Application>> GetApplicationById(int id)
        {
            var applicationDto = await _applicationService.GetApplicationById(id);
            return Ok(applicationDto);
        }


        [HttpPost("applications")]
        public async Task<ActionResult<Application>> CreateApplication(ApplicationDTO applicationDto)
        {
            var addedApplication = await _applicationService.CreateApplication(applicationDto);

            return Ok(addedApplication);
        }
    }
}
