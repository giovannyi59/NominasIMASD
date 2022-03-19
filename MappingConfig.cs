using AutoMapper;
using Nomina2022.Modelos;
using Nomina2022.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomina2022
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<EmpleadoDto, Empleado>();
                config.CreateMap<Empleado, EmpleadoDto>();
            });

            return mappingConfig;
        }
    }
}
