using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LuckysDepartmentStore.Models.ViewModels.Consumer
{
    public class ShippingAddressVM
    {
        public int ShippingAddressID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address1 is required")]
        public string Address1 { get; set; }
        public string? Address2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "ZipCode is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ZipCode is required")]
        public int ZipCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;        
        public string? UserId { get; set; }        
        public virtual ApplicationUser? User { get; set; }
        public int SelectedShippingAddressID { get; set; }
    }
}
