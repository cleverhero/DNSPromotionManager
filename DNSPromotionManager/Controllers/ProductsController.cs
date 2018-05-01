using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class ProductsController : Controller
    {
        DNSContext db;

        public ProductsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            var item = db.Products.Find(id ?? "");

            var ParentVariants = db.Products.Where(i => i.Id != id).ToList();
            ParentVariants.Insert(0, new Product() { Id = "", Name = "" });

            ViewBag.CardEvent = e;

            ViewBag.KindVariants = new SelectList(db.Kinds.ToList(), "Id", "Name", item);
            ViewBag.ParentVariants = new SelectList(ParentVariants, "Id", "Name", item);

            return PartialView("Card", item ?? new Product());
        }

        [HttpPost]
        public IActionResult Card(Product model, TableItemEvent e)
        {
            if (e == TableItemEvent.Delete)
            {
                var items = db.Products.Where(c => c.ParentId == model.Id).ToList();
                foreach (var item in items)
                {
                    item.ParentId = null;
                    db.Products.Update(item);
                }

                db.SaveChanges();
            }


            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.Products.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "Products" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
