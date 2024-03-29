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
    public class OrdersController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();

        // GET: Orders
        public ActionResult Orders(int? i, DateTime? orderDate = null)
        {
            db.Configuration.ProxyCreationEnabled = false; //save memory
            OrdersVM ordVM = new OrdersVM();
            //I need product name but for some reason, product name only comes when I have a vm with a static products list
            ordVM.allOrders = db.orders.Where(oo => oo.order_date == orderDate || orderDate == null).Include(oo => oo.order_items).OrderBy(oo => oo.order_id).ToPagedList(i ?? 1, 10);
            OrdersVM.allProducts = db.products.ToList();

            //display search text only if a search happened
            if (orderDate != null)
            { ViewBag.SearchDate = orderDate.ToString(); }

            return View(ordVM);
        }
    }
}