using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class BranchPromotionsController : Controller
    {
        DNSContext db;

        public BranchPromotionsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            var item = db.BranchPromotions.Find(id ?? "");

            var BranchVariants = db.Branchs.ToList();
            BranchVariants.Insert(0, new Branch() { Id = "", Name = "" });

            var PromotionVariants = db.Promotions.ToList();
            PromotionVariants.Insert(0, new Promotion() { Id = "", Name = "" });

            ViewBag.CardEvent = e;

            ViewBag.BranchVariants = new SelectList(BranchVariants, "Id", "Name", item);
            ViewBag.PromotionVariants = new SelectList(PromotionVariants, "Id", "Name", item);

            return PartialView("Card", item ?? new BranchPromotion());
        }

        [HttpPost]
        public IActionResult Card(BranchPromotion model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.BranchPromotions.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "BranchPromotions" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
