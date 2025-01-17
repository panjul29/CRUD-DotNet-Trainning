﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Items
{
    public class FoodsAndBeverageItems : ProductDetailViewModel ,IItems
    {
        public string ProductionCode { get; set; }
        public string ProductionDate { get; set; }
        public string ExpiredDate { get; set; }
        public string NetWeight { get; set; }
        public string Ingredients { get; set; }
        public string DailyValue { get; set; }
        public string Certification { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CostRate { get; set; }

        public char delimiter()
        {
            return '|';
        }

        public FoodsAndBeverageItems()
        {

        }

        public FoodsAndBeverageItems(Product product)
        {
            this.ProductID = product.ProductID;

            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(delimiter());

                this.ProductDescription = prod[0];
                this.UnitProfit = prod[1];
                this.ProductionCode = prod[2];
                this.ProductionDate = prod[3];
                this.ExpiredDate = prod[4];
                this.NetWeight = prod[5];
                this.Ingredients = prod[6];
                this.DailyValue = prod[7];
                this.Certification = prod[8];
                this.UnitOfMeasurement = prod[9];
                this.CostRate = prod[10];
            }
        }

        public Dictionary<string, object> fromItemsToDict()
        {
            Dictionary<string, object> foodDict = new Dictionary<string, object>();

            foodDict.Add("ProductID", this.ProductID);
            foodDict.Add("ProductDescription", this.ProductDescription);
            foodDict.Add("UnitProfit", this.UnitProfit);
            foodDict.Add("ProductionCode", this.ProductionCode);
            foodDict.Add("ProductionDate", this.ProductionDate);
            foodDict.Add("ExpiredDate", this.ExpiredDate);
            foodDict.Add("NetWeight", this.NetWeight);
            foodDict.Add("Ingredients", this.Ingredients);
            foodDict.Add("DailyValue", this.DailyValue);
            foodDict.Add("Certification", this.Certification);
            foodDict.Add("UnitOfMeasurement", this.UnitOfMeasurement);
            foodDict.Add("CostRate", this.CostRate);

            return foodDict;
        }

        public string convertItemsToString()
        {
            return
                this.ProductDescription + delimiter() +
                this.UnitProfit + delimiter() +
                this.ProductionCode + delimiter() +
                this.ProductionDate + delimiter() +
                this.ExpiredDate + delimiter() +
                this.NetWeight + delimiter() +
                this.Ingredients + delimiter() +
                this.DailyValue + delimiter() +
                this.Certification + delimiter() +
                this.UnitOfMeasurement + delimiter() +
                this.CostRate;
        }

        public decimal calculateProductUnitPrice()
        {
            var result = decimal.Parse(CostRate) * (Convert.ToDecimal(110) / Convert.ToDecimal(100));
            return result;
        }
    }
}