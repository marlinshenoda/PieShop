using Microsoft.AspNetCore.Mvc;

namespace PieShop.Web.Controllers
{
    public class UserController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
