using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Utilities
{
    public interface IUtility
    {
        public ProductEditVM MapEditProduct(ProductDTO products);
        public ProductDetailVM MapDetailProduct(ProductDTO products);
        public string BytesToImage(byte[] imageBytes);
        public byte[] ImageBytes(IFormFile? fileImport);
        public string DefaultImage();
        public ItemVM MapDetailItem(ItemDTO item);
        public double ItemRating(List<RatingVM> ratings);
        public byte[] StringToBytes(string imageString);
        public byte[] DefaultImageBytes();
        public decimal CalculateSalePrice(decimal? discountAmount, decimal? discountPercent, decimal price);
        public decimal CalculateItemSubtotal(int quantity, decimal salePrice);
        public bool IsSearchStringValid(string searchString, out string errorMessage);
    }
}
