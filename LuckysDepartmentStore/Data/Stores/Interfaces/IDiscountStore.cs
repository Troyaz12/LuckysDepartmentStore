using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.ViewModels.Discount;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IDiscountStore
    {
        public Task<List<Discount>> DiscountWithTag();
        public Task<Discount> GetDiscountByID(int discountID);
        public Task DeleteDiscounts(Discount discount);
        public Task<DiscountDTO> GetDiscountsWithSelections(int discountID);
        public Task<int> UpdateDiscount(DiscountEditVM discount);
        public Task AddDiscount(Discount discount);
        public Task<List<DiscountDTO>> GetAllDiscounts();
        public Task<List<Discount>> GetDiscountByTags(List<string> discountTags);
    }
}
