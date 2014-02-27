using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Services.Interfaces
{
    public interface IAssigningService
    {
        List<Route> ProcessingRequest(Request requests);
        bool CheckForward(string coordinateA, string coordinateB, string coordinateC);
    }
}