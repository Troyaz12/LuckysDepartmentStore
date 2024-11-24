using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;

namespace LuckysDepartmentStore.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public SubCategoryService(LuckysContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(ProductVM product)
        {
            var subCategory = new SubCategory();
            subCategory.SubCategoryName = product.SubCategorySelection;

            _context.Add(subCategory);
            var subCategoryResult = _context.SaveChanges();

            int newSubCategoryId = subCategory.SubCategoryID;

            return newSubCategoryId;
        }
    }
}
