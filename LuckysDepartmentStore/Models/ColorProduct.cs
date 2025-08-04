using System.ComponentModel.DataAnnotations.Schema;

namespace LuckysDepartmentStore.Models
{
    public class ColorProduct
    {
        public int ColorProductID { get; set; }
        public int ColorID { get; set; }
        public int Quantity { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
    }
}
