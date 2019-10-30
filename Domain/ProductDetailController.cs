using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;
using Northwind.Domain.Calculation;
using Northwind.Domain.ViewModels;
using Northwind.Domain.Validator;
using Northwind.Domain.Models.ProductDetails;

namespace Northwind.Domain
{
    public class ProductDetailController
    {
        [RoutePrefix("api/ProductDetail")]
        public class CustomProductController : ApiController
        {
            [Route("createProductWithStringProductDetail")]
            [HttpPost]
            public IHttpActionResult Create([FromBody] MainProductDetail dataBody, char Delimeter)
            {
                try
                {
                    using (var db = new DB_Context())
                    {
                        ProductDetail detail = null;
                        ProductValidator valid = new ProductValidator(Delimeter);
                        if (valid.isValidProductDetail(detail, dataBody.ProductType))
                        {
                            Product product = dataBody.insertToProduct();
                            db.Products.Add(product);
                            db.SaveChanges();
                        }
                        return Ok("Insert Data Success");
                    }
                }
                catch (Exception)
                {

                    throw new HttpException("Data yang diinputkan tidak valid");
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
        }
    }
}