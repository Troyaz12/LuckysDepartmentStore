using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models.DTO.Home;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
using Utility = LuckysDepartmentStore.Utilities.Utility;

namespace LuckysDepartmentStore.Data.Stores
{
    public class RatingsStore : IRatingsStore
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly IUtility _utility;
        public RatingsStore(LuckysContext context, IMapper mapper, IUtility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }
        public async Task<List<RatingsDTO>> RatingsByProductID(int productId)
        {
            var ratingProductDTO = await(
                   from Ratings in _context.Ratings
                   where Ratings.ProductID == productId
                   select new RatingsDTO
                   {
                       ProductID = Ratings.ProductID,
                       RatingID = Ratings.RatingID,
                       RatingValue = Ratings.RatingValue,
                       CreatedDate = Ratings.CreatedDate
                   }).ToListAsync();

            return ratingProductDTO;
        }
    }
}
