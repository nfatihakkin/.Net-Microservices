using CovCourse.Services.FakePayment.Dtos;
using CovCourse.Shared.ControllerBases;
using CovCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            //paymentDto ile ödmee işlemi gerçekleştir.
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }

    }
}
