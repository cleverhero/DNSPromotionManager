using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;
using DNSPromotionManager.ViewModels;

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
            var products = (from p in db.Products
                            join kind in db.Kinds on p.KindId equals kind.Id
                            join r in db.Residues on p.Id equals r.ProductId into rs
                            join price in db.ProductPrices on p.Id equals price.ProductId into prices
                            join c in db.ProductCharacteristics on p.Id equals c.ProductId into cs
                            select new ProductViewModel
                            {
                                Product = new Product
                                {
                                    Id   = p.Id,
                                    Name = p.Name,
                                    Code = p.Code,
                                    Parent = p.Parent,
                                    Kind = kind
                                },
                                Residue = rs.FirstOrDefault().Value,
                                Price = prices.FirstOrDefault().Value,
                                Characteristics = cs.ToList()
                           }
                           ).ToList();

            return View(products);
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
