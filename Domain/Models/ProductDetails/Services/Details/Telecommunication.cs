using System;
using System.Collections.Generic;
using Northwind.EntityFrameworks;

namespace Northwind.Domain.Models.ProductDetails.Services.Details
{
    public class Telecommunication : Service
    {
        public string PacketType { get; set; }
        public string PacketLimit { get; set; }

        public Telecommunication(char Delimeter, Product product) : base(Delimeter)
        {
            this.ProductID = product.ProductID;
            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(this.Delimeter);
                this.ProductDescription = prod[0];
                this.UnitProfit = prod[1];
                this.PacketType = prod[2];
                this.PacketLimit = prod[3];
                this.CostCalculationMethod = prod[4];
                this.CostRate = prod[5];

            }
        }

        public override Dictionary<string, object> ConvertToDictionary()
        {
            Dictionary<string, object> telDict = new Dictionary<string, object>();
            telDict.Add("ProductID", this.ProductID);
            telDict.Add("ProductDescription", this.ProductDescription);
            telDict.Add("UnitPrice", this.UnitProfit);
            telDict.Add("PacketType", this.PacketType);
            telDict.Add("PacketLimit", this.PacketLimit);
            telDict.Add("CostCalculationMethod", this.CostCalculationMethod);
            telDict.Add("CostRate", this.CostRate);

            return telDict;
        }

        public override string ConvertToString()
        {
            return this.appendWithDelimiter(this.ProductDescription, this.UnitProfit, this.PacketType, this.PacketLimit, this.CostCalculationMethod, this.CostRate);
        }

        public override decimal calculateProductCost()
        {
            decimal DecCostRate = decimal.Parse(CostRate);
            var temp = this.parameter;

            if (CostCalculationMethod.Equals("PerSecond"))
            {
                return DecCostRate * this.parameter.getNonNullDuration();
            }
            else if (CostCalculationMethod.Equals("PerPacket"))
            {
                if (PacketType.Equals("Data"))
                {
                    return decimal.Parse(PacketLimit) * DecCostRate;
                }
                else
                {
                    return DecCostRate * this.parameter.getNonNullDuration();
                }
            }
            else
            {
                return 0;
            }
        }
    }
}