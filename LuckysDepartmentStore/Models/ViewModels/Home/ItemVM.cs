using LuckysDepartmentStore.Models.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Home
{
    public class ItemVM
    {
        public int ProductID { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public Decimal Price { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression(@"^(?!0$).*$", ErrorMessage ="ProductName cannot be zero.")]
        public int Quantity { get; set; }

        [Display(Name = "ProductName")]
        public string? ProductName { get; set; }
        public string? BrandSelection { get; set; }

        [Display(Name = "Color")]
        public List<string>? Color { get; set; }
        public List<ColorProductVM> ColorProduct { get; set; } = new List<ColorProductVM>();
        public int? ColorID { get; set; }
        public int? ColorSelection { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
        public int? DiscountID { get; set; }
        public byte[]? ProductPicture { get; set; }
        public string? ProductImage { get; set; }

        public List<Rating>? Ratings { get; set; }
        public int RatingsCount {  get; set; }

        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double? Stars { get; set; }
        public List<Sizes>? Sizes { get; set; } = new List<Sizes>();

        public int SizeSelection { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string? DiscountTag { get; set; }
        public Decimal SalePrice { get; set; }


    }
}
