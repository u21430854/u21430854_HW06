using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using u21430854_HW06.Models;
using PagedList.Mvc;
using PagedList;

namespace u21430854_HW06.ViewModels
{
    public class ProductsVM
    {
        public IPagedList<product> products { get; set; }
        public static List<brand> allBrands { get; set; }
        public static List<category> allCategories { get; set; }
    }
}