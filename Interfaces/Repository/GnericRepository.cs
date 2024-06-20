using DataAcess.Data;
using Interfaces.IInterfaces;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class GnericRepository<T>: IGnericRepository<T> where T : class
    {
       private readonly ApplicationDbContext _context;
        public GnericRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

    }
}
