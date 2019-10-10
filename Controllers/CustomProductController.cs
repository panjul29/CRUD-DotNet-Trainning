using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;

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
                    var listProductEntity = tem.AsEnumerable().ToList();
                    foreach (var item in listProductEntity)
                    {
                        CustomProductViewModel region = new CustomProductViewModel(item);
                        listProduct.Add(region);
                    }
                    return Ok(productObj.FinalResult(listProduct, "Read Data Success"));
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
                    Product product = dataBody.convertToProduct();
                    return Ok("SIP");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}