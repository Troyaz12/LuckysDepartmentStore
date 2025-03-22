using LuckysDepartmentStore.Data.Stores.Interfaces;

namespace LuckysDepartmentStore.Data.Stores
{
    public class CheckoutStore : ICheckoutStore
    {
        public LuckysContext _context;

        public CheckoutStore(LuckysContext context)
        {
            _context = context;
        }

    }
}
