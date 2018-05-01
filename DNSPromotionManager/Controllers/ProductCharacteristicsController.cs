using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;

namespace DNSPromotionManager.Controllers
{
    public class ProductCharacteristicsController : Controller
    {
        DNSContext db;

        public ProductCharacteristicsController(DNSContext context)
        {
            db = context;
        }

        //public IActionResult Card(String id, TableItemEvent e)
        //{
        //    var item = db.ProductCharacteristics.Find(id ?? "");

        //    var ProductVariants = db.Products.ToList();
        //    var CharacteristicVariants = db.Characteristics.ToList();
        //    var CharacteristicValueVariants = db.CharacteristicValues.ToList();
            
        //    ViewBag.CardEvent = e;

        //    ViewBag.ProductVariants = 
        //        new SelectList(ProductVariants, "Id", "Name", item);
        //    ViewBag.CharacteristicVariants = 
        //        new SelectList(CharacteristicVariants, "Id", "Name", item);
        //    ViewBag.CharacteristicValueVariants = 
        //        new SelectList(CharacteristicValueVariants, "Id", "Name", item);

        //    return PartialView("Card", item ?? new ProductCharacteristic());
        //}

        //[HttpPost]
        //public IActionResult Card(ProductCharacteristic model, TableItemEvent e)
        //{
        //    if (ModelState.IsValid || e == TableItemEvent.Delete)
        //    {
        //        db.ProductCharacteristics.Change(model, e);
        //        db.SaveChanges();
        //        return RedirectToAction("Tables", "Home", new { name = "ProductCharacteristics" });
        //    }
        //    else return Content($"Поля не удовлетворяют условиям");
        //}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
