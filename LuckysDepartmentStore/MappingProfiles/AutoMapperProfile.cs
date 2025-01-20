using AutoMapper;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Consumer;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Models.DTO.Products;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.Consumer;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Models.ViewModels.Home;
using LuckysDepartmentStore.Models.ViewModels.Product;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
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

            CreateMap<DiscountEditVM, DiscountVM>()
                .ReverseMap();

            CreateMap<DiscountEditVM, RatingsDTO>()
               .ReverseMap();

            CreateMap<ColorProductItemDTO, ColorProductVM>()
                .ReverseMap();

            CreateMap<RatingsDTO, RatingVM>()
                .ReverseMap();

            CreateMap<ShippingAddressDTO, ShippingAddressVM>()
              .ReverseMap();

            CreateMap<PaymentOptionsVM, PaymentOptions>()
             .ReverseMap();

            CreateMap<PaymentOptionsDTO, PaymentOptionsVM>()
              .ReverseMap();

            CreateMap<Carts, CartsVM>()
             .ReverseMap();

            CreateMap<CartsDTO, CartsVM>()
             .ReverseMap();

            CreateMap<Product, ProductVM>()
             .ReverseMap();

            CreateMap<ItemDTO, ItemVM>()
            .ReverseMap();
        }
    }
}