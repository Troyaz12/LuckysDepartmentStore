using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class CategoryService : ICategoryService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public CategoryService(LuckysContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(ProductVM product)
        {
            var newCategory = new Category();
            newCategory.CategoryName = product.CategorySelection;
            newCategory.CategoryDescription = product.CategorySelection;

            _context.Add(newCategory);
            var categoryResult = _context.SaveChanges();

            int newCategoryId = newCategory.CategoryID;

            return newCategoryId;
        }
    }
}
