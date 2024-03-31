﻿using CovCourse.Web.Models.Baskets;
using CovCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CovCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetAllCourseByIdAsync(courseId);
            var basketItem = new BasketItemViewModel { CourseId = courseId ,CourseName=course.Name,Price=course.Price };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBasketItem(string courseId)
        {
           await _basketService.RemoveBasketItem(courseId);
           return RedirectToAction(nameof(Index));
        }
    }
}