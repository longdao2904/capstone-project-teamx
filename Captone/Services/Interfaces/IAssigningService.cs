using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;

namespace Captone.Services.Interfaces
{
    public interface IAssigningService
    {
        void FindPath(Request request);
        Dictionary<Request, Dictionary<Trip, int>> Assigning(List<Request> request);
        bool CheckForward(List<Station> stations);
        void Update(List<int> requestIDs, int tripID);
    }
}