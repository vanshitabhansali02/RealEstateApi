using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class PaginatedDto<T>
    {
       
            public List<T> Items { get; set; }
            public int TotalCount { get; set; }
          public int currentpage { get; set; } 
           public int pageSize { get; set; } 

    }
}
