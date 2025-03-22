using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Data.Stores
{
    public class CategoryStore : ICategoryStore
    {
        public LuckysContext _context;

        public CategoryStore(LuckysContext context)
        {
            _context = context;
        }
        public async Task<int> CreateCategory(ProductCreateVM product)
        {
            var newCategory = new Category();
            newCategory.CategoryName = product.CategorySelection;
            newCategory.CategoryDescription = product.CategorySelection;

            _context.Add(newCategory);
            var categoryResult = await _context.SaveChangesAsync();

            int newCategoryId = newCategory.CategoryID;

            return newCategoryId;

        }
    }
}
