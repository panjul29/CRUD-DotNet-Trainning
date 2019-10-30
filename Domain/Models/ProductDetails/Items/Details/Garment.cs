using System.Collections.Generic;
using Northwind.EntityFrameworks;

namespace Northwind.Domain.Models.ProductDetails.Items.Details
{
    public class Garment : Item
    {
        public string GarmentsType { get; set; }
        public string Fabrics { get; set; }
        public string GenderRelated { get; set; }
        public string IsWaterProof { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string AgeGroup { get; set; }

        public Garment(char Delimeter, Product product) : base(Delimeter)
        {
            this.ProductID = product.ProductID;
            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(this.Delimeter);
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

        public override Dictionary<string, object> ConvertToDictionary()
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

        public override string ConvertToString()
        {
            return this.appendWithDelimiter(
                this.ProductDescription, this.UnitProfit, this.ProductionCode, this.ProductionDate, this.GarmentsType, this.Fabrics,
                this.GenderRelated, this.IsWaterProof, this.Color, this.Size, this.AgeGroup, this.UnitOfMeasurement, this.CostRate);
        }
    }
}