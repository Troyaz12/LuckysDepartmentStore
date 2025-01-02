using Microsoft.AspNetCore.Identity;

namespace LuckysDepartmentStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Customer Customer { get; set; }
    }
}
