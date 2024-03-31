using CovCourse.Shared.Dtos;
using CovCourse.Web.Models.Discounts;
using CovCourse.Web.Services.Interfaces;

namespace CovCourse.Web.Services
{
    public class DiscountService : IDiscountService 
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string code)
        {
            var response = await _httpClient.GetAsync($"discounts/getbycode/{code}");

            if (!response.IsSuccessStatusCode) return null;

            var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();

            return discount.Data;   
        }
    }
}
