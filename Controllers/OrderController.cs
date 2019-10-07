using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;

namespace Northwind.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        [Route("readBy")]
        [HttpGet]
        public IHttpActionResult ReadBy(int orID)
        {
            using(var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    List<CustomOrderViewModel> listProduct = new List<CustomOrderViewModel>();

                    // Mencari list Product berdasarkan OrderID
                    var orDetTemp = db.Orders.Where(data => data.OrderID == orID).AsEnumerable().FirstOrDefault()
                        .Order_Details.Where(data2 => data2.OrderID == orID)
                        .ToList();

                    // Data Order Master
                    var orderData = db.Orders.Where(data => data.OrderID == orID).AsEnumerable().FirstOrDefault();
                    // Mencari Contact Name
                    var resultName = orderData.Customer.ContactName;

                    foreach (var item in orDetTemp)
                    {
                        CustomOrderViewModel productList = new CustomOrderViewModel(item);
                        listProduct.Add(productList);
                    };

                    return Ok(listProduct);
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        
        [Route("readBy2")]
        [HttpGet]
        public IHttpActionResult ReadBy2(int? orID2 = null)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    List<CustomOrderViewModel2> listAllDetail = new List<CustomOrderViewModel2>();
                    var orderData = db.Orders.AsQueryable();

                    if (orID2 != null)
                    {
                        orderData = orderData.Where(data => data.OrderID == orID2);
                    }

                    var listOrder = orderData.AsEnumerable().ToList();

                    foreach (var item in listOrder)
                    {
                        var listProductDetail = item.Order_Details.ToList().Select(dt => new CustomOrderViewModel(dt)).ToList(); // jika mapping di constructor ViewModal
                        CustomOrderViewModel2 orderDetail = new CustomOrderViewModel2(item);
                        // jika mapping manual
                        //{
                        //    OrderID = item.OrderID,
                        //    ContactName = item.Customer.ContactName,
                        //    ProductList = item.Order_Details.ToList().Select(dt => new CustomOrderViewModel(dt)).ToList(),
                        //    GrandTotal = item.Order_Details.ToList().Select(dt => new CustomOrderViewModel(dt)).ToList().Sum(d => d.Total)
                        //};
                        listAllDetail.Add(orderDetail);
                    }
                    result.Add("Message", "Read Data Success");
                    result.Add("Data", listAllDetail);
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Route("readBy3")]
        [HttpGet]

        public IHttpActionResult ReadBy3(string cusID = null)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    List<CustomCustomerViewModel> listAllDetail = new List<CustomCustomerViewModel>();
                    var orderCus = db.Customers.AsQueryable();
                    var listProductDetail = db.Order_Details.ToList().Select(dt => new CustomOrderViewModel(dt)).ToList();

                    if (cusID != null)
                    {
                        orderCus = orderCus.Where(data => data.CustomerID == cusID);
                    }

                    var listCustomer = orderCus.AsEnumerable().ToList();
                    foreach (var item in listCustomer)
                    {
                        CustomCustomerViewModel orderAllData = new CustomCustomerViewModel(item);
                        listAllDetail.Add(orderAllData);
                    }
                    
                    result.Add("Message", "Read Data Success");
                    result.Add("Data", listAllDetail);
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