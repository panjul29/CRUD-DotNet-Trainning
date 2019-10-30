using System.Collections.Generic;
using Northwind.EntityFrameworks;

namespace Northwind.Domain.Models.ProductDetails.Items.Details
{
    public class Material : Item
    {
        public string ExpiredDate { get; set; }
        public string MaterialsType { get; set; }
        public string IsConsumable { get; set; }

        public Material(char Delimeter, Product product) : base(Delimeter)
        {
            this.ProductID = product.ProductID;
            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(this.Delimeter);
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

        public override Dictionary<string, object> ConvertToDictionary()
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

        public override string ConvertToString()
        {
            return this.appendWithDelimiter(
                 this.ProductDescription, this.UnitProfit, this.ProductionCode, this.ProductionDate, this.ExpiredDate, this.MaterialsType,
                 this.IsConsumable, this.UnitOfMeasurement, this.CostRate);
        }
    }
}