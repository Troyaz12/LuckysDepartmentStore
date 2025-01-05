using LuckysDepartmentStore.Models;

namespace LuckysDepartmentStore.Service
{
    public interface ICheckoutService
    {
        public Task<Utilities.ExecutionResult<OrderIds>> Order(Order order);
        public bool IsValid(int id, string user);        
    }
}
