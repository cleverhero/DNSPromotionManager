using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;
using Microsoft.EntityFrameworkCore;

namespace DNSPromotionManager.Controllers 
{
    public class KindsController: Controller
    {
        DNSContext db;

        public KindsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            ViewBag.CardEvent = e;
            return PartialView("Card", db.Kinds.Find(id ?? "") ?? new Kind());
        }

        [HttpPost]
        public IActionResult Card(Kind model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.Kinds.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "Kinds" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
