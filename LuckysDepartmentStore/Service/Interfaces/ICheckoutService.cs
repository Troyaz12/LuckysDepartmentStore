using LuckysDepartmentStore.Models;

namespace LuckysDepartmentStore.Service.Interfaces
{
    public interface ICheckoutService
    {
        public Task<Utilities.ExecutionResult<OrderIds>> Order(Order order);
        public bool IsValid(int id, string user);
    }
}
