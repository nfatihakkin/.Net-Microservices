using CovCourse.Web.Models.Discounts;

namespace CovCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string code);
    }
}
