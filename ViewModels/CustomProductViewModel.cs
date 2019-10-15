using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;
using Northwind.ViewModels.Items;
using Northwind.ViewModels.Services;
using AutoMapper;

namespace Northwind.ViewModels
{
    public class CustomProductViewModel : ProductViewModel
    {
        public CustomProductViewModel()
        {

        }

        public CustomProductViewModel(Product product)
        {
            ProductID = product.ProductID;
            ProductName = product.ProductName;
            SupplierID = product.SupplierID;
            CategoryID = product.CategoryID;
            QuantityPerUnit = product.QuantityPerUnit;
            UnitPrice = product.UnitPrice;
            UnitsInStock = product.UnitsInStock;
            UnitsOnOrder = product.UnitsOnOrder;
            ReorderLevel = product.ReorderLevel;
            Discontinued = product.Discontinued;
            ProductType = product.ProductType;

            if (ProductType != null)
            {
                switch (ProductType)
                {
                    case "FoodAndBeverageItems":
                        FoodsAndBeverageItems food = new FoodsAndBeverageItems(product);
                        ProductDetail = food.fromItemsToDict();
                        break;
                    case "GarmentItems":
                        GarmentItems garment = new GarmentItems(product);
                        ProductDetail = garment.fromItemsToDict();
                        break;
                    case "MaterialItems":
                        MaterialItems materi = new MaterialItems(product);
                        ProductDetail = materi.fromItemsToDict();
                        break;
                    case "TransportationServices":
                        TransportationServices trans = new TransportationServices(product);
                        ProductDetail = trans.fromServicesToDict();
                        break;
                    case "TelecommunicationServices":
                        TelecommunicationServices telcom = new TelecommunicationServices(product);
                        ProductDetail = telcom.fromServicesToDict();
                        break;
                    default:
                        ProductDetail = null;
                        break;
                }
            }
            else
            {
                ProductDetail = null;
            }
        }

        public Product convertToProduct(string condition = "", int? userDemand = 0, decimal? duration = 0)
        {
            var description = "";
            decimal? price = null;
            var config = new MapperConfiguration(cfg => { });
            var mapper = new Mapper(config);

            if (this.ProductType.Contains("FoodAndBeverageItems"))
            {
                FoodsAndBeverageItems foods = mapper.Map<FoodsAndBeverageItems>(this.ProductDetail);
                description = foods.convertItemsToString();
                price = foods.calculateProductUnitPrice();

            }
            else if (this.ProductType.Contains("MaterialItems"))
            {
                MaterialItems materials = mapper.Map<MaterialItems>(this.ProductDetail);
                description = materials.convertItemsToString();
                price = materials.calculateProductUnitPrice();
            }
            else if (this.ProductType.Contains("GarmentItems"))
            {
                GarmentItems garments = mapper.Map<GarmentItems>(this.ProductDetail);
                description = garments.convertItemsToString();
                price = garments.calculateProductUnitPrice();
            }
            else if (this.ProductType.Contains("TransportationServices"))
            {
                TransportationServices transportations = mapper.Map<TransportationServices>(this.ProductDetail);
                description = transportations.convertServicesToString();
                price = transportations.calculateProductUnitPrice(condition, userDemand, 0);
            }
            else if (this.ProductType.Contains("TelecommunicationServices"))
            {
                TelecommunicationServices telecommunications = mapper.Map<TelecommunicationServices>(this.ProductDetail);
                description = telecommunications.convertServicesToString();
                price = telecommunications.calculateProductUnitPrice("", 0, duration);
            }

            return new Product()
            {
                ProductID = this.ProductID,
                ProductName = this.ProductName,
                SupplierID = this.SupplierID,
                CategoryID = this.CategoryID,
                QuantityPerUnit = this.QuantityPerUnit,
                UnitPrice = price,
                UnitsInStock = this.UnitsInStock,
                UnitsOnOrder = this.UnitsOnOrder,
                ReorderLevel = this.ReorderLevel,
                Discontinued = this.Discontinued,
                ProductType = this.ProductType,
                ProductDetail = description,
            };
        }

        public Dictionary<string, object> FinalResult(List<CustomProductViewModel> listObject = null, string msg = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("Message", msg);
            result.Add("Data", listObject);
            return (result);
        }
    }
}