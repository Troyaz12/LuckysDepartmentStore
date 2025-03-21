﻿namespace LuckysDepartmentStore.Models.DTO.Discount
{
    public class DiscountDTO
    {
        public int DiscountID { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool DiscountActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[]? DiscountArt { get; set; }
        public string? DiscountDescription { get; set; }
        public string? SubCategorySelection { get; set; }
        public string? CategorySelection { get; set; }
        public int? ProductID { get; set; }
        public string? BrandSelection { get; set; }
        public string? DiscountTag { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
