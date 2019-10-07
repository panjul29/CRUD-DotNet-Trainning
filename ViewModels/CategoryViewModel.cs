using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        public string CategpryName { get; set; } 

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category entity)
        {
            CategoryID = entity.CategoryID;
            CategpryName = entity.CategoryName;
            Description = entity.Description;
            Picture = entity.Picture;
        }
    }
}