using CovCourse.Web.Models;
using CovCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CovCourse.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signInInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _identityService.SignIn(signInInput);
            if (!response.IsSuccessful)
            {
                if (response.Errors != null && response.Errors.Count>0)
                {
                    response.Errors.ForEach(error => { ModelState.AddModelError(string.Empty, error); });
                }
                else
                {
                   ModelState.AddModelError(string.Empty, "Email or Password Wrong !");
                }
              
                return View();
             
            }
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
