using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models.DTO.ShoppingCart;
using LuckysDepartmentStore.Models.ViewModels.ShoppingCart;
using LuckysDepartmentStore.Service.Interfaces;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class ShippingService : IShippingService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        public const string CartSessionKey = "CartId";
        private readonly IHttpContextAccessor _httpContext;
        public Utility _utility;
        public IColorService _colorService;

        public ShippingService(LuckysContext context, IMapper mapper, IHttpContextAccessor httpContext,
            Utility utility, IColorService color)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _utility = utility;
            _colorService = color;
        }    

    }
}
