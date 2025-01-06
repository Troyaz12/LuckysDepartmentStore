namespace LuckysDepartmentStore.Models
{
    public class Shipping
    {
        public int ShippingID { get; set; }       
        public DateTime? ShipDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }    
        public string state {  get; set; }
        public int Zip {  get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
