using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IGnericRepository<T>
    {
      
        void Create(T entity);
       
    }
}
