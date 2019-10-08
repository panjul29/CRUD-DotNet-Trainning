using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class CustomRegionViewModel
    {
        public int? RegionID { get; set; }

        public string RegionName { get; set; }

        public string RegionLongitude { get; set; }

        public string RegionLatitude { get; set; }

        public string Country { get; set; }

        public CustomRegionViewModel()
        {

        }

        public CustomRegionViewModel(Region entityReg)
        {
            string tem = entityReg.RegionDescription;
            RegionID = entityReg.RegionID;
            
            if (tem.Contains("|"))
            {
                var data = tem.Split('|');
                RegionName = data[0];
                RegionLongitude = data[1];
                RegionLatitude = data[2];
                Country = data[3].Trim();
            }
            else
            {
                RegionName = entityReg.RegionDescription.Trim();
                RegionLongitude = null;
                RegionLatitude = null;
                Country = null;
            }
        }
    }
}