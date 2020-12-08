using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _serviceUser;
        public UsersController(IUserService service)
        {
            _serviceUser = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            // IDENTIFICA SE CHAMADA ESTÁ CORRETA
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 badrequest (Solicitação invalida!)
            }

            try
            {
                return Ok(await _serviceUser.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            // IDENTIFICA SE CHAMADA ESTÁ CORRETA
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 badrequest (Solicitação invalida!)
            }

            try
            {
                return Ok(await _serviceUser.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate usuarioDto)
        {
            // IDENTIFICA SE CHAMADA ESTÁ CORRETA
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 badrequest (Solicitação invalida!)
            }

            try
            {
                var resultado = await _serviceUser.Post(usuarioDto);
                if (resultado != null)
                {
                    return Created(new Uri(Url.Link("GetWithId", new { id = resultado.Id })), resultado);
                }

                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate usuarioDto)
        {
            // IDENTIFICA SE CHAMADA ESTÁ CORRETA
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 badrequest (Solicitação invalida!)
            }

            try
            {
                var resultado = await _serviceUser.Put(usuarioDto);
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            // IDENTIFICA SE CHAMADA ESTÁ CORRETA
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 badrequest (Solicitação invalida!)
            }

            try
            {
                return Ok(await _serviceUser.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
