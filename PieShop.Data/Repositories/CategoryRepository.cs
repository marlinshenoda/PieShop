using PieShop.Core.Models;
using PieShop.Data;
using PieShop.Data.Reposities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public CategoryRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Category> Categories => _appDbContext.Category;
    }
}
