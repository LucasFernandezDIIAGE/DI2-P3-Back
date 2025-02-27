using Business.DTOs;
using Business.Services;
using Domain.Entities;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace DI2_P2_Back.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public PasswordController(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpGet("passwords")]
        public async Task<ActionResult<IEnumerable<Password>>> GetPasswords()
        {
            var passwordsDto = await _passwordService.GetAllPasswords();

            return Ok(passwordsDto);
        }

        [HttpPost("passwords")]
        public async Task<ActionResult<Password>> CreatePassword(PasswordDTO passwordDto)
        {
            var encryptedPassword = _passwordService.EncryptPassword(passwordDto.EncryptedValue, 0);
            passwordDto.EncryptedValue = encryptedPassword;

            var addedPassword = await _passwordService.CreatePassword(passwordDto);

            return Ok(addedPassword);
        }

        [HttpGet("passwords/{applicationId}")]
        public async Task<ActionResult<IEnumerable<Password>>> GetPasswordsByApplicationId(int applicationId)
        {
            var passwordsDto = await _passwordService.GetPasswordsByApplicationId(applicationId);
            return Ok(passwordsDto);
        }

        [HttpDelete("passwords/{id}")]
        public async Task<ActionResult<Password>> DeletePassword(int id)
        {
            var isPasswordDeleted = await _passwordService.DeletePassword(id);

            if (!isPasswordDeleted)
            {
                return NotFound();
            }

            return Ok(isPasswordDeleted);
        }

    }
}
