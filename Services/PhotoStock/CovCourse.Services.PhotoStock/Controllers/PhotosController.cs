﻿using CovCourse.Services.PhotoStock.Dtos;
using CovCourse.Shared.ControllerBases;
using CovCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length>0) {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = photo.FileName;
                    
                PhotoDto photoDto = new () { Url = returnPath};

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            else return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty !", 400));

        }
        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var photo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(photo))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo is empty !", 400));
            }
            else
            {
                System.IO.File.Delete(photo);
                return CreateActionResultInstance(Response<NoContent>.Success(204));
            }
        }
    }
}
