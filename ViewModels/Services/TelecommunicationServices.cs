using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Services
{
    public class TelecommunicationServices : IServices
    {
        public int ProductID { get; set; }
        public string ProductDescription { get; set; }
        public string PacketType { get; set; }
        public string PacketLimit { get; set; }
        public string CostCalculationMethod { get; set; }
        public string CostRate { get; set; }

        public char delimiter()
        {
            return '\'';
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
                this.PacketType = prod[1];
                this.PacketLimit = prod[2];
                this.CostCalculationMethod = prod[3];
                this.CostRate = prod[4];
            }
        }

        public Dictionary<string, object> fromServicesToDict()
        {
            Dictionary<string, object> transDict = new Dictionary<string, object>();

            transDict.Add("ProductID", this.ProductID);
            transDict.Add("ProductDescription", this.ProductDescription);
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
                this.PacketType + delimiter() +
                this.PacketLimit + delimiter() +
                this.CostCalculationMethod + delimiter() +
                this.CostRate;
        }

        public decimal? calculateProductUnitPrice(string condition = null, int? userDemand = null, decimal? duration = null)
        {
            decimal? valueResult = null;
            decimal decCostRate = decimal.Parse(CostRate);

            if (CostCalculationMethod.Equals("PerSecond"))
            {
                valueResult = decCostRate * duration;
            }
            else if (CostCalculationMethod.Equals("PerPacket"))
            {
                if (PacketType.Equals("Data"))
                {
                    valueResult = decimal.Parse(PacketLimit) * decCostRate;
                }
                else
                {
                    valueResult = (decCostRate * duration) * decCostRate;
                }
            }
            else
            {
                valueResult = 0;
            }

            return valueResult * (Convert.ToDecimal(110) / Convert.ToDecimal(100));
        }
    }
}