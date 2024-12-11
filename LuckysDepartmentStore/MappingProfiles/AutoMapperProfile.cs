using AutoMapper;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Utilities;

namespace LuckysDepartmentStore.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {   
        public AutoMapperProfile()
        {
            CreateMap<ColorProductVM, ColorProduct>()
                .ReverseMap();

            CreateMap<ProductCreateVM, Product>();

            CreateMap<ProductVmDTO, ProductVM>()
                .ReverseMap();

            CreateMap<ProductEditVM, Product>()
                .ReverseMap();

            CreateMap<ColorProductEditDTO, ColorProductVM>()
                .ReverseMap();

            CreateMap<ProductEditDTO, ProductVM>()
                .ReverseMap();

            CreateMap<ColorProductDetailDTO, ColorProductVM>()
                .ReverseMap();

            CreateMap<Discount, DiscountCreateVM>()
                .ReverseMap();

            CreateMap<DiscountDTO, DiscountVM>()
               .ForMember(dest => dest.DiscountImage, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
