using CovCourse.Services.Catalog.Dtos;
using CovCourse.Services.Catalog.Models;
using CovCourse.Shared.Dtos;

namespace CovCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
        Task<Response<CategoryDto>> GetByIdAsync(string Id);
    }
}
