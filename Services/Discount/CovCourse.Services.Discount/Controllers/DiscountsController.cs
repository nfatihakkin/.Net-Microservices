using CovCourse.Services.Discount.Services;
using CovCourse.Shared.ControllerBases;
using CovCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var discount = await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }
        [Route("/api/[controller]/[action]/{code}")]
        [HttpGet]
        public async Task<IActionResult> GetByCode(string code)
        {
            var discount = await _discountService.GetByUserId(code, _sharedIdentityService.GetUserId);
            return CreateActionResultInstance(discount);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            var result = await _discountService.Save(discount);
            return CreateActionResultInstance(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            var result = await _discountService.Update(discount);
            return CreateActionResultInstance(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _discountService.Delete(id);
            return CreateActionResultInstance(result);
        }

    }
}
