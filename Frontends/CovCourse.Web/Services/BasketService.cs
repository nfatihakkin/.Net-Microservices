﻿using CovCourse.Shared.Dtos;
using CovCourse.Web.Models.Baskets;
using CovCourse.Web.Services.Interfaces;

namespace CovCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await Get();

            if (basket != null)
            {
                if (!basket.BasketItems.Any(x=>x.CourseId==basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems = new List<BasketItemViewModel>();
                basket.BasketItems.Add(basketItemViewModel);
            }
            await SaveOrUpdate(basket);
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete()
        {
            var result = await _httpClient.DeleteAsync("baskets");
            return result.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("baskets");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();
            if (basket == null) return false;

            var deletedItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (deletedItem == null) return false;
       
            var deleteResult = basket.BasketItems.Remove(deletedItem);
            if (!deleteResult) return false;

            if (!basket.BasketItems.Any()) basket.DiscountCode = null;
            
            return await SaveOrUpdate(basket);
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets",model);

            return response.IsSuccessStatusCode;
        }
    }
}
