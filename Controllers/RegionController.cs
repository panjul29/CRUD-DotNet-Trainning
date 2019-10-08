using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;

namespace Northwind.Controllers
{
    [RoutePrefix("api/Region")]
    public class RegionController : ApiController
    {
        [Route("readAll")]
        [HttpGet]
        public IHttpActionResult ReadAll(int? regID = null)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    var tem = db.Regions.AsQueryable();
                    RegionDetailViewModel regionObj = new RegionDetailViewModel();
                    List<RegionDetailViewModel> listRegion = new List<RegionDetailViewModel>();
                    if (regID != null)
                    {
                        tem = tem.Where(data => data.RegionID == regID);
                    }
                    var listRegionEntity = tem.AsEnumerable().ToList();
                    foreach (var item in listRegionEntity)
                    {
                        RegionDetailViewModel region = new RegionDetailViewModel(item);
                        listRegion.Add(region);
                    }
                    return Ok(regionObj.finalResult(listRegion, "Read Data Success"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] RegionDetailViewModel databody)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    List<RegionDetailViewModel> listRegion = new List<RegionDetailViewModel>();
                    var temp = databody.convertToRegion();
                    db.Regions.Add(temp);
                    db.SaveChanges();
                    var reg = db.Regions.Where(dt => dt.RegionID == databody.RegionID).AsEnumerable().ToList();
                    RegionDetailViewModel regView = new RegionDetailViewModel(reg);
                    listRegion.Add(regView);
                    databody.Insertdata(db);
                    return Ok(databody.finalResult(listRegion, "Insert Data Success"));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] RegionDetailViewModel databody)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    List<RegionDetailViewModel> listRegion = new List<RegionDetailViewModel>();
                    Region region = db.Regions.Find(databody.RegionID);
                    var temp = databody.convertToRegion2(region);
                    RegionDetailViewModel reg = new RegionDetailViewModel(temp);
                    listRegion.Add(reg);
                    db.SaveChanges();
                    return Ok(databody.finalResult(listRegion, "Update Data Success"));

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        [Route("delete")]
        [HttpDelete]

        public IHttpActionResult Delete(int regID)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    var territory = db.Territories.Where(dt => dt.RegionID == regID).ToList();
                    foreach (var item in territory)
                    {
                        db.Territories.Remove(item);
                    }
                    Region region = db.Regions.Where(data => data.RegionID == regID).FirstOrDefault();
                    db.Regions.Remove(region);
                    db.SaveChanges();
                    result.Add("Message", "Delete data success");
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}