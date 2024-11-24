using LuckysDepartmentStore.Models;

namespace LuckysDepartmentStore.Service
{
    public interface IDiscountService
    {
        public void AddDiscount(Discount discount);
        public void UpdateDiscount(Discount discount);
        public void DeleteDiscount(Discount discount);
        public Discount GetDiscount(int productID);
    }
}
