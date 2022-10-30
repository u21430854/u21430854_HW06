using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using u21430854_HW06.Models;
using PagedList.Mvc;
using PagedList;

namespace u21430854_HW06.ViewModels
{
    public class OrdersVM
    {
        public IPagedList<order> allOrders { get; set; }
        public static List<product> allProducts { get; set; }

        //calc grand total per order
        public decimal CalculateGrandTotal(int orderId)
        {
            decimal grandTotal = 0;

            foreach (order_items orditem in allOrders.FirstOrDefault(oo => oo.order_id == orderId).order_items)
            {
                grandTotal += orditem.quantity * orditem.list_price;
            }

            return grandTotal;
        }
    }
}