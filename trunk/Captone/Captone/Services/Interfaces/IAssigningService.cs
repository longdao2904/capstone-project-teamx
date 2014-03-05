using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Services.Interfaces
{
    public interface IAssigningService
    {
        List<Route> FindPath(Request request);
        void Assigning(List<Request> requests, List<Trip> trips, DateTime date);
        bool CheckForward(string coordinateA, string coordinateB, string coordinateC);
        void Update(List<Request> requests, List<Trip> trips, DateTime date);
    }
}