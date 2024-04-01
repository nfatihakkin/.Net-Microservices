using CovCourse.Web.Models.Orders;

namespace CovCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrder();
    }
}
