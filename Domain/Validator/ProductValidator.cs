using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Domain.ViewModels;
using Northwind.Domain.Models;
using Northwind.Domain.Models.ProductDetails;
using Northwind.Domain.Models.ProductDetails.Items;
using Northwind.Domain.Models.ProductDetails.Services;
using Northwind.EntityFrameworks;

namespace Northwind.Domain.Validator
{
    public class ProductValidator
    {
        private char Delimiter;
        public ProductValidator(char Delimiter)
        {
            this.Delimiter = Delimiter;
        }

        public bool isValidProductDetail(ProductDetail productDetail, string productType)
        {
            var status = false;
            Product product = new Product();
            if (productDetail.GetType() == typeof(Item) && productDetail.Delimeter == '|')
            {
                InstantiationObjectProductDetail obj = new InstantiationObjectProductDetail(this.Delimiter);
                obj.instantiationObject(product);
                if (productType == product.ProductType)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                
            }
            else if (productDetail.GetType() == typeof(Service) && productDetail.Delimeter == ';')
            {
                InstantiationObjectProductDetail obj = new InstantiationObjectProductDetail(this.Delimiter);
                obj.instantiationObject(product);
                if (productType == product.ProductType)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
            ;
        } //Done

        public bool isValidProductDetail(Dictionary<string, object> productDetail, string productType)
        {
            return true;
        }

        public bool isValidProductDetail(string productDetail, string productType)
        {
            return false;
        }
    }
}