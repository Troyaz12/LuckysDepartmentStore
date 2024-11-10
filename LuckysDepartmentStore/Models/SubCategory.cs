namespace LuckysDepartmentStore.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set;}
        public string SubCategoryDescription { get; set;}
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
