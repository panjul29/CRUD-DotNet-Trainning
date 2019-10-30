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

        public CustomProductViewModel(Product product, string condition = null, int? userDemand = null, decimal? duration = null)
        {
            ProductID = product.ProductID;
            ProductName = product.ProductName;
            SupplierID = product.SupplierID;
            CategoryID = product.CategoryID;
            QuantityPerUnit = product.QuantityPerUnit;
            ProductType = product.ProductType;
            ProductDetail = convertToList(product);
            UnitPrice = updateToProduct(condition, userDemand, duration);
            UnitsInStock = product.UnitsInStock;
            UnitsOnOrder = product.UnitsOnOrder;
            ReorderLevel = product.ReorderLevel;
            Discontinued = product.Discontinued;
            
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
            ProductDetail = convertToList(product);
        }

        public dynamic convertToList( Product product)
        {
            if (product.ProductType != null)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                switch (product.ProductType)
                {
                    case "FoodAndBeverageItems":
                        FoodsAndBeverageItems food = new FoodsAndBeverageItems(product);
                        ProductDetail = food.fromItemsToDict();
                        result = ProductDetail;
                        break;
                    case "GarmentItems":
                        GarmentItems garment = new GarmentItems(product);
                        ProductDetail = garment.fromItemsToDict();
                        result = ProductDetail;
                        break;
                    case "MaterialItems":
                        MaterialItems materi = new MaterialItems(product);
                        ProductDetail = materi.fromItemsToDict();
                        result = ProductDetail;
                        break;
                    case "TransportationServices":
                        TransportationServices trans = new TransportationServices(product);
                        ProductDetail = trans.fromServicesToDict();
                        result = ProductDetail;
                        break;
                    case "TelecommunicationServices":
                        TelecommunicationServices telcom = new TelecommunicationServices(product);
                        ProductDetail = telcom.fromServicesToDict();
                        result = ProductDetail;
                        break;
                    default:
                        ProductDetail = null;
                        result = ProductDetail;
                        break;
                }
                return result;
            }
            else
            {
                ProductDetail = null;
                return ProductDetail;
            }
        }

        public dynamic updateToProduct(string condition = null, int? userDemand = null, decimal? duration = null)
        {
            decimal? price = null;
            if (ProductType != null)
            {
                if (this.ProductType.Contains("FoodAndBeverageItems"))
                {
                    FoodsAndBeverageItems foods = new FoodsAndBeverageItems();
                    price = foods.calculateProductUnitPrice();

                }
                else if (this.ProductType.Contains("MaterialItems"))
                {
                    MaterialItems materials = new MaterialItems();
                    price = materials.calculateProductUnitPrice();
                }
                else if (this.ProductType.Contains("GarmentItems"))
                {
                    GarmentItems garments = new GarmentItems();
                    price = garments.calculateProductUnitPrice();
                }
                else if (this.ProductType.Contains("TransportationServices"))
                {
                    TransportationServices transportations = new TransportationServices();
                    price = transportations.calculateProductUnitPrice(condition, userDemand, duration);
                }
                else if (this.ProductType.Contains("TelecommunicationServices"))
                {
                    TelecommunicationServices telecommunications = new TelecommunicationServices();
                    price = telecommunications.calculateProductUnitPrice(condition, userDemand, duration);
                }
                else
                {
                    price = 0;
                }
                return price;
            }
            return 0;
        }

        public Product insertToProduct()
        {
            var description = "";
            var config = new MapperConfiguration(cfg => { });
            var mapper = new Mapper(config);

            if (this.ProductType.Contains("FoodAndBeverageItems"))
            {
                FoodsAndBeverageItems foods = mapper.Map<FoodsAndBeverageItems>(this.ProductDetail);
                description = foods.convertItemsToString();

            }
            else if (this.ProductType.Contains("MaterialItems"))
            {
                MaterialItems materials = mapper.Map<MaterialItems>(this.ProductDetail);
                description = materials.convertItemsToString();
            }
            else if (this.ProductType.Contains("GarmentItems"))
            {
                GarmentItems garments = mapper.Map<GarmentItems>(this.ProductDetail);
                description = garments.convertItemsToString();
            }
            else if (this.ProductType.Contains("TransportationServices"))
            {
                TransportationServices transportations = mapper.Map<TransportationServices>(this.ProductDetail);
                description = transportations.convertServicesToString();
            }
            else if (this.ProductType.Contains("TelecommunicationServices"))
            {
                TelecommunicationServices telecommunications = mapper.Map<TelecommunicationServices>(this.ProductDetail);
                description = telecommunications.convertServicesToString();
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

        public Dictionary<string, object> finalResult(List<CustomProductViewModel> listObject = null, string msg = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("Message", msg);
            result.Add("Data", listObject);
            return (result);
        }
    }
}