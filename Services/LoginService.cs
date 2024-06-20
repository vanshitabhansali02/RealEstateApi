using DataAcess.Data;
using DataAcess.DTOs;
using DataAcess.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginService<T> : ILoginInterface<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        
        public LoginService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;

        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        //public async Task<string> Login(T entity)
        //{
        //    var user=_context.Users.FirstOrDefault(x=>x.Username==entity.);
        //    if (entity == null)
        //    {
        //        return  "User not foumd";
        //    }
        //    else if_
        //    {

        //    }


        //}
    }
}
