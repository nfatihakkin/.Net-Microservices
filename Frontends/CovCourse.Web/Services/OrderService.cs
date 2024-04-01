using CovCourse.Shared.Dtos;
using CovCourse.Shared.Services;
using CovCourse.Web.Models.FakePayments;
using CovCourse.Web.Models.Orders;
using CovCourse.Web.Services.Interfaces;

namespace CovCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService,ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var payment = new PaymentInfoInput() 
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error="Payment could not be received !",isSuccessful = false };
            }

            var orderCreateInput = new OrderCreateInput() 
            { 
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput() 
                {
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Province = checkoutInfoInput.Province,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
            };

            basket.BasketItems.ForEach(x => {
                var orderItem = new OrderItemCreateInput() { ProductId = x.CourseId,Price = x.GetCurrentPrice,PictureUrl="",ProductName=x.CourseName };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders",orderCreateInput);
            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Order could not be created !", isSuccessful = false };
            }
            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
             orderCreatedViewModel.Data.isSuccessful = true;
            _basketService.Delete();
            return orderCreatedViewModel.Data;

        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
