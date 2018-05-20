using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

        public FatProductsController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var productList = new ProductListModel()
            {
                FatProducts = ProductQueries.FatProducts(db, null as Filter)
            };

            FilterData Data = new FilterData()
            {
                Characteristics = CharacteristicQueries.Characteristics(db),
                Kinds           = KindQueries.Kinds(db)
            };

            ViewData["FilterData"] = Data;
            ViewData["Bag"] = GetBag();

            return View(productList);
        }

        [HttpPost]
        public IActionResult Products(String JSONFilter)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(JSONFilter);
            var products = ProductQueries.FatProducts(db, filter);

            ViewData["Bag"] = GetBag();

            return PartialView("ProductTable", products);
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

        private List<String> GetBag()
        {
            if (User.Identity.IsAuthenticated)
                return GetBagForUser();

            if (Request.Cookies["Bag"] == null)
                return new List<String>();

            return GetBagForGuest();
        }

        private List<String> GetBagForGuest()
        {
            var str = Request.Cookies["Bag"];
            var list = str.Split("#").ToList();
            return list;
        }

        private List<String> GetBagForUser()
        {
            return null;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
