﻿namespace CovCourse.Web.Models.Catalogs
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public string UserId { get; set; }
        public FeatureViewModel Feature { get; set; }
        public string CategoryId { get; set; }
        public IFormFile PhotoFromFile { get; set; }
    }
}
