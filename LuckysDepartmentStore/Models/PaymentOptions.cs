using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class PaymentOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaymentOptionsID { get; set; }
        public string? RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CvcCode {  get; set; }
        public string BillingAddress1 { get; set; }
        public string? BillingAddress2 {  get; set; }
        public string City { get; set; }
        public string State {  get; set; }
        public int ZipCode { get; set; }
        public bool IsCheckingAccount { get; set; }
        public bool IsCreditCard { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
