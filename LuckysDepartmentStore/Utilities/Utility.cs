using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Drawing;
using System.IO.Compression;

namespace LuckysDepartmentStore.Utilities
{
    public class Utility
    {      
        public static ProductEditVM MapEditProduct(ProductEditDTO products)
        {

            var productsVM = new ProductEditVM();

            productsVM.ProductID = products.ProductID;
            productsVM.ProductName = products.ProductName;
            productsVM.Price = products.Price;
            productsVM.Description = products.Description;
            productsVM.Quantity = products.Quantity;
            productsVM.CategorySelection = products.Category;
            productsVM.SubCategorySelection = products.SubCategory;
            productsVM.BrandSelection = products.Brand;
            productsVM.CategoryID = products.CategoryId;
            productsVM.SubCategoryID = products.SubCategoryId;
            productsVM.BrandID = products.BrandId;
            

            return productsVM;
        }
        public static ProductDetailVM MapDetailProduct(ProductDetailDTO products)
        {

            var productsVM = new ProductDetailVM();

            productsVM.ProductID = products.ProductID;
            productsVM.ProductName = products.ProductName;
            productsVM.Price = products.Price;
            productsVM.Description = products.Description;
            productsVM.Quantity = products.Quantity;
            productsVM.CategorySelection = products.Category;
            productsVM.SubCategorySelection = products.SubCategory;
            productsVM.BrandSelection = products.Brand;
            productsVM.CategoryID = products.CategoryId;
            productsVM.SubCategoryID = products.SubCategoryId;
            productsVM.BrandID = products.BrandId;


            return productsVM;
        }
        public static Image BytesToImage(byte[] imageBytes)
        {
            if(imageBytes == null)
            {
                throw new ArgumentNullException("Error during Convertion");
            }

            using (MemoryStream ms = new MemoryStream(imageBytes))
            { 
                return Image.FromStream(ms);            
            }
        }
    }
}
