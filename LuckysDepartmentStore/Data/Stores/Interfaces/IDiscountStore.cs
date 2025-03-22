using LuckysDepartmentStore.Models;

namespace LuckysDepartmentStore.Data.Stores.Interfaces
{
    public interface IDiscountStore
    {
        Task<List<Discount>> DiscountWithTag();
    }
}
