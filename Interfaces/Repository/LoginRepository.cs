using DataAcess.Data;
using DataAcess.Interfaces;
using DataAcess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    
    public  class LoginRepository : GenericRepository<User>, ILoginRepository
    {
        public LoginRepository(ApplicationDbContext context) : base(context) { }
        public async Task<User> GetByUsername(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<Agent> GetAgentByUsername(string email)
        {
            return await _context.Agents.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
