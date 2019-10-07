using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class CustomCustomerViewModel
    {
        [StringLength(5)]
        public string CustomerID { get; set; }

        [StringLength(30)]
        public string ContactName { get; set; }

        public IList<CustomOrderViewModel2> Customerlist { get; set; }

        public int TotalOrders { get; set; }

        public decimal? GrandTotal { get; set; }

        public CustomCustomerViewModel()
        {

        }

        public CustomCustomerViewModel(Customer entityCus)
        {
            var temp = entityCus.Orders.ToList().Select(dt => new CustomOrderViewModel2(dt)).ToList();
            CustomerID = entityCus.CustomerID;
            ContactName = entityCus.ContactName;
            Customerlist = temp;
            TotalOrders = temp.Select(dt => dt.OrderID).Count();
            GrandTotal = temp.Sum(data => data.GrandTotal);
        }
    }
}