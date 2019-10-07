using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Northwind.EntityFrameworks;
using Northwind.ViewModels;

namespace Northwind.Controllers
{
    [RoutePrefix("api/Category")]
    
    public class CategoryController : ApiController
    {
        // API untuk Get(Read) Data
        [Route("readAll")]
        [HttpGet]
        public IHttpActionResult ReadAll()
        {
            // koneksi Database
            var db = new DB_Context();

            try
            {
                // mengambil semua data Entity dan mengubahnya menjadi List
                var listCategoryEntity = db.Categories.ToList();

                // variable List untuk menampung View Model
                List<CategoryViewModel> ListCategory = new List<CategoryViewModel>();

                // dictionary(map) untuk kebutuhan hasil response (optional)
                Dictionary<string, object> result = new Dictionary<string, object>();

                // mapping Entity ke View Model, dan menambahkan ke dalam list View Model
                foreach(var item in listCategoryEntity)
                {
                    CategoryViewModel category = new CategoryViewModel()
                    {
                        CategoryID = item.CategoryID,
                        CategpryName = item.CategoryName,
                        Description = item.Description,
                        Picture = item.Picture
                    };

                    // menambahkan object view model ke list view model
                    ListCategory.Add(category);
                };

                // menambahkan object ke dictoinary
                result.Add("Message", "Read data success");
                result.Add("Data", ListCategory);

                // menutup koneksi database
                db.Dispose();

                // return data (response)
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // API untuk Create Data
        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] CategoryViewModel dataBody)
        {
            var db = new DB_Context();
            try
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                // Object untuk menginisialisasi dengan data dari dataBody
                Category newCategory = new Category()
                {
                    CategoryID = dataBody.CategoryID,
                    CategoryName = dataBody.CategpryName,
                    Description = dataBody.Description,
                    //Picture = dataBody.Picture
                };

                // menambahkan category baru ke Category Entity Database
                db.Categories.Add(newCategory);

                // method yang digunakn untuk menyimpan perubahan baru di database
                db.SaveChanges();
                db.Dispose();
                result.Add("Message", "Insert Data Success ");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // API untuk Update Data
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] CategoryViewModel dataBody)
        {
            var db = new DB_Context();

            try
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                // Object untuk menginisialisasi dengan data dari database untuk mencari Primary Key dengan method Find
                Category category = db.Categories.Find(dataBody.CategoryID);

                // inisialisasi data yang akan dirubah
                category.CategoryID = dataBody.CategoryID;
                category.CategoryName = dataBody.CategpryName;
                category.Description = dataBody.Description;
                category.Picture = dataBody.Picture;

                db.SaveChanges();
                result.Add("Message", "Update Data Success");
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // API untuk Delete Data
        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int CatID)
        {
            var db = new DB_Context();

            try
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                // Object untuk menginisialisasi dengan data dari database untuk mencari Primary Key dengan method Where
                Category category = db.Categories.Where(data => data.CategoryID == CatID).FirstOrDefault();

                // remove/delete data yang dipilih
                db.Categories.Remove(category);

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
}