using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public required decimal DiscountPercent { get; set; }
        public required decimal DiscountAmount { get; set; }
        public required bool DiscountActive {  get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public byte[]? DiscountArt { get; set; }
        public string? DiscountDescription { get; set; }
        public int? SubCategoryID { get; set; }
        public int? CategoryID { get; set; }
        public int? ProductID { get; set; }
        public int? BrandID { get; set; }
        public DateTime ExpirationDate { get; set; }        
        public required string? DiscountTag { get; set; }
    }
}
