using CovCourse.Web.Models.Orders;
using CovCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CovCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }
        
        public async Task<IActionResult> CheckOut(CheckoutInfoInput checkoutInfoInput)
        {
            var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            if (!orderStatus.isSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderStatus.Error;
                return View();
            }
            return RedirectToAction(nameof(SuccessfulResult),new {orderId=orderStatus.OrderId});
        }
        public IActionResult SuccessfulResult(int orderId)
        {
            ViewBag.orderId = orderId; 
            return View();
        }
    }
}
