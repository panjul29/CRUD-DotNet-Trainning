using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Domain.Models.ProductDetails;
using Northwind.Domain.Models.ProductDetails.Items.Details;
using Northwind.Domain.Models.ProductDetails.Services.Details;
using Northwind.Domain.ViewModels;
using Northwind.EntityFrameworks;

namespace Northwind.Domain
{
    public class InstantiationObjectProductDetail
    {
        private char Delimeter;
        public InstantiationObjectProductDetail(char Delimeter)
        {
            this.Delimeter = Delimeter;
        }

        public void instantiationObject(Product product)
        {
            ProductDetail obj = null;
            if (product.ProductType.Equals("FoodAndBeverageItems"))
            {
                obj = new FoodAndBeverage(this.Delimeter, product);
                obj.GetType();
            }
            else if (product.ProductType.Equals("MaterialItems"))
            {
                obj = new Material(this.Delimeter, product);
                obj.GetType();
            }
            else if (product.ProductType.Equals("GarmentItems"))
            {
                obj = new Garment(this.Delimeter, product);
                obj.GetType();
            }
            else if (product.ProductType.Equals("TransportationServices"))
            {
                obj = new Transportation(this.Delimeter, product);
                obj.GetType();
            }
            else if (product.ProductType.Equals("TelecommunicationServices"))
            {
                obj = new Telecommunication(this.Delimeter, product);
                obj.GetType();
            }
            else
            {
                throw new Exception("Unknown Product Type");
            }
        }
    }
}