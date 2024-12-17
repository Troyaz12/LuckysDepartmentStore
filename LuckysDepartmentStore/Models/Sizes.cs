using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models
{
    public class Sizes
    {
        [Key]
        public int SizesID { get; set; }

        public string Size {  get; set; }
    }
}
