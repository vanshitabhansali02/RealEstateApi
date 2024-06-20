using DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
   

    public class UnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();


        }
    }
}
