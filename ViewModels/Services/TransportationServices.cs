﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Services
{
    public class TransportationServices : ProductDetailViewModel, IServices
    {
        public string VehicleType { get; set; }
        public string RoutePath { get; set; }
        public string RouteMilleage { get; set; }
        public string CostCalculationMethod { get; set; }
        public string CostRate { get; set; }

        public char delimiter()
        {
            return '|';
        }

        public TransportationServices()
        {

        }

        public TransportationServices(Product product)
        {
            this.ProductID = product.ProductID;

            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(delimiter());

                this.ProductDescription = prod[0];
                this.UnitProfit = prod[1];
                this.VehicleType = prod[2];
                this.RoutePath = prod[3];
                this.RouteMilleage = prod[4];
                this.CostCalculationMethod = prod[5];
                this.CostRate = prod[6];
            }
        }

        public Dictionary<string, object> fromServicesToDict()
        {
            Dictionary<string, object> transDict = new Dictionary<string, object>();

            transDict.Add("ProductID", this.ProductID);
            transDict.Add("ProductDescription", this.ProductDescription);
            transDict.Add("UnitPrice", this.UnitProfit);
            transDict.Add("VehicleType", this.VehicleType);
            transDict.Add("RoutePath", this.RoutePath);
            transDict.Add("RouteMilleage", this.RouteMilleage);
            transDict.Add("CostCalculationMethod", this.CostCalculationMethod);
            transDict.Add("CostRate", this.CostRate);

            return transDict;
        }

        public string convertServicesToString()
        {
            return
                this.ProductDescription + delimiter() +
                this.UnitProfit + delimiter() +
                this.VehicleType + delimiter() +
                this.RoutePath + delimiter() +
                this.RouteMilleage + delimiter() +
                this.CostCalculationMethod + delimiter() +
                this.CostRate;
        }

        public decimal? calculateProductUnitPrice(string condition = null, int? userDemand = null, decimal? duration = null)
        {
            int? hasil = null;
            if (CostCalculationMethod.Equals("FixPerRoute"))
            {
                hasil = 1 * Int32.Parse(CostRate);
            }
            if (CostCalculationMethod.Equals("PerMiles"))
            {
                hasil = Int32.Parse(RouteMilleage) * (Int32.Parse(CostRate) / 2);
            }
            if (CostCalculationMethod.Equals("PerMilesWithCondition"))
            {
                var nilai = 0;
                if (condition == "GoodWeather")
                {
                    nilai = 5;
                }
                else if (condition == "BadWeather")
                {
                    nilai = 15;
                }
                hasil = (Int32.Parse(RouteMilleage) * Int32.Parse(CostRate) / 2) * (((nilai + (userDemand / 50)) + 95)) / 100;
            }
            return hasil * (Convert.ToDecimal(110) / Convert.ToDecimal(100));
        }
    }
}