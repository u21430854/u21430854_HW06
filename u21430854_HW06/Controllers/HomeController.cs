﻿using System;
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

        //------------------------------DELETE PRODUCT------------------------------------------------------------------------
        //show product that is to be deleted
        public JsonResult GetProductToDelete(int prodId)
        {
            db.Configuration.ProxyCreationEnabled = false; //save memory
            product prod = db.products.Include(pp => pp.brand)
                                      .Include(pp => pp.category)
                                      .FirstOrDefault(pp => pp.product_id == prodId);
            
            //see read product for explanation of ProductDetails model
            ProductDetails productDetails = new ProductDetails()
            {
                prodId = prodId,
                prodName = prod.product_name,
                prodYear = prod.model_year,
                prodPrice = prod.list_price,
                brandName = prod.brand.brand_name,
                catName = prod.category.category_name
            };

            return Json(productDetails, JsonRequestBehavior.AllowGet);
        }

        //delete product
        public JsonResult DeleteProduct(int prodId)
        {
            db.products.Remove(db.products.FirstOrDefault(pp => pp.product_id == prodId));
            db.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }

        //------------------------------UPDATE PRODUCT------------------------------------------------------------------------
        //get product that must be updated; I'm sure there's a better way to combine the GetProduct function and this function
        //maybe if I had used partial VMs, but that's for another day
        public JsonResult GetProductToUpdate(int prodId)
        {
            db.Configuration.ProxyCreationEnabled = false; //save memory
            product updateProd = db.products.FirstOrDefault(pp => pp.product_id == prodId);

            return Json(updateProd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProduct(product updated)
        {
            db.Configuration.ProxyCreationEnabled = false; //save memory
            product toUpdate = db.products.FirstOrDefault(pp => pp.product_id == updated.product_id);
            if (toUpdate != null)
            {
                toUpdate.product_name = updated.product_name;
                toUpdate.brand_id = updated.brand_id;
                toUpdate.category_id = updated.category_id;
                toUpdate.model_year = updated.model_year;
                toUpdate.list_price = updated.list_price;
                db.SaveChanges();
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

        //------------------------------READ PRODUCT------------------------------------------------------------------------
        //display product details
        public JsonResult GetProduct(int prodId)
        {
            db.Configuration.ProxyCreationEnabled = false; //save memory
            product prod = db.products.Include(pp => pp.brand)
                                      .Include(pp => pp.category)
                                      .FirstOrDefault(pp => pp.product_id == prodId);

            //If I say, .Include(pp => pp.stocks/brand/category), I get a circular reference error so
            //I've tried multiple other ways, I still get the error so here is my workaround #5:
            //create a model that only stores the info I need and send that back to the view
            ProductDetails productDetails = new ProductDetails()
            {
                prodId = prodId,
                prodName = prod.product_name,
                prodYear = prod.model_year,
                prodPrice = prod.list_price,
                brandName = prod.brand.brand_name,
                catName = prod.category.category_name
            };

            List<stock> foundStocks = db.stocks.Where(ss => ss.product_id == prodId).ToList();
            foreach (stock st in foundStocks)
            {
                productDetails.prodStock.Add(new stock()
                {
                    quantity = st.quantity
                });
            }
                        
            return Json(productDetails, JsonRequestBehavior.AllowGet);
        }

        //------------------------------CREATE PRODUCT------------------------------------------------------------------------
        //for creating new product
        public JsonResult CreateProduct(product prod)
        {
            db.products.Add(prod);
            db.SaveChanges();

            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}