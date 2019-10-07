using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;

namespace Northwind.Controllers
{
    [RoutePrefix("api/Product")]

    public class ProductController : ApiController
    {
        // API untuk Read All Data
        [Route("readAll")]
        [HttpGet]
        public IHttpActionResult readAll()
        {
            using (var db = new DB_Context())
            {
                try
                {
                    var listProductEntity = db.Products.ToList();

                    List<ProductViewModel> ListProduct = new List<ProductViewModel>();
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (var item in listProductEntity)
                    {
                        ProductViewModel product = new ProductViewModel()
                        {
                            ProductID = item.ProductID,
                            ProductName = item.ProductName,
                            SupplierID = item.SupplierID,
                            CategoryID = item.CategoryID,
                            QuantityPerUnit = item.QuantityPerUnit,
                            UnitPrice = item.UnitPrice,
                            UnitsInStock = item.UnitsInStock,
                            UnitsOnOrder = item.UnitsOnOrder

                        };
                        ListProduct.Add(product);
                    };
                    result.Add("Message", "Read data success");
                    result.Add("Data", ListProduct);

                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        // API untuk Create Data
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] ProductViewModel dataBody)
        {
            using(var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    Product newProduct = new Product()
                    {
                        ProductID = dataBody.ProductID,
                        ProductName = dataBody.ProductName,
                        SupplierID = dataBody.SupplierID,
                        CategoryID = dataBody.CategoryID,
                        QuantityPerUnit = dataBody.QuantityPerUnit,
                        UnitPrice = dataBody.UnitPrice,
                        UnitsInStock = dataBody.UnitsInStock,
                        UnitsOnOrder = dataBody.UnitsOnOrder
                    };

                    db.Products.Add(newProduct);
                    db.SaveChanges();
                    result.Add("Message", "Insert Data Success ");

                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // API untuk Update Data
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] ProductViewModel dataBody)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();

                    // Object untuk menginisialisasi dengan data dari database untuk mencari Primary Key dengan method Find
                    Product product = db.Products.Find(dataBody.ProductID);

                    // inisialisasi data yang akan dirubah
                    product.ProductID = dataBody.ProductID;
                    product.ProductName = dataBody.ProductName;
                    product.SupplierID = dataBody.SupplierID;
                    product.CategoryID = dataBody.CategoryID;
                    product.QuantityPerUnit = dataBody.QuantityPerUnit;
                    product.UnitPrice = dataBody.UnitPrice;
                    product.UnitsInStock = dataBody.UnitsInStock;
                    product.UnitsOnOrder = dataBody.UnitsOnOrder;

                    db.SaveChanges();
                    result.Add("Message", "Update Data Success");
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        // API untuk Delete Data
        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int ProID)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();

                    // Object untuk menginisialisasi dengan data dari database untuk mencari Primary Key dengan method Where
                    Product product = db.Products.Where(data => data.ProductID == ProID).FirstOrDefault();

                    // remove/delete data yang dipilih
                    db.Products.Remove(product);

                    db.SaveChanges();
                    result.Add("Message", "Delete Data Success");
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Test1
        [Route("CusRead")]
        [HttpGet]
        public IHttpActionResult CusRead()
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    // Mencari product dengan harga termahal
                    var maxPrice = db.Products.Where(data => data.UnitPrice == db.Products.Max(data2 => data2.UnitPrice))
                        .ToList().Select(item => new ProductViewModel(item));
                    // Mencari product dengan ahrga termurah
                    var minPrice = db.Products.Where(data => data.UnitPrice == db.Products.Min(data2 => data2.UnitPrice))
                        .ToList().Select(item => new ProductViewModel(item));
                    // Mencari producut-product yang berada di bawah harga
                    var avg = db.Products.Average(data => data.UnitPrice);
                    var avgProduct=db.Products.Where(data => data.UnitPrice < avg).ToList().Select(item => new ProductViewModel(item));
                    
                    
                    result.Add("Most Expensive Product Price", maxPrice);
                    result.Add("Most Cheapest Product Price", minPrice);
                    result.Add("Product that price under average price", avgProduct);
                    //result.Add("Data", product);
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }  
        }

        // Test2 dan Jawaban Menang Perih
        [Route("Filter")]
        [HttpGet]
        public IHttpActionResult FilterBy(string nmPro = null,string nmCat = null, decimal? price = 0)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();

                    if (nmPro != null)
                    {
                        // Filter by Product Name
                        var listProductEntity = db.Products.ToList();
                        var listProduct = listProductEntity.Where(data => data.ProductName.ToLower().Contains(nmPro.ToLower()))
                            .Select(item => new ProductViewModel(item));
                        result.Add("Product Name group by: " + nmPro, listProduct);
                    }
                    
                    if (nmCat != null)
                    {
                        // Filter by Category Name
                        var listProductEntity = db.Products.ToList();
                        var listCategoryEntity = db.Categories.ToList();

                        var leftJoin = listProductEntity.Join(// outer sequence 
                            listCategoryEntity,  // inner sequence 
                            pro => pro.CategoryID,    // outerKeySelector
                            cat => cat.CategoryID,  // innerKeySelector
                            (pro, cat) => new // result selector
                            {
                                ProductID = pro.ProductID,
                                ProductName = pro.ProductName,
                                SupplierID = pro.SupplierID,
                                CategoryID = pro.CategoryID,
                                CategoryName = cat.CategoryName,
                                QuantityPerUnit = pro.QuantityPerUnit,
                                UnitPrice = pro.UnitPrice,
                                UnitsInStock = pro.UnitsInStock,
                                UnitsOnOrder = pro.UnitsOnOrder,
                            }).Where(cat => cat.CategoryName.ToLower().Contains(nmCat.ToLower()));

                        result.Add("Product that group by Category Name: " + nmCat, leftJoin);
                    }

                    if (price != 0)
                    {
                        // Filter by Price
                        var listPriceAll = db.Products.ToList();
                        var listPrice = listPriceAll.Where(dt => dt.UnitPrice < price)
                            .Select(item => new ProductViewModel(item));
                        result.Add("Price group by: " + price, listPrice);
                    }

                    //result.Add("Data", product);
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Test2 dan Jawaban Asli
        [Route("Filter2")]
        [HttpGet]
        public IHttpActionResult FilterBy2(string nmPro = null, string nmCat = null, decimal? price = 0)
        {
            using (var db = new DB_Context())
            {
                try
                {
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    var temp = db.Products.AsQueryable();
                    if (nmPro != null)
                    {
                        // Filter by Product Name
                        temp = temp.Where(data => data.ProductName.ToLower().Contains(nmPro.ToLower()));
                    }

                    if (nmCat != null)
                    {
                        // Filter by Category Name
                        temp = temp.Where(data => data.Category.CategoryName.ToLower().Contains(nmCat.ToLower()));
                    }

                    if (price != 0)
                    {
                        // Filter by Price
                        temp = temp.Where(data => data.UnitPrice < price);
                    }
                    var listProduct = temp.ToList().Select(item => new ProductViewModel(item));

                    result.Add("Message","Read Data Succes");
                    result.Add("Data", listProduct);
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