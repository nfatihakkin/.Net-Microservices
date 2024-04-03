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
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckoutInfoInput checkoutInfoInput)
        {
            //First way - sync
            //var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);

            //Second way - async
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);

            if (!orderSuspend.isSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderSuspend.Error;
                return View();
            }
            //First way - sync
            // return RedirectToAction(nameof(SuccessfulResult),new {orderId= orderSuspend.OrderId});

            //Second way - async
            return RedirectToAction(nameof(SuccessfulResult),new {orderId= new Random().Next(1,1000)});
        }
        public IActionResult SuccessfulResult(int orderId)
        {
            ViewBag.orderId = orderId; 
            return View();
        }
        public async Task<IActionResult> CheckoutHistory()
        {
            var orders = await _orderService.GetOrder();

         
           return View(orders);
        }
    }
}
