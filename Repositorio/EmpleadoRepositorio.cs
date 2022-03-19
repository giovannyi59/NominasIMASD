using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nomina2022.Data;
using Nomina2022.Modelos;
using Nomina2022.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomina2022.Repositorio
{
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public EmpleadoRepositorio(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<EmpleadoDto> CreateUpdate(EmpleadoDto empleadoDto)
        {
            Empleado empleado = _mapper.Map<EmpleadoDto, Empleado>(empleadoDto);
            if(empleado.Id > 0)
            {
                _db.Empleados.Update(empleado);
            }
            else
            {
                await _db.Empleados.AddAsync(empleado);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Empleado, EmpleadoDto>(empleado);
        }

        public async Task<bool> DeleteEmpleado(int id)
        {
            try
            {
                Empleado empleado = await _db.Empleados.FindAsync(id);
                if(empleado == null){ 
                        return false;
                }
                _db.Empleados.Remove(empleado);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<EmpleadoDto> GetEmpleadoById(int id)
        {
            Empleado empleado = await _db.Empleados.FindAsync(id);
            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<List<EmpleadoDto>> GetEmpleados()
        {
            List<Empleado> lista = await _db.Empleados.ToListAsync();
            return _mapper.Map<List< EmpleadoDto >> (lista);
        }
    }
}
