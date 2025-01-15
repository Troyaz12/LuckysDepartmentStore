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

        [Range(0.0, 1000.00, ErrorMessage = "Must be greater than zero, and 1000 or less.")]
        [Display(Name = "Amount")]
        [OneChoiceRequired("DiscountAmount", "DiscountPercent", ErrorMessage = "A discount must have either a dollar amount or percent.")]
        public decimal DiscountAmount { get; set; }
        [Display(Name = "Active")]
        public bool DiscountActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public IFormFile? DiscountArtFile { get; set; }

        [Display(Name = "Category")]
        public List<Category>? Category { get; set; }

        [OneChoiceRequired("CategorySelection", "SubCategorySelection", "BrandSelection", "ProductID", ErrorMessage = "A discount must have one or more of the following, Category, SubCategory, Brand, or ProductID.")]
        public string? CategorySelection { get; set; }
        public int? CategoryID { get; set; }

        [Display(Name = "SubCategory")]
        public List<SubCategory>? SubCategory { get; set; }

        [Display(Name = "SubCategory")]
        [OneChoiceRequired("CategorySelection", "SubCategorySelection", "BrandSelection", "ProductID", ErrorMessage = "A discount must have one of the following, Category, SubCategory, Brand, or ProductID.")]
        public string? SubCategorySelection { get; set; }
        public int? SubCategoryID { get; set; }

        [Display(Name = "Brand")]
        public List<Brand>? Brand { get; set; }

        [Display(Name = "Brand")]
        [OneChoiceRequired("CategorySelection", "SubCategorySelection", "BrandSelection", "ProductID", ErrorMessage = "A discount must have one of the following, Category, SubCategory, Brand, or ProductID.")]
        public string? BrandSelection { get; set; }

        public int? BrandID { get; set; }

        [OneChoiceRequired("CategorySelection", "SubCategorySelection", "BrandSelection", "ProductID", ErrorMessage = "A discount must have one of the following, Category, SubCategory, Brand, or ProductID.")]
        public int? ProductID { get; set; }

        [Display(Name = "Discount Description")]
        public string? DiscountDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [FutureDateCheck("ExpirationDate", ErrorMessage = "Expiration Date must be greater then today.")]
        public DateTime ExpirationDate { get; set; }
        public string? DiscountTag { get; set; }
    }
}
