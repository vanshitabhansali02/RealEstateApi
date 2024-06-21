using DataAcess.Data;
using DataAcess.Interfaces;
using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AgentPorpertyRepository:GenericRepository<AgentProperty>,IAgentPropertyRepository

    {
        public AgentPorpertyRepository(ApplicationDbContext context) : base(context) { }

    }
}
