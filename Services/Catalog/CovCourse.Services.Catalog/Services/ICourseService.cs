using CovCourse.Services.Catalog.Dtos;
using CovCourse.Shared.Dtos;

namespace CovCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAll();
        Task<Response<CourseDto>> GetByIdAsync(string Id);
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string UserId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string Id);
    }
}
