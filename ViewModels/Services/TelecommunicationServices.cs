using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Services
{
    public class TelecommunicationServices : ProductDetailViewModel, IServices
    {
        public string PacketType { get; set; }
        public string PacketLimit { get; set; }
        public string CostCalculationMethod { get; set; }
        public string CostRate { get; set; }

        public char delimiter()
        {
            return '|';
        }

        public TelecommunicationServices()
        {

        }

        public TelecommunicationServices(Product product)
        {
            this.ProductID = product.ProductID;

            if (!string.IsNullOrEmpty(product.ProductDetail))
            {
                string[] prod = product.ProductDetail.Split(delimiter());

                this.ProductDescription = prod[0];
                this.UnitProfit = prod[1];
                this.PacketType = prod[2];
                this.PacketLimit = prod[3];
                this.CostCalculationMethod = prod[4];
                this.CostRate = prod[5];
            }
        }

        public Dictionary<string, object> fromServicesToDict()
        {
            Dictionary<string, object> transDict = new Dictionary<string, object>();

            transDict.Add("ProductID", this.ProductID);
            transDict.Add("ProductDescription", this.ProductDescription);
            transDict.Add("UnitPrice", this.UnitProfit);
            transDict.Add("PacketType", this.PacketType);
            transDict.Add("PacketLimit", this.PacketLimit);
            transDict.Add("CostCalculationMethod", this.CostCalculationMethod);
            transDict.Add("CostRate", this.CostRate);

            return transDict;
        }

        public string convertServicesToString()
        {
            return
                this.ProductDescription + delimiter() +
                this.UnitProfit + delimiter() +
                this.PacketType + delimiter() +
                this.PacketLimit + delimiter() +
                this.CostCalculationMethod + delimiter() +
                this.CostRate;
        }

        public decimal? calculateProductUnitPrice(string condition = null, int? userDemand = null, decimal? duration = null)
        {
            decimal? result = null;
            decimal decCostRate = decimal.Parse(CostRate);
            if (CostCalculationMethod.Equals("PerSecond"))
            {
                result = decCostRate * duration;
            }
            else if (CostCalculationMethod.Equals("PerPacket"))
            {
                if (PacketType.Equals("Data"))
                {
                    result = decimal.Parse(PacketLimit) * decCostRate;
                }
                else
                {
                    result = (decCostRate * duration) * decCostRate;
                }
            }
            else
            {
                result = 0;
            }

            return result * (Convert.ToDecimal(110) / Convert.ToDecimal(100));
        }
    }
}