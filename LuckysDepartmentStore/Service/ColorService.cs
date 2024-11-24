using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class ColorService : IColorService
    {
        public LuckysContext _context;
        public IMapper _mapper;

        public ColorService(LuckysContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(string name)
        {
            var newColor = new Color();
            newColor.Name = name;

            _context.Add(newColor);
            var ColorResult = _context.SaveChanges();

            int newColorId = newColor.ColorID;

            return newColorId;
        }
        //public ProductVM AddProductColor(ProductVM productVm)
        //{

        //    // check for a new color, if there is one then add it to colors.
        //    if(productVm != null)
        //    {
        //        for(int x=0; x > productVm.ColorProduct.Count; x++)
        //        {
        //            if (productVm.ColorProduct[x].ColorID == 0)
        //            {
        //                var newColor = new Color();
        //                newColor.Name = productVm.ColorProduct[x].Name;

        //                _context.Add(newColor);
        //                var ColorResult = _context.SaveChanges();

        //                int newColorId = newColor.ColorID;
        //                productVm.ColorProduct[x].ColorID = newColorId;
        //            }
        //        }

        //        _context.Add(pro);

        //    }

        //    //add to colorProductId



        //    return productVm;
        //}
    }
}
