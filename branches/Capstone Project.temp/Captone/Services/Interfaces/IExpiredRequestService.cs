using System;
using System.Collections.Generic;
using System.Linq;
using Captone.Models;

namespace Captone.Services.Interfaces
{
    public interface IExpiredRequestService
    {
        void ExpireRequest();
    }
}