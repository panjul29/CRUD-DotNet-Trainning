using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class CustomOrderViewModel
    {
        public int? ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(40)]
        public string SupplierName { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public decimal? Total { get; set; }

        public CustomOrderViewModel()
        {

        }

        public CustomOrderViewModel (Order_Detail entityOrD)
        {
            ProductID = entityOrD.Product.ProductID;
            ProductName = entityOrD.Product.ProductName;
            CategoryName = entityOrD.Product.Category.CategoryName;
            SupplierName = entityOrD.Product.Supplier.CompanyName;
            UnitPrice = entityOrD.UnitPrice;
            Quantity = entityOrD.Quantity;
            Total = (UnitPrice * Quantity) - ((UnitPrice * Quantity) * (decimal)entityOrD.Discount);
        }

    }
}