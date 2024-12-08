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
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte[]? DiscountArt { get; set; }

        [Display(Name = "Category")]
        public string? Category { get; set; }

        [Display(Name = "SubCategory")]
        public string? SubCategory { get; set; }
        
        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        public int? ProductID { get; set; }
    }
}
