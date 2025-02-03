using LuckysDepartmentStore.Models.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.DTO.ShoppingCart
{
    public class CartItemsDTO
    {
        public int ProductID { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression(@"^(?!0$).*$", ErrorMessage = "ProductName cannot be zero.")]
        public int Quantity { get; set; }

        [Display(Name = "ProductName")]
        public string? ProductName { get; set; }
        public int? ColorSelection { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
        public byte[]? ProductPicture { get; set; }
        public string? ProductImage { get; set; }
        public int SizeSelection { get; set; }
    }
}
