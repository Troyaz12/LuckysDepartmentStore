using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class ShippingAddress
    {
        public int ShippingAddressID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string State {  get; set; }
        public int ZipCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
