using CovCourse.Shared.Dtos;
using CovCourse.Web.Models;
using IdentityModel.Client;

namespace CovCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RefreshToken();
    }
}
