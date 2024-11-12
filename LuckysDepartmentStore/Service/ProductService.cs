using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Service;

namespace LuckysDepartmentStore.Service
{
    public class ProductService(LuckysContext _context, IMapper _mapper) : IProductService
    {
        public async Task Create(ProductVM product)
        {
            

            _context.Add(product);
            await _context.SaveChangesAsync();
        }
        public T GetItemAs<T> (object obj) where T : class 
        { 
           


            if(obj.GetType() == typeof(ProductVM))
            {
                Product product = new Product();
                product = (Product) obj;

                var color = _context.Colors
                         .Where(p => p.Name.ToLower() == product.ProductName.ToLower())
                         .ToList();



                return (T)Convert.ChangeType(product, typeof(T));
            }
                



            return (T)Convert.ChangeType(obj, typeof(T));

        }
      
    }
}