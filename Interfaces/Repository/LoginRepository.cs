using DataAcess.Data;
using Interfaces.IInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interfaces
{
    
    public  class LoginRepository
    {
        private readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;

        }

      
    
    }
}
