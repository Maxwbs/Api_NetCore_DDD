using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (userDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _loginService.ConsulteLogin(userDto);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
