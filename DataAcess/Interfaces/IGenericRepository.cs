﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Interfaces
{
    public  interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
    }
}
