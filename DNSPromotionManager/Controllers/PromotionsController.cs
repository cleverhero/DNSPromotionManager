using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class PromotionsController : Controller
    {
        DNSContext db;

        public PromotionsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult PromotionsCard(String id, TableItemEvent e)
        {
            ViewBag.CardEvent = e;
            return PartialView("Card", db.Promotions.Find(id ?? "") ?? new Promotion());
        }
        [HttpPost]
        public IActionResult PromotionsCard(Promotion model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.Promotions.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "Promotions" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
