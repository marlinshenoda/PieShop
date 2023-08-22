using Microsoft.AspNetCore.Mvc;
using PieShop.Data.Reposities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Web.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Categories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
