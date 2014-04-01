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
        Dictionary<Request, List<Trip>> Assigning(List<Request> requests, DateTime date);
        bool CheckForward(List<Station> stations);
        //void Update(List<Request> requests, List<Trip> trips, DateTime date);
        //void Update(List<Request> requests, int tripID);
        void Update(List<int> requestIDs, int tripID);
    }
}