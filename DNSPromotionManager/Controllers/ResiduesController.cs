using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class ResiduesController : Controller
    {
        DNSContext db;

        public ResiduesController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Card(String id, TableItemEvent e)
        {
            var item = db.Residues.Find(id ?? "");

            var BranchVariants = db.Branchs.ToList();
            BranchVariants.Insert(0, new Branch() { Id = "", Name = "" });

            var ProductVariants = db.Products.ToList();
            ProductVariants.Insert(0, new Product() { Id = "", Name = "" });

            ViewBag.CardEvent = e;

            ViewBag.BranchVariants = new SelectList(BranchVariants, "Id", "Name", item);
            ViewBag.ProductVariants = new SelectList(ProductVariants, "Id", "Name", item);

            return PartialView("Card", item ?? new Residue());
        }

        [HttpPost]
        public IActionResult Card(Residue model, TableItemEvent e)
        {
            if (ModelState.IsValid || e == TableItemEvent.Delete)
            {
                db.Residues.Change(model, e);
                db.SaveChanges();
                return RedirectToAction("Tables", "Home", new { name = "Residues" });
            }
            else return Content($"Поля не удовлетворяют условиям");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
