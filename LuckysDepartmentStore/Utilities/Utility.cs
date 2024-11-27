using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.IO.Compression;

namespace LuckysDepartmentStore.Utilities
{
    public class Utility
    {
        //public IFormFile FileUpload { get; private set; }
        //public IConfiguration _config { get; private set; }

        //public Utility(IConfiguration config)
        //{
        //    _config = config;
        //}

        //public Result<byte[]> ImageBytes(IFormFile? fileImport)
        //{
        //    byte[]? imageBytes = null;
        //    var defaultImage = _config["ImagePaths:ShoppingImageEmpty"];

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        if (fileImport != null)
        //        {
        //            //await file.form FormFile.CopyToAsync(memoryStream);
        //            fileImport.CopyTo(memoryStream);
        //            // Upload the file if less than 2 MB
        //            if (memoryStream.Length < 2097152)
        //            {
        //                imageBytes = memoryStream.ToArray();
        //            }
        //            else
        //            {
        //                return Result<byte[]>.Failure("Could not process the image.");
        //            }
        //        }
        //        else
        //        {
        //            var fileInfo = new FileInfo(defaultImage);
        //            var fileStream = new FileStream(defaultImage, FileMode.Open);

        //            var formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
        //            {
        //                Headers = new HeaderDictionary(),
        //                ContentType = "image/jpeg" // Or whatever the MIME type of the image is
        //            };

        //            //await file.form FormFile.CopyToAsync(memoryStream);
        //            formFile.CopyTo(memoryStream);
        //            // Upload the file if less than 2 MB
        //            if (memoryStream.Length < 2097152)
        //            {
        //                imageBytes = memoryStream.ToArray();
        //            }
        //            else
        //            {
        //                return Result<byte[]>.Failure("Could not process the image.");
        //            }
        //        }
        //    }

        //    return Result<byte[]>.Success(imageBytes);
        //}
        //    public static List<ProductVM> MapProduct(List<Product> products)
        //    {

        //        var productsVM = new List<ProductVM>();

        //        foreach(Product product in products) 
        //        {
        //            ProductVM productVM = new ProductVM();

        //            productVM.Price = product.Price;
        //            productVM.ProductID = product.ProductID;
        //            productVM.Description = product.Description;
        //            productVM.Quantity = product.Quantity;
        //            productVM.ProductName = product.ProductName;
        //            productVM.CategoryID = product.CategoryID;
        //            productVM.BrandID = product.BrandID;
        //            productVM.SubCategoryID = product.SubCategoryID;
        //            productVM.ProductPicture = product.ProductPicture;
        //            productVM.CreatedDate = product.CreatedDate;
        //            productVM.DiscountID = product.DiscountID;

        //            productsVM.Add(productVM);
        //        }

        //        return productsVM;
        //    }
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

            return productsVM;
        }
    }
    public class AppFile
    {
        //public int Id { get; set; }
        //public byte[] Content { get; set; }
    }
  

}
