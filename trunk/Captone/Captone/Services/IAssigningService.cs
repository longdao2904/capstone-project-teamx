using System;
namespace Captone.Services
{
    interface IAssigningService
    {
        bool checkForward(string coordinateA, string coordinateB, string coordinateC);
        System.Collections.Generic.List<Captone.Models.Route> ProcessingRequest(Captone.Models.Request request);
    }
}
