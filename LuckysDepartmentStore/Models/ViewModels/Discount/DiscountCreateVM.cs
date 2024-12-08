using LuckysDepartmentStore.Utilities.Validation;
using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Discount
{
    public class DiscountCreateVM
    {
        [Display(Name = "Percent off")]
        [Range(0.0, 100.00, ErrorMessage = "Percent off must be between 0 and 100")]
        [OneChoiceRequired("DiscountAmount","DiscountPercent", ErrorMessage = "A discount must have either a dollar amount or percent.")]
        public decimal DiscountPercent { get; set; }

        [Range(0.0, 1000.00, ErrorMessage = "Percent off must be between 0 and 100")]
        [Display(Name = "Amount")]
        [OneChoiceRequired("DiscountAmount", "DiscountPercent", ErrorMessage = "A discount must have either a dollar amount or percent.")]
        public decimal DiscountAmount { get; set; }
        [Display(Name = "Active")]
        public bool DiscountActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public IFormFile? DiscountArtFile { get; set; }

        [Display(Name = "Category")]
        public List<Category>? Category { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string? CategorySelection { get; set; }
        public int? CategoryID { get; set; }

        [Display(Name = "SubCategory")]
        public List<SubCategory>? SubCategory { get; set; }

        [Display(Name = "SubCategory")]
        [Required(ErrorMessage = "SubCategory is required")]
        public string? SubCategorySelection { get; set; }
        public int? SubCategoryID { get; set; }

        [Display(Name = "Brand")]
        public List<Brand>? Brand { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Category is required")]
        public string? BrandSelection { get; set; }

        public int? BrandID { get; set; }

        public int? ProductID { get; set; }
        
    }
}
