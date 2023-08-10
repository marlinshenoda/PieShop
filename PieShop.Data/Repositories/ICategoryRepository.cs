using PieShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Data.Reposities
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
