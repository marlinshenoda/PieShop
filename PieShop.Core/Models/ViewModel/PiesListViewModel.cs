using PieShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Core.ViewModel
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public string CurrentCategory { get; set; }
    }
}
