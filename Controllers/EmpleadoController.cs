using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nomina2022.Data;
using Nomina2022.Modelos;
using Nomina2022.Modelos.Dto;
using Nomina2022.Repositorio;

namespace Nomina2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoRepositorio _empleadoRepositorio;
        protected ResponseDto _response;

        public EmpleadoController(IEmpleadoRepositorio empleadoRepositorio)
        {
            _empleadoRepositorio = empleadoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Empleadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            try
            {
                var lista = await _empleadoRepositorio.GetEmpleados();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Empleados";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        // GET: api/Empleadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _empleadoRepositorio.GetEmpleadoById(id);
            if (empleado ==null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "empleado no existe";
                return NotFound(_response);
            }
            _response.Result = empleado;
            _response.DisplayMessage = "Informacion del Empleado";
            return Ok(_response);
        }

        // PUT: api/Empleadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, EmpleadoDto empleadoDto)
        {
            try
            {
                EmpleadoDto model = await _empleadoRepositorio.CreateUpdate(empleadoDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Empleadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(EmpleadoDto empleadoDto)
        {
            try
            {
                EmpleadoDto model = await _empleadoRepositorio.CreateUpdate(empleadoDto);
                _response.Result = model;
                return CreatedAtAction("GetEmpleado", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

            
        }

        // DELETE: api/Empleadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                bool estaEliminado = await _empleadoRepositorio.DeleteEmpleado(id);
                if (estaEliminado)
                {
                    _response.Result = "esta eliminado";
                    _response.DisplayMessage = "Empleado eliminado con exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar el empleado";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
        
    }
}
