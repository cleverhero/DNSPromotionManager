using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DNSPromotionManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace DNSPromotionManager.Controllers
{
    public class HomeController: Controller
    {
        DNSContext db;

        public HomeController(DNSContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tables(String name)
        {
            Table Table = TableManager.getInstance().Tables[name];
            return View("Table", Table.Load());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
