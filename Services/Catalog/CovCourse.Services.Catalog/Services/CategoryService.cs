﻿using AutoMapper;
using CovCourse.Services.Catalog.Dtos;
using CovCourse.Services.Catalog.Models;
using CovCourse.Services.Catalog.Settings;
using CovCourse.Shared.Dtos;
using MongoDB.Driver;

namespace CovCourse.Services.Catalog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client =new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categories = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categories.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categories.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        public async Task<Response<CategoryDto>> GetByIdAsync(string Id)
        {
            var category = await _categories.Find<Category>(c=>c.Id == Id).FirstOrDefaultAsync();

            if (category == null) return Response<CategoryDto>.Fail("Category Not Found",404);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
