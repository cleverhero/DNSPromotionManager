using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;
using DNSPromotionManager.ViewModels;
using Microsoft.AspNetCore.Http;
using DNSPromotionManager.Queries;

namespace DNSPromotionManager.Controllers
{
    public class FatProductsController : Controller
    {
        DNSContext db;

        public FatProductsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(ProductQueries.FatProducts(db));
        }

        public IActionResult Card(FatProduct model, TableItemEvent e)
        {
            return PartialView("Card", model ?? new FatProduct());
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
