using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Product
{
    public class ProductVM
    {
        public int ProductID { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }
        [Display(Name = "Category")]
        public List<Category> Category { get; set; }
        public string CategorySelection { get; set; }
        public string CategorySelectionId { get; set; }
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Color")]
        public List<Color> Color { get; set; }

        public int ColorSelectionId {get;set;}
        public string ColorSelection { get; set; }
        [Display(Name = "SubCategory")]
        public List<SubCategory> SubCategory { get; set; }
        public string SubCategorySelection { get; set; }
        public string SubCategorySelectionId { get; set; }
        [Display(Name = "ProductPicture")]
        public byte[]? ProductPicture { get; set; }
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
