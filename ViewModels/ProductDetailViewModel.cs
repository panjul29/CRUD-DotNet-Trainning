using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductID { get; set; }
        public string ProductDescription { get; set; }
        public string UnitProfit { get; set; }

    }
}