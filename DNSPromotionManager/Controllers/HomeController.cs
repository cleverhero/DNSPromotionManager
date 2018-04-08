using System.Diagnostics;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DNSPromotionManager.Models;
using Microsoft.AspNetCore.Mvc;
using DNSPromotionManager.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DNSPromotionManager.Controllers
{
    public class HomeController : Controller
    {
        DNSContext db;

        public HomeController(DNSContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

//--------------------------------------------------------------------------
//------------------------------PRODUCTS------------------------------------
//--------------------------------------------------------------------------
        public IActionResult Products()
        {
            
            return View("Table", new TableViewModel
            {
                TableName = new KeyValuePair<string, string>("Products", Global.TableNames["Products"]),
                ColumnNames = Models.Product.GetColumnNames(),
                Table = db.GetProducts()
            });
        }
        public IActionResult ProductsCard(String id, TableItemEvent e)
        {
            List<Product> itemList = db.Products.Where(i => i.Id == id).ToList();

            var KindVariants = db.Kinds.ToList();
            var ParentVariants = db.Products.Select(i => new { Id = i.Id, Name = i.Name }).ToList();
            ParentVariants.Insert(0, new { Id = "", Name = "" });
            
            ViewBag.CardEvent = e;

            if (e == TableItemEvent.Create)
            {
                ViewBag.ParentVariants = new SelectList(ParentVariants, "Id", "Name");
                ViewBag.KindVariants = new SelectList(KindVariants, "Id", "Name");

                return PartialView("ProductsCard");
            }
            else
            {
                var SelectedKind = itemList[0].KindId ?? "";
                ViewBag.KindVariants = new SelectList(KindVariants, "Id", "Name", SelectedKind);

                var SelectedParent = itemList[0].ParentId ?? "";
                ViewBag.ParentVariants = new SelectList(ParentVariants, "Id", "Name", SelectedParent);

                return PartialView("ProductsCard", itemList[0]);
            }


        }
        [HttpPost]
        public IActionResult ProductsCard(Product model, TableItemEvent e)
        {
            if (e == TableItemEvent.Delete)
            {
                db.Products.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Products");
            }

            if (ModelState.IsValid)
            {
                switch (e) {
                    case TableItemEvent.Create:
                        db.Products.Add(model);
                        break;
                    case TableItemEvent.Edit:
                        db.Products.Update(model);
                        break;
                }
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                return Content($"Поля не удовлетворяют условиям");
            }
        }


//--------------------------------------------------------------------------
//------------------------------KINDS---------------------------------------
//--------------------------------------------------------------------------
        public IActionResult Kinds()
        {
            return View("Table", new TableViewModel
            {
                TableName = new KeyValuePair<string, string>("Kinds", Global.TableNames["Kinds"]),
                ColumnNames = Models.Kind.GetColumnNames(),
                Table = db.GetKinds()
            });
        }
        public IActionResult KindsCard(String id, TableItemEvent e)
        {
            List<Kind> itemList = db.Kinds.Where(i => i.Id == id).ToList();

            ViewBag.CardEvent = e;;

            if (e == TableItemEvent.Create)
                return PartialView("KindsCard");
            else
                return PartialView("KindsCard", itemList[0]);
        }
        [HttpPost]
        public IActionResult KindsCard(Kind model, TableItemEvent e)
        {
            if (e == TableItemEvent.Delete)
            {
                db.Kinds.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Products");
            }

            if (ModelState.IsValid)
            {
                switch (e)
                {
                    case TableItemEvent.Create:
                        db.Kinds.Add(model);
                        break;
                    case TableItemEvent.Edit:
                        db.Kinds.Update(model);
                        break;
                }
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                return Content($"Поля не удовлетворяют условиям");
            }
        }


//--------------------------------------------------------------------------
//------------------------------BRANCHS-------------------------------------
//--------------------------------------------------------------------------
        public IActionResult Branchs()
        {
            return View("Table", new TableViewModel
            {
                TableName = new KeyValuePair<string, string>("Branchs", Global.TableNames["Branchs"]),
                ColumnNames = Models.Branch.GetColumnNames(),
                Table = db.GetBranchs()
            });
        }
        public IActionResult BranchsCard(String id, TableItemEvent e)
        {
            List<Branch> itemList = db.Branchs.Where(i => i.Id == id).ToList();

            ViewBag.CardEvent = e; ;

            if (e == TableItemEvent.Create)
                return PartialView("BranchsCard");
            else
                return PartialView("BranchsCard", itemList[0]);
        }
        [HttpPost]
        public IActionResult BranchsCard(Branch model, TableItemEvent e)
        {
            if (ModelState.IsValid)
            {
                switch (e)
                {
                    case TableItemEvent.Create:
                        db.Branchs.Add(model);
                        break;
                    case TableItemEvent.Edit:
                        db.Branchs.Update(model);
                        break;
                    case TableItemEvent.Delete:
                        db.Branchs.Remove(model);
                        break;
                }
                db.SaveChanges();
                return RedirectToAction("Branchs");
            }
            else
            {
                return Content($"Поля не удовлетворяют условиям");
            }
        }

//--------------------------------------------------------------------------
//----------------------------PROMOTIONS------------------------------------
//--------------------------------------------------------------------------
        public IActionResult Promotions()
        {
            return View("Table", new TableViewModel
            {
                TableName = new KeyValuePair<string, string>("Promotions", Global.TableNames["Promotions"]),
                ColumnNames = Models.Promotion.GetColumnNames(),
                Table = db.GetPromotions()
            });
        }
        public IActionResult PromotionsCard(String id, TableItemEvent e)
        {
            List<Promotion> itemList = db.Promotions.Where(i => i.Id == id).ToList();

            ViewBag.CardEvent = e;

            if (e == TableItemEvent.Create)
                return PartialView("PromotionsCard");
            else
                return PartialView("PromotionsCard", itemList[0]);
        }
        [HttpPost]
        public IActionResult PromotionsCard(Promotion model, TableItemEvent e)
        {
            if (e == TableItemEvent.Delete)
            {
                db.Promotions.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Promotions");
            }

            if (ModelState.IsValid)
            {
                switch (e)
                {
                    case TableItemEvent.Create:
                        db.Promotions.Add(model);
                        break;
                    case TableItemEvent.Edit:
                        db.Promotions.Update(model);
                        break;
                }
                db.SaveChanges();
                return RedirectToAction("Promotions");
            }
            else
            {
                return Content($"Поля не удовлетворяют условиям");
            }
        }

//--------------------------------------------------------------------------
//------------------------------Characteristics-----------------------------
//--------------------------------------------------------------------------
        public IActionResult Characteristics()
        {
            return View("Table", new TableViewModel
            {
                TableName = new KeyValuePair<string, string>("Characteristics", Global.TableNames["Characteristics"]),
                ColumnNames = Models.Characteristic.GetColumnNames(),
                Table = db.GetCharacteristics()
            });
        }
        public IActionResult CharacteristicsCard(String id, TableItemEvent e)
        {
            List<Characteristic> itemList = db.Characteristics.Where(i => i.Id == id).ToList();

            ViewBag.CardEvent = e; ;

            if (e == TableItemEvent.Create)
                return PartialView("CharacteristicsCard");
            else
                return PartialView("CharacteristicsCard", itemList[0]);
        }
        [HttpPost]
        public IActionResult CharacteristicsCard(Characteristic model, TableItemEvent e)
        {
            if (e == TableItemEvent.Delete)
            {
                db.Characteristics.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Characteristics");
            }

            if (ModelState.IsValid)
            {
                switch (e)
                {
                    case TableItemEvent.Create:
                        db.Characteristics.Add(model);
                        break;
                    case TableItemEvent.Edit:
                        db.Characteristics.Update(model);
                        break;
                }
                db.SaveChanges();
                return RedirectToAction("Characteristics");
            }
            else
            {
                return Content($"Поля не удовлетворяют условиям");
            }
        }


//--------------------------------------------------------------------------
//------------------------------CharacteristicValues------------------------
//--------------------------------------------------------------------------
        public IActionResult CharacteristicValues()
        {
            return View("Table", new TableViewModel
            {
                TableName = new KeyValuePair<string, string>("CharacteristicValues", Global.TableNames["CharacteristicValues"]),
                ColumnNames = Models.CharacteristicValue.GetColumnNames(),
                Table = db.GetCharacteristicValues()
            });
        }
        public IActionResult CharacteristicValuesCard(String id, TableItemEvent e)
        {
            List<CharacteristicValue> itemList = db.CharacteristicValues.Where(i => i.Id == id).ToList();

            var CharacteristicVariants = db.Characteristics.ToList();
            

            ViewBag.CardEvent = e;

            if (e == TableItemEvent.Create)
            {
                ViewBag.CharacteristicVariants = new SelectList(CharacteristicVariants, "Id", "Name");
                return PartialView("CharacteristicValuesCard");
            }
            else
            {
                var SelectedCharacteristic = itemList[0].CharacteristicId ?? "";
                ViewBag.CharacteristicVariants = new SelectList(CharacteristicVariants, "Id", "Name", SelectedCharacteristic);

                return PartialView("CharacteristicValuesCard", itemList[0]);
            }
                
        }
        [HttpPost]
        public IActionResult CharacteristicValuesCard(CharacteristicValue model, TableItemEvent e)
        {
            if (e == TableItemEvent.Delete)
            {
                db.CharacteristicValues.Remove(model);
                db.SaveChanges();
                return RedirectToAction("CharacteristicValues");
            }

            if (ModelState.IsValid)
            {
                switch (e)
                {
                    case TableItemEvent.Create:
                        db.CharacteristicValues.Add(model);
                        break;
                    case TableItemEvent.Edit:
                        db.CharacteristicValues.Update(model);
                        break;
                }
                db.SaveChanges();
                return RedirectToAction("CharacteristicValues");
            }
            else
            {
                return Content($"Поля не удовлетворяют условиям");
            }
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
