using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class TransportationServices
    {
        public int ProductID { get; set; }
        public string ProductDescription { get; set; }
        public string VehicleType { get; set; }
        public string RoutePath { get; set; }
        public string RouteMilleage { get; set; }
        public decimal CostCalculationMethod { get; set; }
        public decimal CostRate { get; set; }

        public TransportationServices()
        {

        }

        public TransportationServices(Product product)
        {

        }

        public string convertToString()
        {
            return
                this.ProductDescription + "|" +
                this.VehicleType + "|" +
                this.RoutePath + "|" +
                this.RouteMilleage + "|" +
                this.CostCalculationMethod + "|" +
                this.CostRate;
        }
    }
}