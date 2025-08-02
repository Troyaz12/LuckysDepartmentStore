using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LuckysDepartmentStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ShippingAddress> ShippingAddress { get; set; }
        public virtual ICollection<PaymentOptions> PaymentOptions { get; set; }
        public virtual Consumer Consumer { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
