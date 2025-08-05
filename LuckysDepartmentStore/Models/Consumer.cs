using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class Consumer
    {
        public int ConsumerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
