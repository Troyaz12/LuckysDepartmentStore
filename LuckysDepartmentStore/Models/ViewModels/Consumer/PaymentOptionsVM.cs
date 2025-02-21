using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models.ViewModels.Consumer
{
    public class PaymentOptionsVM
    {
        public int PaymentOptionsID { get; set; }
        public int SelectedPaymentOptionsID { get; set; }

        [Display(Name ="First Name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Credit Card Number")]
        [Required(ErrorMessage = "Credit card number is required")]
        public string AccountNumber { get; set; }

        [Display(Name = "CVC Code")]
        [Required(ErrorMessage = "Cvc code is required")]
        [Range(1, int.MaxValue, ErrorMessage = "CVC code must be greater than 0")]
        public int CvcCode { get; set; }

        [Display(Name = "Billing Address")]
        [Required(ErrorMessage = "Billing address is required")]
        public string BillingAddress1 { get; set; }

        public string? BillingAddress2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip code is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ZipCode must be greater than 0")]
        public int ZipCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
    }
}
