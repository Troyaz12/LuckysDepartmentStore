using AutoMapper;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {   
        public AutoMapperProfile()
        {
            CreateMap<ColorProductVM, ColorProduct>();

            CreateMap<ProductCreateVM, Product>();

            CreateMap<ProductVmDTO, ProductVM>()
                .ReverseMap();           
        }
    }
}
