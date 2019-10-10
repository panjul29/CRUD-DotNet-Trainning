using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;
using AutoMapper;

namespace Northwind.ViewModels
{
    public class CustomProductViewModel : ProductViewModel
    {
        public FoodsAndBeverageItems FoodAndBeverages{get;set;}

        public MaterialItems Materials { get; set; }

        public GarmentItems Garments { get; set; }

        public TransportationServices Transportations { get; set; }


        public CustomProductViewModel()
        {

        }

        //public CustomProductViewModel(Product product)
        //{
        //    ProductID = product.ProductID;
        //    ProductName = product.ProductName;
        //    SupplierID = product.SupplierID;
        //    CategoryID = product.CategoryID;
        //    QuantityPerUnit = product.QuantityPerUnit;
        //    UnitPrice = product.UnitPrice;
        //    UnitsInStock = product.UnitsInStock;
        //    UnitsOnOrder = product.UnitsOnOrder;
        //    ReorderLevel = product.ReorderLevel;
        //    Discontinued = product.Discontinued;
        //    ProductType = product.ProductType;
        //    ProductDetail = null ;
        //}

        public Product convertToProduct()
        {
            var description = "";
            
            if (this.ProductType.Contains("FoodAndBeverageItems"))
            {
                description = FoodAndBeverages.convertToString();
            }
            else if (this.ProductType.Contains("MaterialItems"))
            {
                description = Materials.convertToString();
            }
            else if (this.ProductType.Contains("GarmentItems"))
            {
                description = Garments.convertToString();
            }
            else if (this.ProductType.Contains("TransportationServices"))
            {
                description = Transportations.convertToString();
            }

            return new Product()
            {
                ProductID = this.ProductID,
                ProductName = this.ProductName,
                SupplierID = this.SupplierID,
                CategoryID = this.CategoryID,
                QuantityPerUnit = this.QuantityPerUnit,
                UnitPrice = this.UnitPrice,
                UnitsInStock = this.UnitsInStock,
                UnitsOnOrder = this.UnitsOnOrder,
                ReorderLevel = this.ReorderLevel,
                Discontinued = this.Discontinued,
                ProductType = this.ProductType,
                ProductDetail = description,
            };
        }

        public Dictionary<string, object> FinalResult(List<CustomProductViewModel> listObject, string msg)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("Message", msg);
            result.Add("Data", listObject);
            return (result);
        }
    }
}