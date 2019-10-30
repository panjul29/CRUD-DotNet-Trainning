using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.ViewModels;
using Northwind.Domain.Models.ProductDetails;
using Northwind.Domain.Models.ProductDetails.Items.Details;
using Northwind.Domain.Models.ProductDetails.Services.Details;
using Northwind.EntityFrameworks;
using AutoMapper;

namespace Northwind.Domain.ViewModels
{
    public class MainProductDetail : ProductViewModel
    {
        public MainProductDetail(Product product, char Delimeter)
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
            ProductDetail = convertToList(product, Delimeter);
        }

        public Product insertToProduct()
        {
            var description = "";
            var config = new MapperConfiguration(cfg => { });
            var mapper = new Mapper(config);

            if (this.ProductType.Contains("FoodAndBeverageItems"))
            {
                FoodAndBeverage foods = mapper.Map<FoodAndBeverage>(this.ProductDetail);
                description = foods.ConvertToString();

            }
            else if (this.ProductType.Contains("MaterialItems"))
            {
                Material materials = mapper.Map<Material>(this.ProductDetail);
                description = materials.ConvertToString();
            }
            else if (this.ProductType.Contains("GarmentItems"))
            {
                Garment garments = mapper.Map<Garment>(this.ProductDetail);
                description = garments.ConvertToString();
            }
            else if (this.ProductType.Contains("TransportationServices"))
            {
                Transportation transportations = mapper.Map<Transportation>(this.ProductDetail);
                description = transportations.ConvertToString();
            }
            else if (this.ProductType.Contains("TelecommunicationServices"))
            {
                Telecommunication telecommunications = mapper.Map<Telecommunication>(this.ProductDetail);
                description = telecommunications.ConvertToString();
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


        public dynamic convertToList(Product product, char Delimeter)
        {
           
            InstantiationObjectProductDetail obj = new InstantiationObjectProductDetail(Delimeter);
            if (product.ProductType != null)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                switch (product.ProductType)
                {
                    case "FoodAndBeverageItems":
                        FoodAndBeverage food = new FoodAndBeverage(Delimeter, product);
                        ProductDetail = food.ConvertToDictionary();
                        result = ProductDetail;
                        break;
                    case "GarmentItems":
                        Garment garment = new Garment(Delimeter, product);
                        ProductDetail = garment.ConvertToDictionary();
                        result = ProductDetail;
                        break;
                    case "MaterialItems":
                        Material materi = new Material(Delimeter, product);
                        ProductDetail = materi.ConvertToDictionary();
                        result = ProductDetail;
                        break;
                    case "TransportationServices":
                        Transportation trans = new Transportation(Delimeter, product);
                        ProductDetail = trans.ConvertToDictionary();
                        result = ProductDetail;
                        break;
                    case "TelecommunicationServices":
                        Telecommunication telcom = new Telecommunication(Delimeter, product);
                        ProductDetail = telcom.ConvertToDictionary();
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
    }
}