using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Data.Stores
{
    public class SubcategoryStore : ISubcategoryStore
    {
        public LuckysContext _context;

        public SubcategoryStore(LuckysContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSubcategory(SubCategory subCategory)
        {
            _context.Add(subCategory);
            await _context.SaveChangesAsync();

            return subCategory.SubCategoryID;
        }
    }
}
