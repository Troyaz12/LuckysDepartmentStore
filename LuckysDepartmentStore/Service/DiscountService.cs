using AutoMapper;
using LuckysDepartmentStore.Data;
using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Models.DTO.Discount;
using LuckysDepartmentStore.Models.ViewModels.Discount;
using LuckysDepartmentStore.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LuckysDepartmentStore.Service
{
    public class DiscountService : IDiscountService
    {
        public LuckysContext _context;
        public IMapper _mapper;
        private readonly Utility _utility;

        public DiscountService(LuckysContext context, IMapper mapper, Utility utility)
        {
            _context = context;
            _mapper = mapper;
            _utility = utility;
        }     

        public ExecutionResult<int> DeleteDiscount(int discountId)
        {
            try
            {
                var discount = _context.Discounts.Find(discountId);

                if (discount == null)
                {
                    return ExecutionResult<int>.Failure("Unable to delete discount.");
                }

                _context.Discounts.Remove(discount);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ExecutionResult<int>.Failure("Unable to delete discount.");
            }

            return ExecutionResult<int>.Success(discountId);
        }

        public ExecutionResult<DiscountVM> GetDiscount(int discountId)
        {
            try
            {
                var discount = (
                   from Discount in _context.Discounts
                   join Category in _context.Categories on Discount.CategoryID equals Category.CategoryID into categories
                   from Category in categories.DefaultIfEmpty()
                   join SubCategory in _context.SubCategories on Discount.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                   from SubCategory in subCategories.DefaultIfEmpty()
                   join Brand in _context.Brand on Discount.BrandID equals Brand.BrandId into Brands
                   from Brand in Brands.DefaultIfEmpty()
                   where Discount.DiscountID == discountId
                   select new DiscountDTO
                   {
                       DiscountID = Discount.DiscountID,
                       DiscountPercent = Discount.DiscountPercent,
                       DiscountAmount = Discount.DiscountAmount,
                       DiscountActive = Discount.DiscountActive,
                       CreatedDate = Discount.CreatedDate,
                       DiscountArt = Discount.DiscountArt,
                       DiscountDescription = Discount.DiscountDescription,
                       SubCategory = SubCategory.SubCategoryName,
                       Category = Category.CategoryName,
                       ProductID = Discount.ProductID,
                       Brand = Brand.BrandName
                   }).FirstOrDefault();

                if (discount == null)
                {
                    return ExecutionResult<DiscountVM>.Failure("Unable to retrieve Discounts.");
                }

                var discountProducts = _mapper.Map<DiscountVM>(discount);

                discountProducts.DiscountImage = _utility.BytesToImage(discountProducts.DiscountArt);

                return ExecutionResult<DiscountVM>.Success(discountProducts);

            }
            catch
            {
                return ExecutionResult<DiscountVM>.Failure("Unable to retrieve discount details.");
            }
        }

        public void UpdateDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public async Task<Discount> CreateAsync(DiscountCreateVM discount)
        {

            try
            {
                if(discount.DiscountPercent != 0)
                {
                    discount.DiscountPercent = discount.DiscountPercent/100;
                }

                var newDiscount = _mapper.Map<Discount>(discount);
                newDiscount.DiscountArt = _utility.ImageBytes(discount.DiscountArtFile);

                _context.Add(newDiscount);
                await _context.SaveChangesAsync();

                return newDiscount;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error saving product to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }

        public ExecutionResult<List<DiscountVM>> GetActiveDiscounts()
        {
            try
            {
                var discount =
                    from Discount in _context.Discounts
                    join Category in _context.Categories on Discount.CategoryID equals Category.CategoryID into categories
                    from Category in categories.DefaultIfEmpty()
                    join SubCategory in _context.SubCategories on Discount.SubCategoryID equals SubCategory.SubCategoryID into subCategories
                    from SubCategory in subCategories.DefaultIfEmpty()
                    join Brand in _context.Brand on Discount.BrandID equals Brand.BrandId into Brands
                    from Brand in Brands.DefaultIfEmpty()
                    where Discount.DiscountActive == true
                    select new DiscountDTO
                    {
                        DiscountID = Discount.DiscountID,
                        DiscountPercent = Discount.DiscountPercent,
                        DiscountAmount = Discount.DiscountAmount,
                        DiscountActive = Discount.DiscountActive,
                        CreatedDate = Discount.CreatedDate,
                        DiscountArt = Discount.DiscountArt,
                        DiscountDescription = Discount.DiscountDescription,
                        SubCategory = SubCategory.SubCategoryName,
                        Category = Category.CategoryName,
                        ProductID = Discount.ProductID,
                        Brand = Brand.BrandName
                    };

                if (discount == null)
                {
                    return ExecutionResult<List<DiscountVM>>.Failure("Unable to retrieve Discounts.");
                }

                var discountProducts = _mapper.Map<List<DiscountVM>>(discount);
                var discountDTOList = discount.ToList();
                

                for (int x=0; x < discountProducts.Count; x++)
                {
                    discountProducts[x].DiscountImage = _utility.BytesToImage(discountDTOList[x].DiscountArt);
                }




                return ExecutionResult<List<DiscountVM>>.Success(discountProducts);

            }
            catch(Exception ex)
            {

                return ExecutionResult<List<DiscountVM>>.Failure("Unable to retrieve Discounts.");
            }

        }
    }
}
