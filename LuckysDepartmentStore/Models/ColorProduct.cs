﻿namespace LuckysDepartmentStore.Models
{
    public class ColorProduct
    {
        public int ColorProductID { get; set; }
        public int ColorID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
    }
}
