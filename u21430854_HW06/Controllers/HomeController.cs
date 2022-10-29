using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21430854_HW06.Models;
using System.Data;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;

namespace u21430854_HW06.Controllers
{
    public class HomeController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();

        public ActionResult Products(string prodName, int? i)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!String.IsNullOrEmpty(prodName))
            { ViewBag.SearchTerm = prodName; }

            var products = db.products.Where(pp => pp.product_name.Contains(prodName) || prodName == null).Include(pp => pp.brand).Include(pp => pp.category).ToList().ToPagedList(i ?? 1, 10);
            return View(products);
        }
    }
}