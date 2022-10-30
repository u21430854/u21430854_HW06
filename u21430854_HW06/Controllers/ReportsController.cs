using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21430854_HW06.Models;
using System.Data;
using System.Data.Entity;

namespace u21430854_HW06.Controllers
{
    public class ReportsController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();

        // GET: Reports
        public ActionResult Report()
        {
            db.Configuration.ProxyCreationEnabled = false; //save memory
            //category id for mountain bikes is 6
            List<order_items> mountainBikeSales = db.order_items.Where(oo => oo.product.category_id == 6)
                                                                .Include(oo => oo.order).ToList();
            //probably not the best way to order by month but no time to google
            int jan = 0;
            int feb = 0;
            int mar = 0;
            int apr = 0;
            int may = 0;
            int jun = 0;
            int jul = 0;
            int aug = 0;
            int sep = 0;
            int oct = 0;
            int nov = 0;
            int dec = 0;

            //I know there'll be 12 months so:
            foreach (order_items item in mountainBikeSales)
            {
                switch (item.order.order_date.Month)
                {
                    case 1:
                        jan += item.quantity;
                        break;
                    case 2:
                        feb += item.quantity;
                        break;
                    case 3:
                        mar += item.quantity;
                        break;
                    case 4:
                        apr += item.quantity;
                        break;
                    case 5:
                        may += item.quantity;
                        break;
                    case 6:
                        jun += item.quantity;
                        break;
                    case 7:
                        jul += item.quantity;
                        break;
                    case 8:
                        aug += item.quantity;
                        break;
                    case 9:
                        sep += item.quantity;
                        break;
                    case 10:
                        oct += item.quantity;
                        break;
                    case 11:
                        nov += item.quantity;
                        break;
                    case 12:
                        dec += item.quantity;
                        break;
                    default:
                        break;
                }
            }

            List<int> salesPerMonth = new List<int> { jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec };

            return View(salesPerMonth);
        }
    }
}