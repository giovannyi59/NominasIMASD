using Nomina2022.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomina2022.Repositorio
{
    public interface IUserRepositorio
    {
        Task<int> Register(User user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExiste(string username);
    }
}
