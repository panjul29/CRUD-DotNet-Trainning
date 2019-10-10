using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class GarmentItems
    {
        public int ProductID { get; set; }
        public string ProductDescription { get; set; }
        public int ProductionCode { get; set; }
        public string ProductionDate { get; set; }
        public string GarmentsType { get; set; }
        public string Fabrics { get; set; }
        public string GenderRelated { get; set; }
        public bool IsWaterProof { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string AgeGroup { get; set; }

        public GarmentItems()
        {

        }

        public GarmentItems(Product product)
        {

        }

        public string convertToString()
        {
            return
                this.ProductDescription + "|" +
                this.ProductionCode + "|" +
                this.ProductionDate + "|" +
                this.GarmentsType + "|" +
                this.Fabrics + "|" +
                this.GenderRelated + "|" +
                this.IsWaterProof + "|" +
                this.Color + "|" +
                this.Size + "|" +
                this.AgeGroup;
        }
    }
}