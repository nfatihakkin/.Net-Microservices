using AutoMapper;
using CovCourse.Services.Catalog.Dtos;
using CovCourse.Services.Catalog.Models;
using CovCourse.Services.Catalog.Settings;
using CovCourse.Shared.Dtos;
using CovCourse.Shared.Messages;
using Mass = MassTransit;
using MongoDB.Driver;

namespace CovCourse.Services.Catalog.Services
{
    public class CourseService: ICourseService
    {
        private readonly IMongoCollection<Course> _courses;
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, Mass.IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courses = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categories = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAll()
        {
            var courses = await _courses.Find(course => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await  _categories.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string Id)
        {
            var course = await _courses.Find<Course>(c => c.Id == Id).FirstOrDefaultAsync();

            if (course == null)
                return Shared.Dtos.Response<CourseDto>.Fail("Bulunamadı",400);

            course.Category = await _categories.Find(c=>c.Id == course.CategoryId).FirstOrDefaultAsync();
            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(course),200);
        }
        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserIdAsync(string UserId)
        {
            var courses = await _courses.Find<Course>(c => c.UserId == UserId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses )
                {
                    course.Category = await _categories.Find(c => c.Id == course.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courses.InsertOneAsync(newCourse);
            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse),200);
        }
        public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courses.FindOneAndReplaceAsync(c=>c.Id== courseUpdateDto.Id,updateCourse);

            if (result == null)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course Not Found", 404);
            }

            await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = updateCourse.Id,UpdatedName=updateCourse.Name});
            return Shared.Dtos.Response<NoContent>.Success(204);
        }
        public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string Id)
        {
            var result = await _courses.DeleteOneAsync<Course>(c => c.Id == Id);

            if (result.DeletedCount<=0)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course Not Found", 404);
            }
            return Shared.Dtos.Response<NoContent>.Success(204);
        }
    }
}
