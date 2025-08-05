using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string FirstNameShipping { get; set; }
        public string LastNameShipping { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public int Zip { get; set; }
        public string FirstNameBilling { get; set; }
        public string LastNameBilling { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CvcCode { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public int BillingZipCode { get; set; }
        public bool IsCheckingAccount { get; set; }
        public bool IsCreditCard { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
