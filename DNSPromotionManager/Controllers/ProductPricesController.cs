using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class ProductPricesController : Controller
    {
        DNSContext db;

        public ProductPricesController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            var item = db.ProductPrices.Find(id ?? "");

            var BranchVariants = db.Branchs.ToList();
            var ProductVariants = db.Products.ToList();
            
            ViewBag.CardEvent = e;

            ViewBag.BranchVariants = new SelectList(BranchVariants, "Id", "Name", item);
            ViewBag.ProductVariants = new SelectList(ProductVariants, "Id", "Name", item);

            return PartialView("Card", item ?? new ProductPrice());
        }

        [HttpPost]
        public IActionResult Card(ProductPrice model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.ProductPrices.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "ProductPrices" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
