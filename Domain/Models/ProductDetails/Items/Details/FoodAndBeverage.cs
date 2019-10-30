using System.Collections.Generic;
using Northwind.EntityFrameworks;

namespace Northwind.Domain.Models.ProductDetails.Items.Details
{
    public class FoodAndBeverage : Item
    {
        public string ExpiredDate { get; set; }
        public string NetWeight { get; set; }
        public string Ingredients { get; set; }
        public string DailyValue { get; set; }
        public string Certification { get; set; }

        public FoodAndBeverage(char Delimeter, Product product) : base(Delimeter)
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
                this.NetWeight = prod[5];
                this.Ingredients = prod[6];
                this.DailyValue = prod[7];
                this.Certification = prod[8];
                this.UnitOfMeasurement = prod[9];
                this.CostRate = prod[10];
            }
        }

        public override Dictionary<string, object> ConvertToDictionary()
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

        public override string ConvertToString()
        {
            return this.appendWithDelimiter(
                this.ProductDescription, this.UnitProfit, this.ProductionCode, this.ProductionDate, this.ExpiredDate, this.NetWeight,
                this.Ingredients, this.DailyValue, this.Certification, this.UnitOfMeasurement, this.CostRate);
        }

    }
}