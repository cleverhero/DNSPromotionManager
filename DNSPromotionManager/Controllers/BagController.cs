using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNSPromotionManager.Models;
using DNSPromotionManager.App_Code;
using Microsoft.AspNetCore.Http;
using DNSPromotionManager.Queries;

namespace DNSPromotionManager.Controllers
{
    public class BagController : Controller
    {
        DNSContext db;

        public BagController(DNSContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var products = ProductQueries.FatProducts(db, GetProducts());
            return View(products);
        }

        public bool AddItemInBag(String ItemId)
        {
            if (User.Identity.IsAuthenticated)
                AppendForUser(ItemId);         
            else
                return true;

            return false;
        }

        public bool DeleteItemFromBag(String ItemId)
        {
            if (User.Identity.IsAuthenticated)
                RemoveForUser(ItemId);
            else
                return true;

            return false;
        }

        public List<String> GetProducts()
        {
            if (User.Identity.IsAuthenticated)
                return GetProductsForUser();

            if (Request.Cookies["Bag"] == null)
                return new List<String>();

            return GetProductsForGuest();
        }

        private List<String> GetProductsForGuest()
        {
            var str = Request.Cookies["Bag"];
            var list = str.Split("#").ToList();
            return list;
        }

        private List<String> GetProductsForUser()
        {
            return null;
        }
        private void AppendForUser(String id)
        {
        }

        private void RemoveForUser(String id)
        {
        }


    }
}
