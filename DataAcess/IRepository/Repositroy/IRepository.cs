using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.IRepository.Repositroy
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity); 
    }
}
