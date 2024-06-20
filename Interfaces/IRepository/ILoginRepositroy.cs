using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.IInterfaces
{
    public  interface IGenericRepositroy<T>where T : class
    {
        public void  Add(T entity);
    }
}
