using System.ComponentModel.DataAnnotations.Schema;

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

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
