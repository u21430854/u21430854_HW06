using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21430854_HW06.Models
{
    public class ProductDetails
    {
        public int prodId { get; set; }
        public string prodName { get; set; }
        public int prodYear { get; set; }
        public decimal prodPrice { get; set; }
        public string brandName { get; set; }
        public string catName { get; set; }
        public List<stock> prodStock = new List<stock>();
        
    }
}