namespace LuckysDepartmentStore.Models
{
    public class Order
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public int Zip { get; set; }
   //     public int ProductID { get; set; }
    //    public Decimal Price { get; set; }
 //       public int Quantity { get; set; }
        public int RoutingNumber { get; set; }
        public int AccountNumber { get; set; }
        public int CvcCode { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public int BillingZipCode { get; set; }
        public bool IsCheckingAccount { get; set; }
        public bool IsCreditCard { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
