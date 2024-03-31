using CovCourse.Web.Models.Baskets;

namespace CovCourse.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel model);
        Task<bool> Delete();
        Task<bool> RemoveBasketItem(string courseId);
        Task<bool> ApplyDiscount(string discountCode);
        Task<bool> CancelApplyDiscount();
        Task<BasketViewModel> Get();
        Task AddBasketItem(BasketItemViewModel basketItemViewModel);
    }
}
