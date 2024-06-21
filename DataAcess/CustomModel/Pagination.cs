using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.CustomModel
{
    public class Paginationparameters
    {
        public int PageSize { get; set; } = 10;
        public int currentpage { get; set; } = 1;
    }
}
