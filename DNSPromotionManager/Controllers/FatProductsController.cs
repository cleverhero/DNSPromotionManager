using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.ViewModels;
using DNSPromotionManager.App_Code;
using Microsoft.AspNetCore.Http;
using DNSPromotionManager.Queries;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DNSPromotionManager.Controllers
{
    public class FatProductsController : Controller
    {
        DNSContext db;
        Filter Filter;

        public FatProductsController(DNSContext context)
        {
            db = context;
            Filter = new Filter()
            {
                MaxPrice = 1000
            };
        }

        public IActionResult Index(String JSONFilter)
        {
            if (JSONFilter != null) Filter = JsonConvert.DeserializeObject<Filter>(JSONFilter); ;

            var productList = new ProductListModel
            {
                FatProducts = ProductQueries.FatProducts(db, Filter)
            };

            var characteristics = CharacteristicQueries.Characteristics(db);
            foreach(var characteristic in characteristics)
            {
                List<SelectedVariant> variants = new List<SelectedVariant>();
                var dbVariants = CharacteristicQueries.CharacteristicValues(db, characteristic.Id);
                foreach (var variant in dbVariants)
                    variants.Add(new SelectedVariant(variant.Id, variant.Name));

                var characteristicModel = new CharacteristicModel(characteristic, variants);
                productList.Filter.Characteristics.Add(characteristicModel);
            }

            ViewData["Filter"] = Filter;

            return View(productList);
        }

        [HttpPost]
        public IActionResult Products(String JSONFilter)
        {
            return RedirectToAction("Index", new { JSONFilter });
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
