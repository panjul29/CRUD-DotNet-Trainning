using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class CustomOrderViewModel2
    {
        public int? OrderID { get; set; }

        public IList<CustomOrderViewModel> ProductList { get; set; }

        public decimal? GrandTotal { get; set; }

        public CustomOrderViewModel2()
        {

        }

        public CustomOrderViewModel2(Order entityOr)
        {
            var temp = entityOr.Order_Details.ToList().Select(dt => new CustomOrderViewModel(dt)).ToList();
            OrderID = entityOr.OrderID;
            ProductList = temp;
            GrandTotal = temp.Sum(data => data.Total);
        }

    }
}