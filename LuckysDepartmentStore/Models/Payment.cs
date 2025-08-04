using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int CustomerID { get; set; }
        public bool IsProcessed { get; set; }
        public string ProcessMessage { get; set; }
        public string? RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CvcCode { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public bool IsCheckingAccount { get; set; }
        public bool IsCreditCard { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public decimal? Total { get; set; }
    }
}
