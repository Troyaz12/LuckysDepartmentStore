using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Discount;

namespace LuckysDepartmentStore.Service
{
    public interface IDiscountService
    {
        public void UpdateDiscount(Discount discount);
        public void DeleteDiscount(Discount discount);
        public Discount GetDiscount(int productID);
        public Task<Discount> CreateAsync(DiscountCreateVM discount);
    }
}
