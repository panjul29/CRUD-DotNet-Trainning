﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Items
{
    public class MaterialItems : ProductDetailViewModel, IItems
    {
        public string ProductionCode { get; set; }
        public string ProductionDate { get; set; }
        public string ExpiredDate { get; set; }
        public string MaterialsType { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string IsConsumable { get; set; }
        public string CostRate { get; set; }

        public char delimiter()
        {
            return '|';
        }

        public MaterialItems()
        {

        }

        public MaterialItems(Product product)
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
                this.MaterialsType = prod[5];
                this.UnitOfMeasurement = prod[6];
                this.IsConsumable = prod[7];
                this.CostRate = prod[8];
            }
        }

        public Dictionary<string, object> fromItemsToDict()
        {
            Dictionary<string, object> mateDict = new Dictionary<string, object>();

            mateDict.Add("ProductID", this.ProductID);
            mateDict.Add("ProductDescription", this.ProductDescription);
            mateDict.Add("UnitProfit", this.UnitProfit);
            mateDict.Add("ProductionCode", this.ProductionCode);
            mateDict.Add("ProductionDate", this.ProductionDate);
            mateDict.Add("ExpiredDate", this.ExpiredDate);
            mateDict.Add("MaterialsType", this.MaterialsType);
            mateDict.Add("UnitOfMeasurement", this.UnitOfMeasurement);
            mateDict.Add("IsConsumable", this.IsConsumable);
            mateDict.Add("CostRate", this.CostRate);

            return mateDict;
        }

        public string convertItemsToString()
        {
            return
                this.ProductDescription + delimiter() +
                this.UnitProfit + delimiter() +
                this.ProductionCode + delimiter() +
                this.ProductionDate + delimiter() +
                this.ExpiredDate + delimiter() +
                this.MaterialsType + delimiter() +
                this.UnitOfMeasurement + delimiter() +
                this.IsConsumable + delimiter() +
                this.CostRate;
        }

        public decimal calculateProductUnitPrice()
        {
            var result = decimal.Parse(CostRate) * (Convert.ToDecimal(110) / Convert.ToDecimal(100));
            return result;
        }
    }
}