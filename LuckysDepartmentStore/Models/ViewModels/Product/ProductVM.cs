﻿using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Product
{
    public class ProductVM
    {
        public int ProductID { get; set; }

        [Display(Name = "ProductName")]      
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        public Decimal Price { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]        
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "SubCategory")]
        public string SubCategory { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }
       

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public byte[] ProductPicture { get; set; }
        public string? ProductImage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }

        [Display(Name = "Price")]
        public Decimal SalePrice { get; set; }
    }
}
