﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Items
{
    public class GarmentItems : ProductDetailViewModel, IItems
    {
        public string ProductionCode { get; set; }
        public string ProductionDate { get; set; }
        public string GarmentsType { get; set; }
        public string Fabrics { get; set; }
        public string GenderRelated { get; set; }
        public string IsWaterProof { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string AgeGroup { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CostRate { get; set; }

        public char delimiter()
        {
            return '|';
        }

        public GarmentItems()
        {

        }

        public GarmentItems(Product product)
        {
            this.ProductID = product.ProductID;

            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(delimiter());

                this.ProductDescription = prod[0];
                this.UnitProfit = prod[1];
                this.ProductionCode = prod[2];
                this.ProductionDate = prod[3];
                this.GarmentsType = prod[4];
                this.Fabrics = prod[5];
                this.GenderRelated = prod[6];
                this.IsWaterProof = prod[7];
                this.Color = prod[8];
                this.Size = prod[9];
                this.AgeGroup = prod[10];
                this.UnitOfMeasurement = prod[11];
                this.CostRate = prod[12];
            }
        }

        public Dictionary<string, object> fromItemsToDict()
        {
            Dictionary<string, object> garmentDict = new Dictionary<string, object>();

            garmentDict.Add("ProductID", this.ProductID);
            garmentDict.Add("ProductDescription", this.ProductDescription);
            garmentDict.Add("UnitProfit", this.UnitProfit);
            garmentDict.Add("ProductionCode", this.ProductionCode);
            garmentDict.Add("ProductionDate", this.ProductionDate);
            garmentDict.Add("GarmentsType", this.GarmentsType);
            garmentDict.Add("Fabrics", this.Fabrics);
            garmentDict.Add("GenderRelated", this.GenderRelated);
            garmentDict.Add("IsWaterProof", this.IsWaterProof);
            garmentDict.Add("Color", this.Color);
            garmentDict.Add("Size", this.Size);
            garmentDict.Add("AgeGroup", this.AgeGroup);
            garmentDict.Add("UnitOfMeasurement", this.UnitOfMeasurement);
            garmentDict.Add("CostRate", this.CostRate);

            return garmentDict;
        }

        public string convertItemsToString()
        {
            return
                this.ProductDescription + delimiter() +
                this.UnitProfit + delimiter() +
                this.ProductionCode + delimiter() +
                this.ProductionDate + delimiter() +
                this.GarmentsType + delimiter() +
                this.Fabrics + delimiter() +
                this.GenderRelated + delimiter() +
                this.IsWaterProof + delimiter() +
                this.Color + delimiter() +
                this.Size + delimiter() +
                this.AgeGroup + delimiter() +
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