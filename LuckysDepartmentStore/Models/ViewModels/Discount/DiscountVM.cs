using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Discount
{
    public class DiscountVM
    {
        [Display(Name = "Percent off")]
        public decimal DiscountPercent { get; set; }

        [Display(Name = "Amount")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Active")]
        public bool DiscountActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[]? DiscountArt { get; set; }

        [Display(Name = "Category")]
        public string? Category { get; set; }

        [Display(Name = "SubCategory")]
        public string? SubCategory { get; set; }

        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        public int? ProductID { get; set; }
        public string? DiscountImage { get; set; }
        public string? DiscountDescription { get; set; }
        public int DiscountID { get; set; }
        public string? Keywords { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        public string? BrandSelection { get; set; }
        public string? SubCategorySelection { get; set; }
        public string? CategorySelection { get; set; }
    }
}
