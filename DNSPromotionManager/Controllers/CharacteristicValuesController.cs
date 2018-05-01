using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class CharacteristicValuesController : Controller
    {
        DNSContext db;

        public CharacteristicValuesController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            var item = db.CharacteristicValues.Find(id ?? "");
            ViewBag.CardEvent = e;
            ViewBag.CharacteristicVariants = new SelectList(db.Characteristics.ToList(), "Id", "Name", item);

            return PartialView("Card", item ?? new CharacteristicValue());
        }

        [HttpPost]
        public IActionResult Card(CharacteristicValue model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.CharacteristicValues.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "CharacteristicValues" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}