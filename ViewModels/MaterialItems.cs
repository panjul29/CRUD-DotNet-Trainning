using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class MaterialItems
    {
        public int ProductID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductionCode { get; set; }
        public string ProductionDate { get; set; }
        public string ExpiredDate { get; set; }
        public string MaterialsType { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string IsConsumable { get; set; }

        public MaterialItems()
        {

        }

        public MaterialItems(Product product)
        {
            char[] delimiter = { '|' };
            this.ProductID = product.ProductID;

            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(delimiter);

                this.ProductDescription = prod[0];
                this.ProductionCode = prod[1];
                this.ProductionDate = prod[2];
                this.ExpiredDate = prod[3];
                this.MaterialsType = prod[4];
                this.UnitOfMeasurement = prod[5];
                this.IsConsumable = prod[6];
            }
        }

        public Dictionary<string, object> fromMaterialToDict()
        {
            Dictionary<string, object> mateDict = new Dictionary<string, object>();

            mateDict.Add("ProductID", this.ProductID);
            mateDict.Add("ProductDescription", this.ProductDescription);
            mateDict.Add("ProductionCode", this.ProductionCode);
            mateDict.Add("ProductionDate", this.ProductionDate);
            mateDict.Add("ExpiredDate", this.ExpiredDate);
            mateDict.Add("MaterialsType", this.MaterialsType);
            mateDict.Add("UnitOfMeasurement", this.UnitOfMeasurement);
            mateDict.Add("IsConsumable", this.IsConsumable);

            return mateDict;
        }

        public string convertToString()
        {
            return
                this.ProductDescription + "|" +
                this.ProductionCode + "|" +
                this.ProductionDate + "|" +
                this.ExpiredDate + "|" +
                this.MaterialsType + "|" +
                this.UnitOfMeasurement + "|" +
                this.IsConsumable;
        }
    }
}