using AutoMapper;
using LuckysDepartmentStore.Data.Stores.Interfaces;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LuckysDepartmentStore.Data.Stores
{
    public class DiscountStore : IDiscountStore
    {
        public LuckysContext _context;
        private readonly IUtility _utility;

        public DiscountStore(LuckysContext context, IUtility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task<List<Discount>> DiscountWithTag()
        {
            var allDiscounts = await _context.Discounts
                                             .Where(d => d.DiscountTag != null)
                                             .ToListAsync();

            return allDiscounts;
        }

        public async Task<Discount> GetDiscountByID(int discountID)
        {
            var discount = await _context.Discounts.FindAsync(discountID);

            return discount;
        }

        public async Task DeleteDiscounts(Discount discount)
        {
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
        }

        public async Task<DiscountDTO> GetDiscountsWithSelections(int discountID)
        {
            var discount = await(
                  from Discount in _context.Discounts
                  join Category in _context.Categories on Discount.CategoryID equals Category.CategoryID into categories
                  from Category in categories.DefaultIfEmpty()
                  join SubCategory in _context.SubCategories on Discount.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                  from SubCategory in subCategories.DefaultIfEmpty()
                  join Brand in _context.Brand on Discount.BrandID equals Brand.BrandId into Brands
                  from Brand in Brands.DefaultIfEmpty()
                  where Discount.DiscountID == discountID
                  select new DiscountDTO
                  {
                      DiscountID = Discount.DiscountID,
                      DiscountPercent = Discount.DiscountPercent,
                      DiscountAmount = Discount.DiscountAmount,
                      DiscountActive = Discount.DiscountActive,
                      CreatedDate = Discount.CreatedDate,
                      DiscountArt = Discount.DiscountArt,
                      DiscountDescription = Discount.DiscountDescription,
                      SubCategorySelection = SubCategory.SubCategoryName,
                      CategorySelection = Category.CategoryName,
                      ProductID = Discount.ProductID,
                      BrandSelection = Brand.BrandName,
                      DiscountTag = Discount.DiscountTag,
                      ExpirationDate = Discount.ExpirationDate
                  }).FirstOrDefaultAsync();

            return discount;
        }

        public async Task<int> UpdateDiscount(DiscountEditVM discount)
        {
            var discountOld = await _context.Discounts.FindAsync(discount.DiscountID);

            if (discountOld == null)
            {
                return 0;
            }

            discountOld.DiscountPercent = discount.DiscountPercent;
            discountOld.DiscountAmount = discount.DiscountAmount;
            discountOld.DiscountActive = discount.DiscountActive;

            if (discount.DiscountArtFile != null)
            {
                discountOld.DiscountArt = _utility.ImageBytes(discount.DiscountArtFile);
            }

            discountOld.DiscountDescription = discount.DiscountDescription;
            discountOld.SubCategoryID = discount.SubCategoryID;
            discountOld.CategoryID = discount.CategoryID;
            discountOld.ProductID = discount.ProductID;
            discountOld.BrandID = discount.BrandID;
            discountOld.DiscountTag = discount.DiscountTag;
            discountOld.ExpirationDate = discount.ExpirationDate;

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }

        public async Task AddDiscount(Discount discount)
        {
            _context.Add(discount);
            await _context.SaveChangesAsync();

        }

        public async Task<List<DiscountDTO>> GetAllDiscounts()
        {
            var discount = await (
                    from Discount in _context.Discounts
                    join Category in _context.Categories on Discount.CategoryID equals Category.CategoryID into categories
                    from Category in categories.DefaultIfEmpty()
                    join SubCategory in _context.SubCategories on Discount.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                    from SubCategory in subCategories.DefaultIfEmpty()
                    join Brand in _context.Brand on Discount.BrandID equals Brand.BrandId into Brands
                    from Brand in Brands.DefaultIfEmpty()
                    where Discount.DiscountActive == true
                        && Discount.ProductID == null
                    select new DiscountDTO
                    {
                        DiscountID = Discount.DiscountID,
                        DiscountPercent = Discount.DiscountPercent,
                        DiscountAmount = Discount.DiscountAmount,
                        DiscountActive = Discount.DiscountActive,
                        CreatedDate = Discount.CreatedDate,
                        DiscountArt = Discount.DiscountArt,
                        DiscountDescription = Discount.DiscountDescription,
                        SubCategorySelection = SubCategory.SubCategoryName,
                        CategorySelection = Category.CategoryName,
                        ProductID = Discount.ProductID,
                        BrandSelection = Brand.BrandName,
                        DiscountTag = Discount.DiscountTag,
                        ExpirationDate = Discount.ExpirationDate
                    }).ToListAsync();

            return discount;
        }

        public async Task<List<Discount>> GetDiscountByTags(List<string> discountTags)
        {

            var upperTags = discountTags.Select(k => k.ToUpper()).ToList();

            var discounts = await(
                   from Discount in _context.Discounts
                   where !string.IsNullOrEmpty(Discount.DiscountTag) && upperTags.Contains(Discount.DiscountTag.ToUpper())
                   select Discount).ToListAsync();


                        

            return discounts;
        }
    }
}
