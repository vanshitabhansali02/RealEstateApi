using DataAcess.Interfaces;
using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Interfaces
{
    
    public interface ILoginRepository:IGenericRepository<User>
    {
        Task<User> GetByUsername(string username);
        Task<Agent> GetAgentByUsername(string username);

    }
}
