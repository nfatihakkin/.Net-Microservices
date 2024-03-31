using CovCourse.Web.Models.Catalogs;
using FluentValidation;

namespace CovCourse.Web.Validators
{
    public class CourseUpdateInputValidator : AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Course Name is required !");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Course Description is required !");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Course Category is required !");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Duration is required !");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required !").ScalePrecision(2, 6).WithMessage("$$$$$$.$$ Format is required !");//$$$$$$.$$

        }
    }
}
