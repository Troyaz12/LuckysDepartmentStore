namespace Lucky_sDepartmentStore.Models
{
    public class ColorProduct
    {
        public int ColorProductID { get; set; }
        public int ColorID { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
