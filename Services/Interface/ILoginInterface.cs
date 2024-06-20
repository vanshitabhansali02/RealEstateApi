using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public  interface ILoginInterface<T> where T : class
    {
        Task AddAsync(T entity);

    }
}
