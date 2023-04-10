using Microsoft.AspNetCore.Mvc;

namespace Assignment.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
