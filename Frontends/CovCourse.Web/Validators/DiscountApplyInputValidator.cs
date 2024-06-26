﻿using CovCourse.Web.Models.Discounts;
using FluentValidation;

namespace CovCourse.Web.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x=>x.Code).NotEmpty().WithMessage("Code is required !");
        }
    }
}
