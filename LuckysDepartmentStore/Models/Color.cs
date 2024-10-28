namespace Lucky_sDepartmentStore.Models
{
    public class Color
    {
        public int ColorID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
