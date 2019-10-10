using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class RegionDetailViewModel
    {
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionLongitude { get; set; }
        public string RegionLatitude { get; set; }
        public string Country { get; set; }

        public RegionDetailViewModel()
        {

        }

        public RegionDetailViewModel(List<Region> regionList)
        {
            foreach (var region in regionList)
            {
                this.RegionID = region.RegionID;

                if (!string.IsNullOrEmpty(region.RegionDescription))
                {
                    char[] delimiter = { '|' };
                    String[] regionDetailData = region.RegionDescription.Split(delimiter);

                    if (regionDetailData.Length == 4)
                    {
                        this.RegionName = regionDetailData[0];
                        this.RegionLongitude = regionDetailData[1];
                        this.RegionLatitude = regionDetailData[2];
                        this.Country = regionDetailData[3].Trim();
                    }
                }
                else
                {
                    RegionName = region.RegionDescription.Trim();
                    RegionLongitude = null;
                    RegionLatitude = null;
                    Country = null;
                }
            }
        }

        public RegionDetailViewModel(Region region)
        {
            string tem = region.RegionDescription;
            RegionID = region.RegionID;

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
                RegionName = region.RegionDescription.Trim();
                RegionLongitude = null;
                RegionLatitude = null;
                Country = null;
            }
        }

        public Region convertToRegion()
        {
            char[] delimiter = { '|' };
            return new Region()
            {
                RegionID = this.RegionID,
                RegionDescription =
                    this.RegionName + delimiter[0] +
                    this.RegionLongitude + delimiter[0] +
                    this.RegionLatitude + delimiter[0] +
                    this.Country,
            };
        }

        public Region convertToRegion2(Region region)
        {
            char[] delimiter = { '|' };
            region.RegionID = this.RegionID;
            region.RegionDescription =
                this.RegionName + delimiter[0] +
                this.RegionLongitude + delimiter[0] +
                this.RegionLatitude + delimiter[0] +
                this.Country;
            return region;
        }

        public Territory Insertdata(DB_Context db)
        {
            if (this.Country.Contains("INA"))
            {
                Territory desc_terito = new Territory()
                {
                    TerritoryID = "INA-01",
                    TerritoryDescription = "Bandung adalah bagian dari Indonesia",
                    RegionID = this.RegionID
                };
                db.Territories.Add(desc_terito);
                db.SaveChanges();
                return desc_terito;
            }
            return new Territory();
        }

        public Dictionary<string, object> finalResult(List<RegionDetailViewModel> listObject, string message)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("Message", message);
            result.Add("Data", listObject);
            return result;
        }
    }
}