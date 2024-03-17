using System;
using System.Collections.Generic;
using System.Text;

namespace CovCourse.Shared.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get;  }
    }
}
