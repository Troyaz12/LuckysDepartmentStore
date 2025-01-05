namespace LuckysDepartmentStore.Models.ViewModels.Consumer
{
    public class PaymentOptionsVM
    {
        public int PaymentOptionsID { get; set; }
        public int RoutingNumber { get; set; }
        public int AccountNumber { get; set; }
        public int CvcCode { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public bool IsCheckingAccount { get; set; }
        public bool IsCreditCard { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ProcessedDate { get; set; } = DateTime.Now;
    }
}
