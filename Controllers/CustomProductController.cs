using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;
using Northwind.Domain.Calculation;
using Northwind.Domain.ViewModels;

namespace Northwind.Controllers
{
    [RoutePrefix("api/ProductCus")]
    public class CustomProductController : ApiController
    {
        [Route("read")]
        [HttpGet]
        public IHttpActionResult Read(int? proID = null)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    var tem = db.Products.AsQueryable();
                    CustomProductViewModel productObj = new CustomProductViewModel();
                    List<CustomProductViewModel> listProduct = new List<CustomProductViewModel>();
                    if (proID != null)
                    {
                        tem = tem.Where(data => data.ProductID == proID);
                    }
                    foreach (var item in tem)
                    {
                        CustomProductViewModel product = new CustomProductViewModel(item);
                        listProduct.Add(product);
                    }
                    return Ok(productObj.finalResult(listProduct, "Read Data Success"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] CustomProductViewModel dataBody)
        {
            try
            {
                using (var db = new DB_Context())
                {
                    List<CustomProductViewModel> listResult = new List<CustomProductViewModel>();
                    Product product = dataBody.insertToProduct();
                    db.Products.Add(product);
                    db.SaveChanges();
                    return Ok("Insert Data Success");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("calculateProductUnitPrice")]
        [HttpPut]
        public IHttpActionResult UpdateUnitPrice(string condition = null, int? userDemand = null, decimal? duration = null)
        {
            try
            {
                using(var db = new DB_Context())
                {
                    var listProduct = db.Products.OrderByDescending(data => data.ProductID).ToList();
                    List<CustomProductViewModel> listResult = new List<CustomProductViewModel>();
                    foreach (var item in listProduct)
                    {
                        CustomProductViewModel product = new CustomProductViewModel(item, condition, userDemand, duration);
                        listResult.Add(product);
                        db.SaveChanges();
                    }
                    CustomProductViewModel obj = new CustomProductViewModel();
                    return Ok(obj.finalResult(listResult, "Update Unit Price Success"));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int ProID)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Product product = db.Products.Where(data => data.ProductID == ProID).FirstOrDefault();
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return Ok("Delete data Success");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Route("calculateProductUnitPrice2")]
        [HttpPut]
        public IHttpActionResult calculateProductUnitPrice([FromBody] ProductDetailCalculatorParameter parameter)
        {
            try
            {
                using (var db = new DB_Context())
                {
                    var temp = db.Products.AsQueryable();
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    var listProduct = db.Products.OrderByDescending(data => data.ProductID).ToList();

                    ProductCalculator calculator = new ProductCalculator('|');
                    foreach (var item in listProduct)
                    {
                        calculator.calculateProductUnitPrice(item, parameter);
                    }

                    db.SaveChanges();
                    return Ok("UnitPrice Has Been Updated");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Route("getCost")]
        //[HttpGet]
        //public IHttpActionResult CostCalculation(string condition, int userdemand)
        //{
        //    using (var db = new DB_Context())
        //    {
        //        try
        //        {
        //            var tem = db.Products.AsQueryable();
        //            Dictionary<string, object> result = new Dictionary<string, object>();
        //            List<CostCalculationViewModel> listProduct = new List<CostCalculationViewModel>();
        //            tem = tem.Where(data => data.ProductType.Contains("TransportationServices"));
        //            var listCostEntity = tem.AsEnumerable().ToList();
        //            foreach (var item in listCostEntity)
        //            {
        //                CostCalculationViewModel product = new CostCalculationViewModel(item, condition, userdemand);
        //                listProduct.Add(product);
        //            }
        //            result.Add("Message", "Read Data Success");
        //            result.Add("Data", listProduct);
        //            return Ok(result);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //}


    }
}