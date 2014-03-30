using System;
using System.Collections.Generic;
using System.Linq;
using Captone.Models;
using Captone.Repositories;
using Captone.Services.Interfaces;
using System.Web.Services;

namespace Captone.Services
{
    public class AssigningService : IAssigningService
    {
        #region Declareration
        //declare interface
        private readonly GenericRepository<Route> _routeRepository =
            new GenericRepository<Route>(new iDeliverEntities());
        private readonly GenericRepository<Station> _stationRepository =
            new GenericRepository<Station>(new iDeliverEntities());
        private readonly GenericRepository<Invoice> _invoiceRepository =
            new GenericRepository<Invoice>(new iDeliverEntities());
        private readonly GenericRepository<Trip> _tripRepository =
            new GenericRepository<Trip>(new iDeliverEntities());
        private readonly GenericRepository<Request> _requestRepository =
            new GenericRepository<Request>(new iDeliverEntities());
        private readonly GenericRepository<Assigning> _assigningRepository =
            new GenericRepository<Assigning>(new iDeliverEntities());
        private readonly GenericRepository<Stage> _stageRepository =
            new GenericRepository<Stage>(new iDeliverEntities());
        private readonly GenericRepository<RouteStage> _routeStageRepository =
            new GenericRepository<RouteStage>(new iDeliverEntities());
        //declare class
        private List<Invoice> _invoices = new List<Invoice>();
        private List<Station> _stations = new List<Station>();
        private List<Route> _routes = new List<Route>();
        private List<Trip> _trips = new List<Trip>();
        private List<Assigning> _assignings = new List<Assigning>();
        private List<Stage> _stages = new List<Stage>();
        private List<RouteStage> _routeStages = new List<RouteStage>();
        private Dictionary<Route, List<Stage>> _stageOfRoute = new Dictionary<Route, List<Stage>>();
        //declare const
        private readonly TimeSpan _maxTime = new TimeSpan(5, 0, 0, 0);
        private readonly int _maxWay = 5;
        private readonly TimeSpan _deltaTime = new TimeSpan(0,45,0);
        //result of route
        List<Dictionary<Request, List<Stage>>> _tmpResult = new List<Dictionary<Request, List<Stage>>>();
        //result of trip
        Dictionary<Request, List<Trip>> _finalResult = new Dictionary<Request, List<Trip>>();
        //adjancent list of station
        Dictionary<Station, List<Station>> adj = new Dictionary<Station, List<Station>>();

        public AssigningService(GenericRepository<Route> routeRepository
            , GenericRepository<Station> stationRepository
            , GenericRepository<Invoice> invoiceRepository
            , GenericRepository<Trip> tripRepository
            , GenericRepository<Request> requestRepository
            , GenericRepository<Assigning> assigningRepository
            , GenericRepository<Stage> stageRepository
            , GenericRepository<RouteStage> routeStageRepository)
        {
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;
            _invoiceRepository = invoiceRepository;
            _tripRepository = tripRepository;
            _requestRepository = requestRepository;
            _assigningRepository = assigningRepository;
            _stageRepository = stageRepository;
            _routeStageRepository = routeStageRepository;
        }
        #endregion
        public AssigningService()
        {
            //Constructor without argument
        }
        private void InitData()
        {
            //load all relative data in the database to memory
            _routes = _routeRepository.GetAll().ToList();
            _stations = _stationRepository.GetAll().ToList();
            _invoices = _invoiceRepository.GetAll().ToList();
            _stages = _stageRepository.GetAll().ToList();
            _assignings = _assigningRepository.GetAll().ToList();
            _trips = _tripRepository.GetAll().ToList();
            _routeStages = _routeStageRepository.GetAll().ToList();
            //add the list of stages for each route
            MakePairRouteStage();
            //right after list all trips from database, filter them to receive the availabel trip
            FilterTrip();
            //build the adjancient list
            BuildGraph();
        }
        private void BuildGraph()
        {
            //build the adjancient list in which there is a stage connect two stations
            foreach (var station1 in _stations)
            {
                var tmp = new List<Station>();
                foreach (var station2 in _stations)
                {
                    tmp.AddRange(from t in _stages where t.FromLocation == station1.StationID && 
                                     t.ToLocation == station2.StationID select station2);
                }
                //insert the current adjance list of station to the full list
                adj.Add(station1, tmp);
            }
        }
        //accept trips have departure time later from now 45 min and current status is active 
        public void FilterTrip()
        {
            var tmpList = _trips.Where(trip => ChangeTime(trip.Date, trip.EstimateDepartureTime) - DateTime.Now >= 
                _deltaTime && trip.IsActive).ToList();
            _trips = tmpList;
        }
        //find the list of route with stages
        public void MakePairRouteStage()
        {
            foreach (var route in _routes)
            {
                var listRouteStage = new List<RouteStage>();
                foreach (var routeStage in _routeStages)
                {
                    if (routeStage.RouteID == route.RouteID)
                    {
                        listRouteStage.Add(routeStage);
                    }
                    listRouteStage.Sort(delegate(RouteStage stage1, RouteStage stage2)
                        {
                            if (stage1.StageIndex > stage2.StageIndex) return 1;
                            return -1;
                        });
                }
                var listStage = new List<Stage>();
                foreach (var routeStage in listRouteStage)
                {
                    listStage.AddRange(_stages.Where(stage => routeStage.StageID == stage.StageID));
                }
                _stageOfRoute.Add(route, listStage);
            }
        }
        //sort by the day of request increasing, if two request posted in the same day,
        //then, sort by the weight of request increasing
        public int RequestCompare(Request a, Request b)
        {
            Invoice tmp1 = FindInvoiceFromRequest(a);
            Invoice tmp2 = FindInvoiceFromRequest(b);
            //compare by date post request increasingly
            if (a.DateRequest > b.DateRequest) return 1;
            if (a.DateRequest == b.DateRequest)
            {
                //compare by price of request increasingly
                if (tmp1.Price > tmp2.Price) return 1;
                if (tmp1.Price == tmp2.Price)
                {
                    //compare by volume of request increasingly
                    if (tmp1.Volume > tmp2.Volume) return 1;
                    if (tmp1.Volume == tmp2.Volume) return 0;
                    return -1;
                }
                return -1;
            }
            return -1;
        }
        //find invoice from the input request
        public Invoice FindInvoiceFromRequest(Request request)
        {
            return _invoices.FirstOrDefault(invoice => request.RequestID == invoice.RequestID);
        }
        //MAIN FUNCTION OF ASSIGNING PROCESS
        public Dictionary<Request, List<Trip>> Assigning(List<Request> requests)
        {
            InitData();
            if (requests == null) return _finalResult;
            //if some request doesn't have invoice, meaning there a big error because only requests have invoice are allowed
            //to assigning, the action in this situation is terminal and notice to staff
            if (requests.Any(request => FindInvoiceFromRequest(request) == null))
            {
                return _finalResult;
            }
            //sort list request base on criteria
            requests.Sort(RequestCompare);
            var remainRequest = new List<Request>();
            foreach (var request in requests)
            {
                //mark the current size to compare
                int tmpSize = _finalResult.Count();
                int tmp = CheckOneTrip(request);
                if (tmp > 0)
                {
                    //try processing with one trip
                    ProcessingOneTrip(tmp, request);
                }
                else
                {
                    //if the request can't be assigned to the one trip of one route, try processing multiple trips
                    ProcessingMultipleTrip(request);
                }
                //if request hadn't been assigned, add to a temp list
                if (_finalResult.Count == tmpSize) remainRequest.Add(request);
            }
            //iterate the temp list to try process for the remaining request
            foreach (var request in remainRequest)
            {
                ProcessingMiddleTrip(request);
            }
            return _finalResult;
        }
        //check if there exist a route connect two location of request
        public int CheckOneTrip(Request request)
        {
            Route route = FindRouteFromStations(request.FromLocation, request.ToLocation);
            if (route == null) return -1;
            return route.RouteID;
        }
        //find the end station of route
        public Station FindEndStationFromRoute(Route route)
        {
            List<Stage> listStage = _stageOfRoute.FirstOrDefault(i => i.Key == route).Value;
            var value = listStage[listStage.Count - 1].ToLocation;
            return _stations.FirstOrDefault(station => station.StationID == value);
        }
        //list all trip travel on the route
        public List<Trip> FindTripFromRoute(int routeID)
        {
            return _trips.Where(t => t.RouteID == routeID).ToList();
        }
        //processing with one trip
        public void ProcessingOneTrip(int routeID, Request request)
        {
            DateTime curr = DateTime.Now;
            //find the list of trip can serve this request
            List<Trip> candidates = FindTripFromRoute(routeID);
            foreach (var candidate in candidates)
            {
                DateTime departure = ChangeTime(candidate.Date, candidate.EstimateArrivalTime);
                //if the time departure of trip is too late, don't consider it more
                if (departure - curr > _maxTime) continue;
                Invoice invoice = FindInvoiceFromRequest(request);
                // if volume of trip is big enough, insert this solution to the result
                if (candidate.AvailableVolume >= invoice.Volume)
                {
                    var tmpList = new List<Trip> {candidate};
                    //update the volume
                    candidate.AvailableVolume -= invoice.Volume;
                    _finalResult.Add(request, tmpList);
                    return;
                }
            }
        }
        //processing with multiple trips
        public void ProcessingMultipleTrip(Request request)
        {
            var deliveryTime = new TimeSpan();
            var resTrip = new List<Trip>();
            var volume = FindInvoiceFromRequest(request).Volume;
            if (volume != null)
            {
                var tmp = volume;
                //find the path for delivery
                FindPath(request);
                if (_tmpResult == null) return;
                //iterate through each of request to find the found list route
                foreach (var res in _tmpResult)
                {
                    var current = DateTime.Now;
                    var tmpListStage = res.FirstOrDefault(x => x.Key == request).Value;
                    if (tmpListStage == null)
                    {
                        continue;
                    }
                    //iterate through each route to find the list of corresponding trips
                    int flag = 0;
                    List<Route> tmpListRoute = FindListRouteFromListStage(tmpListStage);
                    foreach (var route in tmpListRoute)
                    {
                        List<Trip> tmpListTrip = FindTripFromRoute(route.RouteID);
                        if (tmpListTrip == null)
                        {
                            continue;
                        }
                        foreach (var trip in tmpListTrip)
                        {
                            if (trip.AvailableVolume < tmp) flag = -1;
                            var departure = ChangeTime(trip.Date, trip.EstimateDepartureTime);
                            if (departure >= current)
                            {
                                var arrival = ChangeTime(trip.Date, trip.EstimateArrivalTime);
                                deliveryTime += (arrival - current);
                                resTrip.Add(trip);
                                var breakTime = FindEndStationFromRoute(route).BreakTime;
                                if (breakTime != null)
                                    current = arrival.AddHours((double) breakTime);
                            }
                            else flag = -1;

                        }
                        //if there is not trip in one of route, reject whole list route of current ways
                        if (deliveryTime > _maxTime || flag == -1)
                        {
                            resTrip.Clear();
                            break;
                        }
                    }
                    if (resTrip.Count > 0)
                    {
                        _finalResult.Add(request, resTrip);
                        return;
                    }
                }
            }
        }
        //processing middle trips
        public void ProcessingMiddleTrip(Request request)
        {
            foreach (var listStage in _tmpResult)
            {
                List<Stage> stages = listStage.FirstOrDefault(i => i.Key == request).Value;
                int count = stages.Count;
                if (count <= 2) goto Check3;
                //divide into 2 routes
                for (int i = 1; i < count - 1; i++)
                {
                    Route a = CheckSubWay(CutList(stages,0,i));
                    Route b = CheckSubWay(CutList(stages, i+1, count-1));
                    if (a != null && b != null)
                    {
                        var stations = new List<int>();
                        stations.Add(request.FromLocation);
                        stations.Add(stages[i+1].FromLocation);
                        var res = new List<Route> {a, b};
                        CheckResult(stations, res, request);
                    }
                }
                //divide into 3 routes
            Check3:
                if (count == 3) goto Check4;
                for (int i = 1; i < count - 1; i++)
                {
                    for (int j = i + 1; j < count - 2; j++)
                    {
                        Route a = CheckSubWay(CutList(stages, 0, i));
                        Route b = CheckSubWay(CutList(stages, i + 1, j));
                        Route c = CheckSubWay(CutList(stages, j + 1, count - 1));
                        if (a != null && b != null && c != null)
                        {
                            var stations = new List<int>();
                            stations.Add(request.FromLocation);
                            stations.Add(stages[i + 1].FromLocation);
                            stations.Add(stages[j + 1].FromLocation);
                            var res = new List<Route> {a, b, c};
                            CheckResult(stations, res, request);
                        }
                    }
                }
                //divide into 4 routes
            Check4:
                if (count == 4) continue;
                for (int i = 1; i < count - 1; i++)
                {
                    for (int j = i + 1; j < count - 2; j++)
                    {
                        for (int k = j + 1; k < count - 3; k++)
                        {
                            Route a = CheckSubWay(CutList(stages, 0, i));
                            Route b = CheckSubWay(CutList(stages, i + 1, j));
                            Route c = CheckSubWay(CutList(stages, j + 1, k));
                            Route d = CheckSubWay(CutList(stages, k + 1, count - 1));

                            if (a != null && b != null && c != null && d != null)
                            {
                                var stations = new List<int>();
                                stations.Add(request.FromLocation);
                                stations.Add(stages[i + 1].FromLocation);
                                stations.Add(stages[j + 1].FromLocation);
                                stations.Add(stages[k + 1].FromLocation);
                                var res = new List<Route> {a, b, c, d};
                                CheckResult(stations, res, request);
                            }
                        }
                    }
                }
            }
        }
        public float FindVolumeOfTripWithStation(Trip trip, int StationID)
        {
            float volume = 0;
            var requests = FindListRequestAssignToTrip(trip);
            foreach (var request in requests)
            {
                var listStation = FindStationFromAssignedRequest(request);
                foreach (var i in listStation)
                {
                    if (i == StationID)
                    {
                        var invoice = FindInvoiceFromRequest(request);
                        if(invoice.Volume != null) volume += (float)invoice.Volume;
                    }
                }
            }
            return ((float)trip.AvailableVolume - volume);
        }
        public List<int> FindStationFromAssignedRequest(Request request)
        {
            var stations = new List<int>();
            foreach (var result in _finalResult)
            {
                var stages = new List<Stage>();
                if (result.Key == request)
                {
                    stations.Add(stages[0].FromLocation);
                    stations.AddRange(stages.Select(stage => stage.ToLocation));
                }
            }
            return stations;
        }
        public List<Request> FindListRequestAssignToTrip(Trip trip)
        {
            var requests = new List<Request>();
            foreach (var result in _finalResult)
            {
                var listTrip = result.Value;
                if (listTrip.Any(t => t == trip))
                {
                    requests.Add(result.Key);
                }
            }
            return requests;
        }
        public void CheckResult(List<int> stations, List<Route> routes, Request request)
        {
            var deliveryTime = new TimeSpan();
            var resTrip = new List<Trip>();
            var volume = FindInvoiceFromRequest(request).Volume;
            if (volume != null)
            {
                var tmp = volume;
                //find the path for delivery
                FindPath(request);
                if (_tmpResult == null) return;
                //iterate through each of request to find the found list route
                foreach (var res in _tmpResult)
                {
                    var current = DateTime.Now;
                    var tmpListStage = res.FirstOrDefault(x => x.Key == request).Value;
                    if (tmpListStage == null)
                    {
                        continue;
                    }
                    //iterate through each route to find the list of corresponding trips
                    int flag = 0;
                    List<Route> tmpListRoute = FindListRouteFromListStage(tmpListStage);
                    int index = -1;
                    foreach (var route in tmpListRoute)
                    {
                        index++;
                        List<Trip> tmpListTrip = FindTripFromRoute(route.RouteID);
                        if (tmpListTrip == null)
                        {
                            continue;
                        }
                        foreach (var trip in tmpListTrip)
                        {
                            if (FindVolumeOfTripWithStation(trip, stations[index]) < tmp) flag = -1;
                            var departure = ChangeTime(trip.Date, trip.EstimateDepartureTime);
                            if (departure >= current)
                            {
                                var arrival = ChangeTime(trip.Date, trip.EstimateArrivalTime);
                                deliveryTime += (arrival - current);
                                resTrip.Add(trip);
                                var breakTime = FindEndStationFromRoute(route).BreakTime;
                                if (breakTime != null)
                                    current = arrival.AddHours((double)breakTime);
                            }
                            else flag = -1;

                        }
                        //if there is not trip in one of route, reject whole list route of current ways
                        if (deliveryTime > _maxTime || flag == -1)
                        {
                            resTrip.Clear();
                            break;
                        }
                    }
                    if (resTrip.Count > 0)
                    {
                        _finalResult.Add(request, resTrip);
                        return;
                    }
                }
            }
        }
        public List<Stage> CutList(List<Stage> stages, int begin, int end)
        {
            var tmp = new List<Stage>();
            for (int i = begin; i <= end; i++)
            {
                tmp.Add(stages[i]);
            }
            return tmp;
        }
        public Route CheckSubWay(List<Stage> stages)
        {
            foreach (var route in _routes)
            {
                List<Stage> listStage = _stageOfRoute.FirstOrDefault(i => i.Key == route).Value;
                int index = 0;
                for (int i = 0; i < listStage.Count; i++)
                {
                    if (listStage[i] == stages[0]) index = i;
                }
                int flag = stages.Where((t, i) => t == listStage[index - 1 + i]).Count();
                if (flag == stages.Count) return route;
            }
            return null;
        }
        public Route FindRouteFromStations(int start, int end)
        {
            foreach (var route in _routes)
            {
                foreach (var stageOfRoute in _stageOfRoute)
                {
                    if (stageOfRoute.Key == route)
                    {
                        //list all stage of the route and find its start station and its end station
                        List<Stage> tmpStage = stageOfRoute.Value;
                        Stage a = tmpStage[0];
                        Stage b = tmpStage[tmpStage.Count - 1];
                        if (a.FromLocation == start && b.ToLocation == end) return route;
                    }
                }
            }
            return null;
        }
        public List<Route> FindListRouteFromListStage(List<Stage> stages)
        {
            int count = stages.Count;
            if (count <= 2) return null;
            //divide into 2 routes
            for (int i = 1; i < count - 1; i++)
            {
                Route a = FindRouteFromStations(stages[0].FromLocation, stages[i].ToLocation);
                Route b = FindRouteFromStations(stages[i + 1].FromLocation, stages[count - 1].ToLocation);
                if (a != null && b != null)
                {
                    var res = new List<Route> {a, b};
                    return res;
                }
            }
            //divide into 3 routes
            if (count == 3) return null;
            for (int i = 1; i < count - 1; i++)
            {
                for (int j = i + 1; j < count - 2; j++)
                {
                    Route a = FindRouteFromStations(stages[0].FromLocation, stages[i].ToLocation);
                    Route b = FindRouteFromStations(stages[i + 1].FromLocation, stages[j].ToLocation);
                    Route c = FindRouteFromStations(stages[j + 1].FromLocation, stages[count - 1].ToLocation);
                    if (a != null && b != null && c != null)
                    {
                        var res = new List<Route> {a, b, c};
                        return res;
                    }
                }
            }
            //divide into 4 routes
            if (count == 4) return null;
            for (int i = 1; i < count - 1; i++)
            {
                for (int j = i + 1; j < count - 2; j++)
                {
                    for (int k = j + 1; k < count - 3; k++)
                    {
                        Route a = FindRouteFromStations(stages[0].FromLocation, stages[i].ToLocation);
                        Route b = FindRouteFromStations(stages[i + 1].FromLocation, stages[j].ToLocation);
                        Route c = FindRouteFromStations(stages[j + 1].FromLocation, stages[k].ToLocation);
                        Route d = FindRouteFromStations(stages[k + 1].FromLocation, stages[count - 1].ToLocation);
                        if (a != null && b != null && c != null)
                        {
                            var res = new List<Route> {a, b, c, d};
                            return res;
                        }
                    }
                }
            }
            return null;
        }
        //check whether the way of request is belong to the way of route
        public bool CheckWay(Request request, Route route)
        {
            foreach (var tmp in _tmpResult)
            {
                var tmpListStage1 = tmp.FirstOrDefault(x => x.Key == request).Value;
                var tmpListStage2 = _stageOfRoute.FirstOrDefault(x => x.Key == route).Value;
                int index = 0;
                if(tmpListStage2.Except(tmpListStage1).Any()) return false;
                for (int i = 0; i < tmpListStage1.Count; i++)
                {
                    if (tmpListStage1[i] == tmpListStage2[0])
                    {
                        index = i;
                        break;
                    }
                }
                if (tmpListStage2.Where((t, i) => t != tmpListStage1[index + i - 1]).Any())
                {
                    return false;
                }
            }
            return true;
        }
        //add a time span to datetime 
        public DateTime ChangeTime(DateTime a, TimeSpan b)
        {
            DateTime result = a;
            result = result.AddHours(b.Days);
            result = result.AddHours(b.Hours);
            result = result.AddMinutes(b.Minutes);
            return result;
        }
        //find all path between two station of the request
        public void FindPath(Request request)
        {
            var fromStation = new Station();
            var toStation = new Station();
            int count = 0;
            for (int i = 0; i < _stations.Count && count < 2; i++)
            {
                if (_stations[i].StationID == request.FromLocation)
                {
                    fromStation = _stations[i];
                    count++;
                }
                if (_stations[i].StationID == request.ToLocation)
                {
                    toStation = _stations[i];
                    count++;
                }
            }
            //recursion to find all path using BFS + check not go backward
            var visited = new List<Station> {fromStation};
            //after call this method, all path connect two station of the request
            BreadthFirstSearch(request, toStation, visited);
        }
        //BreadthFirstSearch to find all path
        public void BreadthFirstSearch(Request request, Station toStation, List<Station> visited)
        {
            Station currNode = visited.Last();
            List<Station> nodes = adj.FirstOrDefault(x => x.Key == currNode).Value;
            foreach (var station in nodes)
            {
                if (visited.Contains(station)) continue;
                if (station.Equals(toStation))
                {
                    visited.Add(station);
                    if (CheckForward(visited))
                    {
                        var tmpRoutes = new List<Stage>();
                        for (int i = 0; i < visited.Count - 1; i++)
                        {
                            tmpRoutes.Add(FindStageFromStation(visited[i], visited[i + 1]));
                        }
                        var tmp = new Dictionary<Request, List<Stage>> {{request, tmpRoutes}};
                        _tmpResult.Add(tmp);
                        //allow _maxWay = 5 ways in searching -> the complexity of algorithm is approximate O(_maxWay|E||V|)
                        if (_tmpResult.Count > _maxWay) return;
                    }
                    visited.Remove(visited.Last());
                    break;
                }
            }
            foreach (var station in nodes)
            {
                if (visited.Contains(station) || station.Equals(toStation)) continue;
                visited.Add(station);
                BreadthFirstSearch(request, toStation, visited);
                visited.Remove(visited.Last());
            }
        }
        //find the stage connects two stations
        public Stage FindStageFromStation(Station a, Station b)
        {
            return _stages.FirstOrDefault(stage => stage.FromLocation == a.StationID && stage.ToLocation == b.StationID);
        }
        //check the diection always forward
        public bool CheckForward(List<Station> stationsForCheck)
        {
            //iterate through list of station, if there are 3 stations not lie forward, return false
            for (int i = 0; i < stationsForCheck.Count() - 2; i++)
            {
                string coordinateA = stationsForCheck[i].Coordinate;
                string coordinateB = stationsForCheck[i + 1].Coordinate;
                string coordinateC = stationsForCheck[i + 2].Coordinate;
                string tmp = coordinateA + "," + coordinateB + "," + coordinateC;
                string[] separator = { ",", " " };
                string[] words = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var longitude = new double[10];
                var latitude = new double[10];
                int c = 0, numLongitude = 0, numLatitude = 0;
                //convert longitude and latitude from coordinates
                foreach (var word in words)
                {
                    if (c % 2 == 1)
                    {
                        longitude[++numLongitude] = double.Parse(word);
                    }
                    else
                    {
                        latitude[++numLatitude] = double.Parse(word);
                    }
                    c++;
                }
                var x = new double[10];
                var y = new double[10];
                var z = new double[10];
                //convert longitude and lititude into 3D coordinate
                //base on the link: http://math.stackexchange.com/questions/441182/how-to-check-if-three-coordinates-form-a-line

                for (int j = 1; j <= 3; j++)
                {
                    x[j] = Math.Cos(Math.PI*longitude[j]/180)*Math.Cos(Math.PI*latitude[j]/180);
                    y[j] = Math.Sin(Math.PI*longitude[j]/180)*Math.Cos(Math.PI*latitude[j]/180);
                    z[j] = Math.Sin(Math.PI*latitude[j]/180);
                }
                double xAB = x[2] - x[1];
                double yAB = y[2] - y[1];
                double zAB = z[2] - z[1];
                double xBC = x[3] - z[2];
                double yBC = y[3] - y[2];
                double zBC = z[3] - z[2];
                //if the dot product of two vectors connect AB and BC is negative, reject immediately
                if (xAB*xBC + yAB*yBC + zAB*zBC < 0) return false;
            }
            //if all well, the line is accepted
            return true;
        }
        //when delete a trip or list of trips, call this method to change the status of
        //processing request to "Đã xác nhận" then re-schedule
        [System.Web.Mvc.HttpPost]
        [WebMethod]
        public void Update(List<int> requestIDs, int tripID)
        {
            var db = new iDeliverEntities();
            var tmpRequestID = (from assigning in _assignings where assigning.TripID == tripID select assigning.RequestID).ToList();
            //find the list of request id that related to delete trips

            //update the status of all request in the list
            
            foreach (var id in requestIDs)
            {
                Request request = db.Requests.FirstOrDefault(r => r.RequestID == id);
                foreach (var i in tmpRequestID)
                {
                    if (request != null && request.RequestID == i)
                    {
                        request.DeliveryStatusID = 1;
                        _requestRepository.Update(request);
                    }
                }
            }
        }
    }
}