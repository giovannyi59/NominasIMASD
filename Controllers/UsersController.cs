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

            if (respuesta== "existe")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya existe";
                return BadRequest(_response);
            }

            if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario creado con exito";
            //_response.Result = respuesta;
            JwTPackage jtp = new JwTPackage();
            jtp.UserName = user.UserName;
            jtp.Token = respuesta;
            _response.Result = jtp;
            return Ok(_response);
            
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _userRepositorio.Login(user.UserName, user.Password);

            if (respuesta == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no existe";
                return BadRequest(_response);
            }
            if (respuesta == "wrongrespuesta")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Password incorrecta";
                return BadRequest(_response);
            }

            //_response.Result = respuesta;
            JwTPackage jtp = new JwTPackage();
            jtp.UserName = user.UserName;
            jtp.Token = respuesta;
            _response.Result = jtp;            
            _response.DisplayMessage = "Usuario conectado";
            return Ok(_response);
        }

    }

    public class JwTPackage
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
