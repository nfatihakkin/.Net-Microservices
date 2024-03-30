using CovCourse.Shared.Dtos;
using CovCourse.Web.Helpers;
using CovCourse.Web.Models;
using CovCourse.Web.Models.Catalogs;
using CovCourse.Web.Services.Interfaces;

namespace CovCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService,PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFromFile);
            if (resultPhoto != null)
            {
                courseCreateInput.Photo = resultPhoto.Url;

            }
            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("course", courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourse(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"course/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("course");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccess.Data.ForEach(x => {
                x.StockPhotoUrl = _photoHelper.GetPhotoStockUrl(x.Photo);
            });
            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetAllCourseByIdAsync(string Id)
        {
            var response = await _httpClient.GetAsync($"course/{Id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            responseSuccess.Data.StockPhotoUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Photo);
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            responseSuccess.Data.ForEach(x=> {
                x.StockPhotoUrl = _photoHelper.GetPhotoStockUrl(x.Photo);
            });   

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhoto = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFromFile);
            if (resultPhoto != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Photo);
                courseUpdateInput.Photo = resultPhoto.Url;

            }
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("course", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
