using AspNetCoreHero.ToastNotification.Abstractions;
using Assignment.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class HomeController : Controller
    {
        public INotyfService _notyfService { get; }
        public HomeController( INotyfService notyfService)
        {
            
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            _notyfService.Information("Welcome to the Shop Owner board");
            return View();
        }
    }
}
