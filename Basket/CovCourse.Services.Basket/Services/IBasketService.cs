using CovCourse.Services.Basket.Dtos;
using CovCourse.Shared.Dtos;

namespace CovCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);


    }
}
