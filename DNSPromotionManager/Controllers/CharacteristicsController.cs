using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class CharacteristicsController : Controller
    {
        DNSContext db;

        public CharacteristicsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            ViewBag.CardEvent = e;
            return PartialView("Card", db.Characteristics.Find(id ?? "") ?? new Characteristic());
        }
        [HttpPost]
        public IActionResult Card(Characteristic model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.Characteristics.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "Characteristics" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
