namespace LuckysDepartmentStore.Models.DTO.Home
{
    public class RatingsDTO
    {
        public int RatingID { get; set; }
        public double RatingValue { get; set; }
        public string Review { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
