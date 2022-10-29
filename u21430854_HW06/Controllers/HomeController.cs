using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21430854_HW06.Models;
using u21430854_HW06.ViewModels;
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
            db.Configuration.ProxyCreationEnabled = false; //save memory

            ProductsVM prodVM = new ProductsVM();

            ProductsVM.allBrands = db.brands.ToList();
            ProductsVM.allCategories = db.categories.ToList();
            prodVM.products = db.products.Where(pp => pp.product_name.Contains(prodName) || prodName == null).Include(pp => pp.brand).Include(pp => pp.category).ToList().ToPagedList(i ?? 1, 10);

            //display search text only if a search happened
            if (!String.IsNullOrEmpty(prodName))
            { ViewBag.SearchTerm = prodName; }

            return View(prodVM);
        }

        public JsonResult CreateProduct(product prod)
        {
            db.products.Add(prod);
            db.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}