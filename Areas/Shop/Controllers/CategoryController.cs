using Assignment.DataAccess;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AspNetCoreHero.ToastNotification.Abstractions;
using PagedList;


namespace Assignment.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class CategoryController : Controller
    {
        private readonly BookShopDbContext _bookShopDbContext;
        public INotyfService _notyfService { get; }
        public CategoryController(BookShopDbContext bookShopDbContext, INotyfService notyfService)
        {
            _bookShopDbContext = bookShopDbContext;
            _notyfService = notyfService;
        }
        public IActionResult Index(int? page)
        {
            var categories = _bookShopDbContext.Category.Where(c => c.Status == "Active")
                .Union(_bookShopDbContext.Category.Where(c => c.Status == "Non-active"))
                .ToList();
            if (page == null) page = 1;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(categories.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            _notyfService.Success("You successfully create a new category");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory([Bind("ID,Name,Description,Alias,Status")]Category category)
        {
            if (ModelState.IsValid)
            {
               
                _bookShopDbContext.Category.Add(category);
                await _bookShopDbContext.SaveChangesAsync();
                _notyfService.Success("You successfully create a new category");
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            Category category = _bookShopDbContext.Category.FirstOrDefault(c => c.ID == id);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Active", Value = "Active" });
            items.Add(new SelectListItem { Text = "Non-active", Value = "Non-active" });
            ViewBag.Status = items;
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _bookShopDbContext.Entry(category).State = EntityState.Modified;
                await _bookShopDbContext.SaveChangesAsync();
                return RedirectToAction("index");
            }
                List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Active", Value = "Active" });
            items.Add(new SelectListItem { Text = "Non-active", Value = "Non-active" });
            ViewBag.Status = items;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirmed(int id)
        {
            Category category = _bookShopDbContext.Category.FirstOrDefault(c => c.ID == id);
            if (id == null ||category == null )
            {
                return View("Error");
            }
            _bookShopDbContext.Category.Remove(category);
            _bookShopDbContext.SaveChanges();
            _notyfService.Success("You successfully remoce a category");
            return RedirectToAction("Index");
        }

    }
}
