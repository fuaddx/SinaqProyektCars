using Cars.Contexts;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cars.Controllers
{
	public class HomeController : Controller
	{
		DataDbContext _db {  get; set; }

        public HomeController(DataDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
		{
			return View();
		}
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
