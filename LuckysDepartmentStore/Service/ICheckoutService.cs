using LuckysDepartmentStore.Models;

namespace LuckysDepartmentStore.Service
{
    public interface ICheckoutService
    {
        public int Order(Order order);
        public bool IsValid(int id, string user);
    }
}
