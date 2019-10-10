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
        public int ProductionCode { get; set; }
        public string ProductionDate { get; set; }
        public string ExpiredDate { get; set; }
        public string MaterialsType { get; set; }
        public string UnitOfMeasurement { get; set; }
        public bool IsConsumable { get; set; }

        public MaterialItems()
        {

        }

        public MaterialItems(Product product)
        {

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