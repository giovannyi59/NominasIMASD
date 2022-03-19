using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomina2022.Modelos;
using Nomina2022.Modelos.Dto;
using Nomina2022.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomina2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepositorio _userRepositorio;
        protected ResponseDto _response;

        public UsersController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var respuesta = await _userRepositorio.Register(
                new User
                {
                    UserName = user.UserName
                },    user.Password);

            if (respuesta==-1)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya existe";
                return BadRequest(_response);
            }

            if (respuesta == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario creado con exito";
            _response.Result = respuesta;
            return Ok(_response);
            
        }

    }
}
