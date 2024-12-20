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
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Display(Name = "ProductName")]
        [Required(ErrorMessage = "Name is required")]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        public List<Category>? Category { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string? CategorySelection { get; set; }
        public int? CategoryID { get; set; }

        [Display(Name = "Brand")]
        public List<Brand>? Brand { get; set; }
        public int? BrandID { get; set; }
        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Category is required")]
        public string? BrandSelection { get; set; }

        [Display(Name = "Color")]
        public List<string>? Color { get; set; }
        public List<ColorProductVM> ColorProduct { get; set; } = new List<ColorProductVM>();
        public int? ColorID { get; set; }
        public string? ColorSelection { get; set; }

        [Display(Name = "SubCategory")]
        public List<SubCategory>? SubCategory { get; set; }
        [Display(Name = "SubCategory")]
        [Required(ErrorMessage = "SubCategory is required")]
        public string? SubCategorySelection { get; set; }

        public int? SubCategoryID { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }
        public int? DiscountID { get; set; }
        public byte[]? ProductPicture { get; set; }
        public string? ProductImage { get; set; }

        public List<Rating> Ratings { get; set; }
        public int RatingsCount {  get; set; }

        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double? Stars { get; set; }
        public List<Sizes>? Sizes { get; set; } = new List<Sizes>();

    }
}
