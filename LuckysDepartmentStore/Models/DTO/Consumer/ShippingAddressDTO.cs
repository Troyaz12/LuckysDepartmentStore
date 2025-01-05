namespace LuckysDepartmentStore.Models.DTO.Consumer
{
    public class ShippingAddressDTO
    {
        public int ShippingAddressID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
