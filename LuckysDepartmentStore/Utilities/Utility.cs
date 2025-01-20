using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.ViewModels.Home;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LuckysDepartmentStore.Utilities
{
    public class Utility
    {
        private readonly IConfiguration _config;

        public Utility(IConfiguration config)
        {
            _config = config;
        }

        public ProductEditVM MapEditProduct(ProductEditDTO products)
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
            productsVM.DiscountTag = products.DiscountTags;
            productsVM.SearchWords = products.SearchWords;


            return productsVM;
        }
        public ProductDetailVM MapDetailProduct(ProductDetailDTO products)
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
            productsVM.ProductPicture = products.ProductPicture;
            productsVM.DiscountTags = products.DiscountTags;
            productsVM.SearchWords = products.SearchWords;

            return productsVM;
        }
        public string BytesToImage(byte[] imageBytes)
        {
            try
            {
                if (imageBytes == null)
                {
                    return DefaultImage();
                }
                else
                {
                    string image64 = Convert.ToBase64String(imageBytes);
                    string image = $"data:image/jpeg;base64,{image64}";

                    return image;
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Error during Convertion: " + ex.Message);
            }

        }
        public byte[] ImageBytes(IFormFile? fileImport)
        {
            byte[]? imageBytes = null;
            var defaultImage = _config["ImagePaths:ShoppingImageEmpty"];

            using (var memoryStream = new MemoryStream())
            {
                if (fileImport != null)
                {
                    //await file.form FormFile.CopyToAsync(memoryStream);
                    fileImport.CopyTo(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        imageBytes = memoryStream.ToArray();
                    }
                    else
                    {
                        throw new Exception("Insufficient Memory");
                    }
                }
                else
                {
                    var fileInfo = new FileInfo(defaultImage);
                    var fileStream = new FileStream(defaultImage, FileMode.Open);

                    var formFile = new FormFile(fileStream, 0, fileInfo.Length, fileInfo.Name, fileInfo.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg" // Or whatever the MIME type of the image is
                    };

                    //await file.form FormFile.CopyToAsync(memoryStream);
                    formFile.CopyTo(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        imageBytes = memoryStream.ToArray();
                    }
                    else
                    {
                        throw new Exception("Insufficient Memory");
                    }
                }
            }

            return imageBytes;
        }
        public string DefaultImage()
        {
            var defaultImage = _config["ImagePaths:ShoppingImageEmpty"];

            var pictureBase64 = Convert.ToBase64String(File.ReadAllBytes(defaultImage));
            string image = $"data:image/jpeg;base64,{pictureBase64}";

            return image;
        }
        public ItemVM MapDetailItem(ItemDTO item)
        {

            var itemVM = new ItemVM();

            itemVM.ProductID = item.ProductID;
            itemVM.ProductName = item.ProductName;
            itemVM.Price = item.Price;
            itemVM.Description = item.Description;
            itemVM.Quantity = item.Quantity;
            //itemVM.CategorySelection = item.Category;
            //itemVM.SubCategorySelection = item.SubCategory;
            itemVM.BrandSelection = item.Brand;
            //itemVM.CategoryID = item.CategoryId;
            //itemVM.SubCategoryID = item.SubCategoryId;
            //itemVM.BrandID = item.BrandId;
            itemVM.ProductPicture = item.ProductPicture;

            return itemVM;
        }
        public double ItemRating(List<RatingVM> ratings)
        {
            double ratingValTotal = 0;
            double totalPossible = 5 * ratings.Count;
            double starPercent = 0;
            double stars = 0;

            foreach (var rating in ratings)
            {
                ratingValTotal += rating.RatingValue;
            }

            starPercent = ratingValTotal / totalPossible;
            stars = 5 * starPercent;

            return stars;
        }
        public byte[] StringToBytes(string imageString)
        {
            try
            {
                if (imageString == null)
                {
                    return DefaultImageBytes();
                }
                else
                {
                    var data = imageString.Substring(imageString.IndexOf(",") + 1);
                    byte[] imageBytes = Convert.FromBase64String(data);

                    return imageBytes;
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Error during Convertion: " + ex.Message);
            }

        }
        public byte[] DefaultImageBytes()
        {
            var defaultImage = _config["ImagePaths:ShoppingImageEmpty"];

            byte[] imageBytes = Convert.FromBase64String(defaultImage);

            return imageBytes;
        }
        public decimal CalculateSalePrice(decimal? discountAmount, decimal? discountPercent, decimal price)
        {
            decimal salePrice = price;

            if (discountPercent != 0)
            {
                salePrice = price - (price * (decimal)discountPercent);
            }

            if (discountAmount != 0)
            {
                salePrice = salePrice - (decimal)discountAmount;
            }

            return salePrice;
        }
    }
}
