using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Product
{
    public class ProductVM
    {
        public int? ProductID { get; set; }
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
        public int? CategoryId { get; set; }
        [Display(Name = "Brand")]
        public List<Brand> Brand { get; set; }
        public int? BrandId { get; set; }
        public string BrandSelection { get; set; }
        [Display(Name = "Color")]
        public List<Color> Color { get; set; }
        public int? ColorId {get;set;}
        public string ColorSelection { get; set; }
        [Display(Name = "SubCategory")]
        public List<SubCategory> SubCategory { get; set; }
        public string SubCategorySelection { get; set; }
        public int? SubCategoryId { get; set; }
        [Display(Name = "ProductPicture")]
        public IFormFile? ProductPictureFile { get; set; }
        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
