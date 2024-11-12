using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;
using SQLitePCL;

namespace LuckysDepartmentStore.Service
{
    public class ProductService : IProductService
    {
        public LuckysContext _context;
        public ProductService(LuckysContext context)
        {
            _context = context;
        }
        public async Task Create(ProductVM product)
        {
            

            _context.Add(product);
            await _context.SaveChangesAsync();
        }
        public T GetItemAs<T> (object obj) where T : class 
        { 
           


            if(obj.GetType() == typeof(ProductVM))
            {
                ProductVM productVm = new ProductVM();
                productVm = (ProductVM) obj;

                var color = _context.Colors
                         .Where(p => p.Name.ToLower() == productVm.Color.ToLower())
                         .ToList();

                Product product = new Product();
                product.ColorProductID = color[0].ColorID;


                return product as T;
            }




            return obj as T;

        }
      
    }
}