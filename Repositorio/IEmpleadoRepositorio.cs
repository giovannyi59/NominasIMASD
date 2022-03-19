using Nomina2022.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomina2022.Repositorio
{
    public interface IEmpleadoRepositorio
    {

        Task<List<EmpleadoDto>> GetEmpleados();

        Task<EmpleadoDto> GetEmpleadoById(int id);

        Task<EmpleadoDto> CreateUpdate(EmpleadoDto empleadoDto);

        Task<bool> DeleteEmpleado(int id);
    }
}
