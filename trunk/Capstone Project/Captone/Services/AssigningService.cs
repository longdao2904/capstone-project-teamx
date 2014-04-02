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
        private const int MaxWay = 5;
        private readonly TimeSpan _deltaTime = new TimeSpan(0, 45, 0);
        private const double MaxAngle = 135*Math.PI/180;
        //result of route
        List<Dictionary<Request, List<Stage>>> _foundWays = new List<Dictionary<Request, List<Stage>>>();
        //result of trip
        Dictionary<Request, List<Trip>> _finalResult = new Dictionary<Request, List<Trip>>();
        //adjancent list of station
        Dictionary<Station, List<Station>> adj = new Dictionary<Station, List<Station>>();
        //public AssigningService()
        //{
        //    //Constructor without argument
        //}
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
                    tmp.AddRange(from t in _stages
                                 where t.StartPoint == station1.StationID &&
                                     t.EndPoint == station2.StationID
                                 select station2);
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
                //add list of stage for each route, sort by the increasing index
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
            var tmp1 = FindInvoiceFromRequest(a);
            var tmp2 = FindInvoiceFromRequest(b);
            //compare with criteria: Date Request -> Price -> Volume
            if (a.DateRequest == b.DateRequest)
            {
                return tmp1.Price == tmp2.Price ? (tmp1.Volume).CompareTo(tmp2.Volume) : (tmp1.Price).CompareTo(tmp2.Price);
            }
            return a.DateRequest.CompareTo(b.DateRequest);
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
            //if the list request is empty, terminate function
            if (requests == null) return _finalResult;
            //if some request doesn't have invoice, meaning there a big error because only requests have invoice are allowed
            //to assigning, the action in this situation is terminal and notice to staff
            if (requests.Any(request => FindInvoiceFromRequest(request) == null))
            {
                return _finalResult;
            }
            //sort list request base on criteria
            requests.Sort(RequestCompare);
            //try one trip -> multiple trip -> middle trip with this priority
            var remainRequest = new List<Request>();
            foreach (var request in requests)
            {
                //mark the current size to compare
                int tmpSize = _finalResult.Count();
                //check if there is a route connect two stations of request, if yes, get the routeID
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
            int res = 0;
            foreach (var stages in from stage in _stageOfRoute where stage.Key == route select stage.Value)
            {
                res = stages[stages.Count - 1].EndPoint;
            }
            return _stations.FirstOrDefault(station => station.StationID == res);
        }
        //list all trips travel on the route
        public List<Trip> FindTripFromRoute(int routeID)
        {
            return _trips.Where(t => t.RouteID == routeID).ToList();
        }
        //sort base on departure time and then, arrival time
        public int SortTripCompare(Trip a, Trip b)
        {
            return a.EstimateDepartureTime == b.EstimateDepartureTime ? 
                (a.EstimateArrivalTime).CompareTo(b.EstimateArrivalTime) : 
                (a.EstimateDepartureTime).CompareTo(b.EstimateDepartureTime);
        }
        //processing with one trip
        public void ProcessingOneTrip(int routeID, Request request)
        {
            DateTime curr = DateTime.Now;
            //find the list of trip can serve this request
            var candidates = FindTripFromRoute(routeID);
            //sort list trip base on the departure time and arrival time
            candidates.Sort(SortTripCompare);
            foreach (var candidate in candidates)
            {
                var arrival = ChangeTime(candidate.Date, candidate.EstimateArrivalTime);
                //if the time departure of trip is too late, don't consider it more
                if (arrival - curr > _maxTime) continue;
                var invoice = FindInvoiceFromRequest(request);
                // if volume of trip is big enough, insert this solution to the result
                if (candidate.AvailableVolume < invoice.Volume) continue;
                var tmpList = new List<Trip> { candidate };
                //update the volume
                candidate.AvailableVolume -= invoice.Volume;
                _finalResult.Add(request, tmpList);
                return;
            }
        }
        //processing with multiple trips
        public void ProcessingMultipleTrip(Request request)
        {
            var resTrip = new List<Trip>();
            var volume = FindInvoiceFromRequest(request).Volume;
            var tmp = volume;
            //find the path for delivery
            FindPath(request);
            //sort the list of ways base on the distance and duration
            _foundWays.Sort(CompareWays);
            if (_foundWays == null) return;
            var tmpListOfResult = new List<List<Trip>>();
            //iterate through each of found ways to find the found list route
            foreach (var res in _foundWays)
            {
                var current = DateTime.Now;
                //take the list of stage from request
                var tmpListStage = res.FirstOrDefault(x => x.Key == request).Value;
                if (tmpListStage == null)
                {
                    continue;
                }
                //iterate through each route to find the list of corresponding trips
                int flag = 0;
                var tmpListRoute = FindListRouteFromListStage(tmpListStage, true);
                if (tmpListRoute == null) break;
                foreach (var tmpListTrip in tmpListRoute.Select(route => FindTripFromRoute(route.RouteID)))
                {
                    //if some route of this way doesn't have trip, reject this way
                    if (tmpListTrip == null)
                    {
                        flag = -1;
                        continue;
                    }
                    foreach (var trip in tmpListTrip)
                    {
                        if (trip.AvailableVolume < tmp) flag = -1;
                        var departure = ChangeTime(trip.Date, trip.EstimateDepartureTime);
                        if (departure - current >= _deltaTime)
                        {
                            current = ChangeTime(trip.Date, trip.EstimateArrivalTime);
                            resTrip.Add(trip);
                        }
                        else flag = -1;
                    }
                    //if there is not a chosen trip in one of route or the delivery time is too late
                    //reject whole list route of current ways
                    if (flag == -1)
                    {
                        resTrip.Clear();
                        break;
                    }
                    //else, add the current solution to the list
                    tmpListOfResult.Add(resTrip);
                }
            }
            //sort to find out the best solution
            tmpListOfResult.Sort(CompareQualityOfSolution);
            if (tmpListOfResult.Count > 0)
            {
                _finalResult.Add(request, tmpListOfResult.First());
            }
        }

        //choose the solution with criteria: the arrival time -> number of route
        public int CompareQualityOfSolution(List<Trip> a, List<Trip> b)
        {
            return a.Last().EstimateArrivalTime == b.Last().EstimateArrivalTime ? (a.Count).CompareTo(b.Count) : 
                (a.Last().EstimateArrivalTime).CompareTo(b.Last().EstimateArrivalTime);
        }

        //compare the distance of two list of stage
        public int CompareWays(Dictionary<Request, List<Stage>> a, Dictionary<Request, List<Stage>> b)
        {
            var request1 = new Request();
            var request2 = new Request();
            foreach (var list in a)
            {
                request1 = list.Key;
                break;
            }
            foreach (var list in b)
            {
                request2 = list.Key;
                break;
            }
            var tmp1 = a.FirstOrDefault(i => i.Key == request1).Value;
            var tmp2 = b.FirstOrDefault(i => i.Key == request2).Value;
            return FindDistanceFromListStage(tmp1) == FindDistanceFromListStage(tmp2) ? 
                FindDurationFromListStage(tmp1).CompareTo(FindDurationFromListStage(tmp2)) : 
                FindDistanceFromListStage(tmp1).CompareTo(FindDistanceFromListStage(tmp2));
        }
        public double FindDistanceFromListStage(List<Stage> a)
        {
            return a.Sum(stage => stage.Distance);
        }

        public double FindDurationFromListStage(List<Stage> a)
        {
            return a.Sum(stage => stage.Duration);
        }

        //processing middle trips
        public void ProcessingMiddleTrip(Request request)
        {
            var tmpResult = new List<List<Trip>>();
            foreach (var listStage in _foundWays)
            {
                var stages = listStage.FirstOrDefault(i => i.Key == request).Value;
                if(stages == null) continue;
                int count = stages.Count;
                //find one route
                Route one = CheckSubWay(CutList(stages, 0, stages.Count - 1));
                if (one != null)
                {
                    var stations = new List<int> {request.FromLocation};
                    var res = new List<Route> { one };
                    var solution = CheckResult(stations, res, request);
                    if(solution != null) tmpResult.Add(solution);
                }
                //find two routes
                for (int i = 1; i < count - 1; i++)
                {
                    Route a = CheckSubWay(CutList(stages, 0, i - 1));
                    Route b = CheckSubWay(CutList(stages, i, count - 1));
                    if (a != null && b != null)
                    {
                        var stations = new List<int> {request.FromLocation, stages[i].StartPoint};
                        var res = new List<Route> {a, b};
                        var solution = CheckResult(stations, res, request);
                        if (solution != null) tmpResult.Add(solution);
                    }
                }
                //find three routes
                for (int i = 1; i < count - 1; i++)
                {
                    for (int j = i + 1; j < count - 2; j++)
                    {
                        Route a = CheckSubWay(CutList(stages, 0, i));
                        Route b = CheckSubWay(CutList(stages, i + 1, j));
                        Route c = CheckSubWay(CutList(stages, j + 1, count - 1));
                        if (a != null && b != null && c != null)
                        {
                            var stations = new List<int>
                                {
                                    request.FromLocation,
                                    stages[i + 1].StartPoint,
                                    stages[j + 1].StartPoint
                                };
                            var res = new List<Route> { a, b, c };
                            var solution = CheckResult(stations, res, request);
                            if (solution != null) tmpResult.Add(solution);
                        }
                    }
                }
                //divide into 4 routes
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
                                var stations = new List<int>
                                    {
                                        request.FromLocation,
                                        stages[i + 1].StartPoint,
                                        stages[j + 1].StartPoint,
                                        stages[k + 1].StartPoint
                                    };
                                var res = new List<Route> { a, b, c, d };
                                var solution = CheckResult(stations, res, request);
                                if (solution != null) tmpResult.Add(solution);
                            }
                        }
                    }
                }
            }
            if (tmpResult.Count > 0)
            {
                tmpResult.Sort(CompareQualityOfSolution);
                _finalResult.Add(request, tmpResult.First());
            }
        }
        public float FindVolumeOfTripWithStation(Trip trip, int stationID)
        {
            float volume = 0;
            var requests = FindListRequestAssignToTrip(trip);
            foreach (var request in requests)
            {
                var listStation = FindStationFromAssignedRequest(request);
                foreach (var i in listStation)
                {
                    if (i == stationID)
                    {
                        var invoice = FindInvoiceFromRequest(request);
                        if (invoice.Volume != null) volume += (float)invoice.Volume;
                    }
                }
            }
            return ((float)trip.EstimateVolume - volume);
        }
        public List<int> FindStationFromAssignedRequest(Request request)
        {
            var stations = new HashSet<int>();
            var trips = _finalResult.FirstOrDefault(i => i.Key == request).Value;
            foreach (var trip in trips)
            {
                var stages = FindListStageOfTrip(trip);
                foreach (var stage in stages)
                {
                    stations.Add(stage.StartPoint);
                    stations.Add(stage.EndPoint);
                }
            }
            return stations.ToList();
        }
        public List<Stage> FindListStageOfTrip(Trip trip)
        {
            return _routes.Where(route => trip.RouteID == route.RouteID).
                Select(route => (_stageOfRoute.FirstOrDefault(i => i.Key == route).Value)).FirstOrDefault();
        }
        public List<Request> FindListRequestAssignToTrip(Trip trip)
        {
            return (from result in _finalResult
                    let listTrip = result.Value
                    where listTrip.Any(t => t == trip)
                    select result.Key).ToList();
        }

        public List<Trip> CheckResult(List<int> stations, List<Route> routes, Request request)
        {
            var resTrip = new List<Trip>();
            var volume = FindInvoiceFromRequest(request).Volume;
            var tmp = volume;
            var current = DateTime.Now;
            //iterate through each route to find the list of corresponding trips
            int flag = 0;
            int index = -1;
            foreach (var route in routes)
            {
                index++;
                var tmpListTrip = FindTripFromRoute(route.RouteID);
                if (tmpListTrip == null)
                {
                    return null;
                }
                foreach (var trip in tmpListTrip)
                {
                    if (FindVolumeOfTripWithStation(trip, stations[index]) < tmp) flag = -1;
                    var departure = ChangeTime(trip.Date, trip.EstimateDepartureTime);
                    if (departure - current >= _deltaTime)
                    {
                        current = ChangeTime(trip.Date, trip.EstimateArrivalTime);
                        resTrip.Add(trip);
                    }
                    else flag = -1;

                }
                //if there is not trip in one of route, reject whole list route of current ways
                if (flag == -1)
                {
                    resTrip.Clear();
                    break;
                }
            }
            return resTrip.Count > 0 ? resTrip : null;
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
                var listStage = _stageOfRoute.FirstOrDefault(i => i.Key == route).Value.ToList();
                int index = 0;
                for (int i = 0; i < listStage.Count; i++)
                {
                    if (listStage[i] == stages[0])
                    {
                        index = i;
                        break;
                    }
                }
                if (stages.Count + index > listStage.Count) continue;
                int flag = 0;
                for (int i = 0; i < stages.Count; i++)
                {
                    if (stages[i] == listStage[index + i]) flag++;
                }
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
                        var tmpStage = stageOfRoute.Value.ToList();
                        if (tmpStage.Count == 0) return null;
                        Stage a = tmpStage.First();
                        Stage b = tmpStage.Last();
                        if (a.StartPoint == start && b.EndPoint == end) return route;
                    }
                }
            }
            return null;
        }
        public List<Route> FindListRouteFromListStage(List<Stage> stages, bool type)
        {
            if (type)
            {
                return stages.Select(stage => FindRouteFromStations(stage.StartPoint, stage.EndPoint)).Where(a => a != null).ToList();
            }
            int count = stages.Count;
            if (count <= 1) return null;
            //divide into 2 routes
            for (int i = 1; i <= count - 1; i++)
            {
                Route a = FindRouteFromStations(stages[0].StartPoint, stages[i - 1].EndPoint);
                Route b = FindRouteFromStations(stages[i].StartPoint, stages[count - 1].EndPoint);
                if (a != null && b != null)
                {
                    var res = new List<Route> { a, b };
                    return res;
                }
            }
            //divide into 3 routes
            if (count == 2) return null;
            for (int i = 1; i <= count - 2; i++)
            {
                for (int j = i + 1; j <= count - 1; j++)
                {
                    Route a = FindRouteFromStations(stages[0].StartPoint, stages[i - 1].EndPoint);
                    Route b = FindRouteFromStations(stages[i].StartPoint, stages[j - 1].EndPoint);
                    Route c = FindRouteFromStations(stages[j].StartPoint, stages[count - 1].EndPoint);
                    if (a != null && b != null && c != null)
                    {
                        var res = new List<Route> { a, b, c };
                        return res;
                    }
                }
            }
            //divide into 4 routes
            if (count == 3) return null;
            for (int i = 1; i <= count - 3; i++)
            {
                for (int j = i + 1; j <= count - 2; j++)
                {
                    for (int k = j + 1; k <= count - 1; k++)
                    {
                        Route a = FindRouteFromStations(stages[0].StartPoint, stages[i - 1].EndPoint);
                        Route b = FindRouteFromStations(stages[i].StartPoint, stages[j - 1].EndPoint);
                        Route c = FindRouteFromStations(stages[j].StartPoint, stages[k - 1].EndPoint);
                        Route d = FindRouteFromStations(stages[k].StartPoint, stages[count - 1].EndPoint);
                        if (a != null && b != null && c != null)
                        {
                            var res = new List<Route> { a, b, c, d };
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
            foreach (var tmp in _foundWays)
            {
                var tmpListStage1 = tmp.FirstOrDefault(x => x.Key == request).Value;
                var tmpListStage2 = _stageOfRoute.FirstOrDefault(x => x.Key == route).Value;
                int index = 0;
                if (tmpListStage2.Except(tmpListStage1).Any()) return false;
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
            var visited = new List<Station> { fromStation };
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
                        var tmp = new Dictionary<Request, List<Stage>> { { request, tmpRoutes } };
                        _foundWays.Add(tmp);
                        //allow _maxWay = 5 ways in searching -> the complexity of algorithm is approximate O(_maxWay|E||V|)
                        if (_foundWays.Count > MaxWay) return;
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
            return _stages.FirstOrDefault(stage => stage.StartPoint == a.StationID &&
                stage.EndPoint == b.StationID);
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
                var words = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                var longitude = FindListLongitude(words);
                var latitude = FindListLatitude(words);
                var x = new double[10];
                var y = new double[10];
                var z = new double[10];
                //convert longitude and lititude into 3D coordinate
                //base on the link: http://math.stackexchange.com/questions/441182/how-to-check-if-three-coordinates-form-a-line
                for (int j = 1; j <= 3; j++)
                {
                    x[j] = Math.Cos(Math.PI * longitude[j] / 180) * Math.Cos(Math.PI * latitude[j] / 180);
                    y[j] = Math.Sin(Math.PI * longitude[j] / 180) * Math.Cos(Math.PI * latitude[j] / 180);
                    z[j] = Math.Sin(Math.PI * latitude[j] / 180);
                }
                double xAB = x[2] - x[1];
                double yAB = y[2] - y[1];
                double zAB = z[2] - z[1];
                double xBC = x[3] - z[2];
                double yBC = y[3] - y[2];
                double zBC = z[3] - z[2];
                double AB = Math.Sqrt(xAB * xAB + yAB * yAB + zBC * zBC);
                double BC = Math.Sqrt(xBC * xBC + yBC * yBC + zBC * zBC);
                //if the dot product of two vectors connect AB and BC is negative, reject immediately
                if ((xAB * xBC + yAB * yBC + zAB * zBC) / AB * BC < Math.Cos(MaxAngle)) return false;
            }
            //if all well, the line is accepted
            return true;
        }
        //find list longitude
        public double[] FindListLongitude(List<String> words)
        {
            int c = 0, numLongitude = 0;
            var longitude = new double[10];
            foreach (var word in words)
            {
                if (c % 2 == 1)
                {
                    longitude[++numLongitude] = double.Parse(word);
                }
                c++;
            }
            return longitude;
        }
        //find list latitude
        public double[] FindListLatitude(List<String> words)
        {
            int c = 0, numLatitude = 0;
            var latitude = new double[10];
            foreach (var word in words)
            {
                if (c % 2 == 0)
                {
                    latitude[++numLatitude] = double.Parse(word);
                }
                c++;
            }
            return latitude;
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