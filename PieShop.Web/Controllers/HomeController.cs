using Microsoft.AspNetCore.Mvc;
using PieShop.Core.ViewModel;
using PieShop.Data.Reposities;
using PieShop.Web.Models;
using System.Diagnostics;

namespace PieShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            };

            return View(homeViewModel);
        }
    }
}