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
        private readonly GenericRepository<Schedule> _scheduleRepository =
            new GenericRepository<Schedule>(new iDeliverEntities());
        //declare class
        private List<Invoice> _invoices = new List<Invoice>();
        private List<Station> _stations = new List<Station>();
        private List<Route> _routes = new List<Route>();
        private List<Trip> _trips = new List<Trip>();
        private List<Assigning> _assignings = new List<Assigning>();
        private List<Stage> _stages = new List<Stage>();
        private List<RouteStage> _routeStages = new List<RouteStage>();
        private List<Schedule> _schedules = new List<Schedule>();
        private Dictionary<Route, List<Stage>> _stageOfRoute = new Dictionary<Route, List<Stage>>();
        //declare const
        private readonly TimeSpan _maxTime = new TimeSpan(5, 0, 0, 0);
        private const int MaxWay = 10;
        private readonly TimeSpan _deltaTime = new TimeSpan(0, 45, 0);
        private const double MaxAngle = 90 * Math.PI / 180;
        //result of route
        List<Dictionary<Request, List<Stage>>> _foundWays = new List<Dictionary<Request, List<Stage>>>();
        //result of trip
        Dictionary<Request, Dictionary<Trip, int>> _finalResult = new Dictionary<Request, Dictionary<Trip, int>>();
        //adjancent list of station
        Dictionary<Station, List<Station>> adj = new Dictionary<Station, List<Station>>();
        Dictionary<Request, Reason> reasons = new Dictionary<Request, Reason>();
        public AssigningService()
        {
            //Constructor without argument
        }
        public AssigningService(GenericRepository<Route> routeRepository
            , GenericRepository<Station> stationRepository
            , GenericRepository<Invoice> invoiceRepository
            , GenericRepository<Trip> tripRepository
            , GenericRepository<Request> requestRepository
            , GenericRepository<Assigning> assigningRepository
            , GenericRepository<Stage> stageRepository
            , GenericRepository<RouteStage> routeStageRepository
            , GenericRepository<Schedule> scheduleRepository)
        {
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;
            _invoiceRepository = invoiceRepository;
            _tripRepository = tripRepository;
            _requestRepository = requestRepository;
            _assigningRepository = assigningRepository;
            _stageRepository = stageRepository;
            _routeStageRepository = routeStageRepository;
            _scheduleRepository = scheduleRepository;
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
            _schedules = _scheduleRepository.GetAll().ToList();
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
            var tmpList = _trips.Where(trip => trip.EstimateDepartureTime - DateTime.Now >=
                _deltaTime && trip.IsActive).ToList();
            _trips = tmpList;
        }
        //find the list of route with stages
        public void MakePairRouteStage()
        {
            foreach (var route in _routes)
            {
                var listRouteStage = _routeStages.Where(routeStage => routeStage.RouteID == route.RouteID).ToList();
                listRouteStage.Sort(delegate(RouteStage stage1, RouteStage stage2)
                {
                    if (stage1.StageIndex > stage2.StageIndex) return 1;
                    return -1;
                });
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
        //check if a request has been scheduled
        public bool CheckExist(Request request)
        {
            return _assignings.Any(assigning => assigning.RequestID == request.RequestID);
        }
        //init the paramater of reason
        public Reason Init(Reason reason)
        {
            reason.RequestCode = "";
            reason.FromTo = "";
            reason.NumberOfWay = 0;
            reason.WayList = new List<string>();
            reason.OneTrip = "";
            reason.MultipleTrip = "";
            reason.MiddleTrip = "";
            return reason;
        }
        //create the way list for request
        public List<String> AddWayList(Request request)
        {
            if (!CheckNotNull(_foundWays)) return null;
            var result = new List<String>();
            foreach (var foundWay in _foundWays)
            {
                var tmpList = foundWay.First(i => i.Key == request).Value;
                if (CheckNotNull(tmpList))
                {
                    string tmp = tmpList.Aggregate("", (current, stage) => current + (FindName(stage.StartPoint) + " => "));
                    tmp += FindName(tmpList.Last().EndPoint);
                    result.Add(tmp);
                }
            }
            return result;
        }
        //find name of station base on its id
        public string FindName(int stationID)
        {
            foreach (var station in _stations.Where(station => station.StationID == stationID))
            {
                return station.StationName;
            }
            return "";
        }

        //MAIN FUNCTION OF ASSIGNING PROCESS
        public Dictionary<Request, Dictionary<Trip, int>> Assigning(List<Request> requests, out List<Reason> failedReason)
        {
            InitData();
            failedReason = new List<Reason>();
            //if the list request is empty, terminate function
            if (!CheckNotNull(requests))
            {
                failedReason = null;
                return _finalResult;
            }
            //if some request doesn't have invoice, meaning there a big error because only requests have invoice are allowed
            //to assigning, the action in this situation is terminal and notice to staff
            if (requests.Any(request => FindInvoiceFromRequest(request) == null))
            {
                failedReason = null;
                return _finalResult;
            }
            //sort list request base on criteria
            requests.Sort(RequestCompare);
            //try one trip -> multiple trip -> middle trip with this priority
            var remainRequest = new List<Request>();
            foreach (var request in requests)
            {
                if (CheckExist(request)) continue;
                var tmpReason = new Reason();
                tmpReason = Init(tmpReason);
                tmpReason.RequestCode = request.RequestCode;
                tmpReason.FromTo = FindName(request.FromLocation) + " => " + FindName(request.ToLocation);
                //mark the current size to compare
                int tmpSize = _finalResult.Count();
                int flag = 0;
                //check if there is a route connect two stations of request, if yes, get the routeID
                int tmp = CheckOneTrip(request);
                if (tmp > 0)
                {
                    //try processing with one trip
                    ProcessingOneTrip(tmp, request);
                    if (tmpSize == _finalResult.Count)
                    {
                        tmpReason.OneTrip = @"Có chuyến trực tiếp nhưng không có xe phù hợp";
                    }
                }
                else
                {
                    tmpReason.OneTrip = @"Không có chuyến trực tiếp nối hai trạm";
                    //if the request can't be assigned to the one trip of one route, try processing multiple trips
                    flag = ProcessingMultipleTrip(request);
                    tmpReason.NumberOfWay = _foundWays.Count;
                    tmpReason.WayList = AddWayList(request);
                }
                //if request hadn't been assigned, add to a temp list
                if (_finalResult.Count == tmpSize)
                {
                    if (flag == 1) tmpReason.MultipleTrip = @"Tìm được tuyến đường nhưng không có xe chạy";
                    else if (flag == 2)
                        tmpReason.MultipleTrip = @"Tìm được tuyến đường, chuyến xe chạy nhưng xe không thỏa mãn ràng buộc về thời gian - thể tích";
                    else tmpReason.MultipleTrip = @"Không tìm được các tuyến xe để gửi hàng chỉ tại trạm đầu, trạm cuối chứ không gửi giữa đường";
                    remainRequest.Add(request);
                }
                reasons.Add(request, tmpReason);
            }
            //iterate the temp list to try process for the remaining request
            foreach (var request in remainRequest)
            {
                int flag = ProcessingMiddleTrip(request);
                var tmp = reasons.First(i => i.Key == request);
                var tmpReason = tmp.Value;
                reasons.Remove(tmp.Key);
                if (flag == 1) tmpReason.MiddleTrip = @"Tìm được tuyến đường nhưng không chưa có xe chạy";
                else if (flag == 2) tmpReason.MiddleTrip = @"Tìm được tuyến đường, chuyến xe nhưng xe không thỏa mãn ràng buộc về thời gian - thể tích";
                else tmpReason.MiddleTrip = @"Hoàn toàn không tìm được tuyến đường nào cho yêu cầu vận chuyển này";
                reasons.Add(tmp.Key, tmpReason);
            }
            foreach (var reason in reasons)
            {
                int flag = 0;
                foreach (var result in _finalResult)
                {
                    if (result.Key == reason.Key) flag = 1;
                }
                if (flag == 0) failedReason.Add(reason.Value);
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
            var trips = (from trip in _trips
                         from schedule in _schedules
                         where schedule.ScheduleID == trip.ScheduleID && schedule.RouteID == routeID
                         select trip).ToList();
            if (!CheckNotNull(trips)) return null;
            trips.Sort((a, b) => (a.EstimateDepartureTime).CompareTo(b.EstimateDepartureTime));
            return trips;
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
            var curr = DateTime.Now;
            //find the list of trip can serve this request
            var candidates = FindTripFromRoute(routeID);
            //sort list trip base on the departure time and arrival time
            if (!CheckNotNull(candidates)) return;
            candidates.Sort(SortTripCompare);
            foreach (var candidate in candidates)
            {
                var arrival = candidate.EstimateArrivalTime;
                //if the time departure of trip is too late, don't consider it more
                if (arrival - curr > _maxTime) continue;
                var invoice = FindInvoiceFromRequest(request);
                // if volume of trip is big enough, insert this solution to the result
                if (candidate.AvailableVolume < invoice.Volume) continue;
                var tmpMap = new Dictionary<Trip, int>();
                tmpMap.Add(candidate, request.ToLocation);
                //update the volume
                candidate.AvailableVolume -= invoice.Volume;
                _finalResult.Add(request, tmpMap);
                return;
            }
        }
        //processing with multiple trips
        public int ProcessingMultipleTrip(Request request)
        {
            var volume = FindInvoiceFromRequest(request).Volume;
            var tmp = volume;
            int flag = 0; //for check status of reason
            //find the path for delivery
            _foundWays.Clear();
            FindPath(request);
            if (!CheckNotNull(_foundWays)) return 0;
            CleanWay(request);
            //sort the list of ways base on the distance and duration
            _foundWays.Sort(CompareWays);
            if (!CheckNotNull(_foundWays)) return 0;
            var tmpListOfResult = new List<Dictionary<Trip, int>>();
            //iterate through each of found ways to find the found list route
            foreach (var res in _foundWays)
            {
                var resTrip = new List<Trip>();
                var current = DateTime.Now;
                //take the list of stage from request
                var tmpListStage = res.FirstOrDefault(x => x.Key == request).Value;
                if (!CheckNotNull(tmpListStage))
                {
                    continue;
                }
                //iterate through each route to find the list of corresponding trips
                var tmpListRoute = new List<Route>();
                if (CheckNotNull(FindListRouteFromListStage(tmpListStage)))
                {
                    tmpListRoute = FindListRouteFromListStage(tmpListStage);
                }
                int count = tmpListRoute.Count;
                if (count > 0) flag = 1; //remark for reason string
                foreach (var route in tmpListRoute)
                {
                    if (route == null) continue;
                    var tmpListTrip = FindTripFromRoute(route.RouteID);
                    //if some route of this way doesn't have trip, reject this way
                    if (!CheckNotNull(tmpListTrip)) continue;
                    tmpListTrip.Sort(SortTripCompare);
                    foreach (var trip in tmpListTrip)
                    {
                        flag = 2; //remark for reason string
                        var departure = trip.EstimateDepartureTime;
                        if (trip.AvailableVolume >= tmp && departure - current >= _deltaTime)
                        {
                            current = trip.EstimateArrivalTime;
                            resTrip.Add(trip);
                            break;
                        }
                    }
                    //if there is not a chosen trip in one of route or the delivery time is too late
                    //reject whole list route of current ways
                }
                if (resTrip.Count == count && count > 0)
                {
                    var tmpMap = new Dictionary<Trip, int>();
                    foreach (var trip in resTrip)
                    {
                        var station = FindListStageOfTrip(trip).Last().EndPoint;
                        tmpMap.Add(trip, station);
                    }
                    tmpListOfResult.Add(tmpMap);
                }
            }
            //sort to find out the best solution
            if (CheckNotNull(tmpListOfResult))
            {
                if (tmpListOfResult.Count >= 2) tmpListOfResult.Sort(CompareQualityOfSolution);
                var first = tmpListOfResult.First();
                _finalResult.Add(request, first);
            }
            if (flag == 1) return 1;
            if (flag == 2) return 2;
            return 3;
        }
        //check a list of T not null
        public bool CheckNotNull<T>(List<T> list)
        {
            if (list == null) return false;
            if (list.Count == 0) return false;
            return true;
        }
        public void CleanWay(Request request)
        {
            foreach (var foundWay in _foundWays)
            {
                if (foundWay.ContainsKey(request))
                {
                    var tmpStage = foundWay.Values.First();
                    var checkStage = new HashSet<Stage>();
                    foreach (var stage in tmpStage)
                    {
                        checkStage.Add(stage);
                    }
                    if (checkStage.Count != tmpStage.Count) _foundWays.Remove(foundWay);
                }
            }
        }
        //choose the solution with criteria: the arrival time -> number of route
        public int CompareQualityOfSolution(Dictionary<Trip, int> resA, Dictionary<Trip, int> resB)
        {
            var a = resA.Select(i => i.Key).ToList();
            var b = resB.Select(i => i.Key).ToList();
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
            var check = FindDistanceFromListStage(tmp1) == FindDistanceFromListStage(tmp2) ?
                    FindDurationFromListStage(tmp1).CompareTo(FindDurationFromListStage(tmp2)) :
                    FindDistanceFromListStage(tmp1).CompareTo(FindDistanceFromListStage(tmp2));
            return check;
        }
        public double FindDistanceFromListStage(List<Stage> a)
        {
            return a.Sum(item => item.Distance);
        }

        public double FindDurationFromListStage(List<Stage> a)
        {
            return a.Sum(stage => stage.Duration);
        }

        //processing middle trips
        public int ProcessingMiddleTrip(Request request)
        {
            int flag = 0;
            _foundWays.Clear();
            FindPath(request);
            if (!CheckNotNull(_foundWays)) return 0;
            var tmpResult = new List<Dictionary<Trip, int>>();
            foreach (var listStage in _foundWays)
            {
                var stages = listStage.FirstOrDefault(i => i.Key == request).Value;
                if (!CheckNotNull(stages)) continue;
                int count = stages.Count;
                //find one route
                var oneRoute = CheckSubWay(CutList(stages, 0, stages.Count - 1));
                if (CheckNotNull(oneRoute))
                {
                    if (flag == 0) flag = 1;
                    foreach (var route in oneRoute)
                    {
                        var stations = new List<int> { request.ToLocation };
                        var res = new List<Route> { route };
                        var solution = CheckResult(stations, res, request);
                        var tmpMap = new Dictionary<Trip, int>();
                        if (CheckNotNull(solution))
                        {
                            for (int index = 0; index < solution.Count; index++)
                            {
                                tmpMap.Add(solution[index], stations[index]);
                            }
                            tmpResult.Add(tmpMap);
                        }
                        else
                        {
                            if (flag == 1) flag = 2;
                        }
                    }
                }

                //find two routes
                for (int i = 0; i < count - 1; i++)
                {
                    var aList = CheckSubWay(CutList(stages, 0, i));
                    var bList = CheckSubWay(CutList(stages, i + 1, count - 1));
                    if (CheckNotNull(aList) && CheckNotNull(bList))
                    {
                        if (flag == 0) flag = 1;
                        foreach (var a in aList)
                        {
                            foreach (var b in bList)
                            {
                                var stations = new List<int> { stages[i].EndPoint, request.ToLocation };
                                var res = new List<Route> { a, b };
                                var solution = CheckResult(stations, res, request);
                                var tmpMap = new Dictionary<Trip, int>();
                                if (CheckNotNull(solution) && CheckDistinct(res))
                                {
                                    for (int index = 0; index < solution.Count; index++)
                                    {
                                        tmpMap.Add(solution[index], stations[index]);
                                    }
                                    tmpResult.Add(tmpMap);
                                }
                                else
                                {
                                    if (flag == 1) flag = 2;
                                }
                            }
                        }
                    }
                }
                //find three routes
                for (int i = 0; i <= count - 1; i++)
                {
                    for (int j = i + 1; j < count - 1; j++)
                    {
                        var aList = CheckSubWay(CutList(stages, 0, i));
                        var bList = CheckSubWay(CutList(stages, i + 1, j));
                        var cList = CheckSubWay(CutList(stages, j + 1, count - 1));
                        if (CheckNotNull(aList) && CheckNotNull(bList) && CheckNotNull(cList))
                        {
                            if (flag == 0) flag = 1;
                            foreach (var a in aList)
                            {
                                foreach (var b in bList)
                                {
                                    foreach (var c in cList)
                                    {
                                        var stations = new List<int>
                                            {
                                                stages[i].EndPoint,
                                                stages[j].EndPoint,
                                                request.ToLocation
                                            };
                                        var res = new List<Route> { a, b, c };
                                        var solution = CheckResult(stations, res, request);
                                        var tmpMap = new Dictionary<Trip, int>();
                                        if (CheckNotNull(solution) && CheckDistinct(res))
                                        {
                                            for (int index = 0; index < solution.Count; index++)
                                            {
                                                tmpMap.Add(solution[index], stations[index]);
                                            }
                                            tmpResult.Add(tmpMap);
                                        }
                                        else
                                        {
                                            if (flag == 1) flag = 2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //divide into 4 routes
                for (int i = 0; i <= count - 1; i++)
                {
                    for (int j = i + 1; j <= count - 1; j++)
                    {
                        for (int k = j + 1; k < count - 1; k++)
                        {
                            var aList = CheckSubWay(CutList(stages, 0, i));
                            var bList = CheckSubWay(CutList(stages, i + 1, j));
                            var cList = CheckSubWay(CutList(stages, j + 1, k));
                            var dList = CheckSubWay(CutList(stages, k + 1, count - 1));

                            if (CheckNotNull(aList) && CheckNotNull(bList) && CheckNotNull(cList) && CheckNotNull(dList))
                            {
                                if (flag == 0) flag = 1;
                                foreach (var a in aList)
                                {
                                    foreach (var b in bList)
                                    {
                                        foreach (var c in cList)
                                        {
                                            foreach (var d in dList)
                                            {
                                                var stations = new List<int>
                                                        {
                                                            stages[i].EndPoint,
                                                            stages[j].EndPoint,
                                                            stages[k].EndPoint,
                                                            request.ToLocation,
                                                        };
                                                var res = new List<Route> { a, b, c, d };
                                                var solution = CheckResult(stations, res, request);
                                                var tmpMap = new Dictionary<Trip, int>();
                                                if (CheckNotNull(solution) && CheckDistinct(res))
                                                {
                                                    for (int index = 0; index < solution.Count; index++)
                                                    {
                                                        tmpMap.Add(solution[index], stations[index]);
                                                    }
                                                    tmpResult.Add(tmpMap);
                                                }
                                                else
                                                {
                                                    if (flag == 1) flag = 2;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (tmpResult.Count > 0)
            {
                if (tmpResult.Count >= 2) tmpResult.Sort(CompareQualityOfSolution);
                _finalResult.Add(request, tmpResult.First());
            }
            if (flag == 1) return 1;
            if (flag == 2) return 2;
            return 3;
        }
        //check the list of route
        public bool CheckDistinct(List<Route> routes)
        {
            var tmp = new HashSet<Route>();
            foreach (var route in routes)
            {
                tmp.Add(route);
            }
            if (routes.Count == tmp.Count) return true;
            return false;
        }
        public float FindVolumeOfTripWithStation(Trip trip, int stationID)
        {
            float volume = 0;
            var requests = FindListRequestAssignToTrip(trip);
            if (!CheckNotNull(requests)) return (float)trip.EstimateVolume;
            foreach (var request in requests)
            {
                var listStation = FindStationFromAssignedRequest(request);
                foreach (var i in listStation)
                {
                    if (i == stationID)
                    {
                        var invoice = FindInvoiceFromRequest(request);
                        volume += (float)invoice.Volume;
                    }
                }
            }
            return ((float)trip.EstimateVolume - volume);
        }
        public List<int> FindStationFromAssignedRequest(Request request)
        {
            var stations = new HashSet<int>();
            var tmpMap = _finalResult.FirstOrDefault(i => i.Key == request).Value;
            var trips = tmpMap.Select(i => i.Key).ToList();
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
            int routeID = 0;
            foreach (var schedule in _schedules)
            {
                if (schedule.ScheduleID == trip.ScheduleID)
                {
                    routeID = schedule.RouteID;
                }
            }
            foreach (var stage in _stageOfRoute)
            {
                if (stage.Key.RouteID == routeID) return stage.Value;
            }
            return null;
        }
        public List<Request> FindListRequestAssignToTrip(Trip trip)
        {
            return (from result in _finalResult
                    let listTrip = result.Value
                    where listTrip.Any(t => t.Key == trip)
                    select result.Key).ToList();
        }

        public List<Trip> CheckResult(List<int> stations, List<Route> routes, Request request)
        {
            var resTrip = new List<Trip>();
            var volume = FindInvoiceFromRequest(request).Volume;
            var tmp = volume;
            var current = DateTime.Now;
            //iterate through each route to find the list of corresponding trips
            int index = -1;
            int count = routes.Count;
            foreach (var route in routes)
            {
                index++;
                var tmpListTrip = FindTripFromRoute(route.RouteID);
                if (!CheckNotNull(tmpListTrip))
                {
                    return null;
                }
                foreach (var trip in tmpListTrip)
                {
                    //if there is not trip in one of route, reject whole list route of current ways
                    var departure = trip.EstimateDepartureTime;
                    if (FindVolumeOfTripWithStation(trip, stations[index]) >= tmp &&
                        departure - current >= _deltaTime)
                    {
                        current = trip.EstimateDepartureTime.Add(FindTimeTripComeToStation(trip, stations[index]));
                        resTrip.Add(trip);
                        break;
                    }
                }
                //else, add the current solution to the list
            }
            return resTrip.Count == count ? resTrip : null;
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
        public List<Route> CheckSubWay(List<Stage> stages)
        {
            var resRoute = new List<Route>();
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
                if (flag == stages.Count) resRoute.Add(route);
            }
            if (CheckNotNull(resRoute)) return resRoute;
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
                        if (tmpStage.Count == 0) break;
                        Stage a = tmpStage.First();
                        Stage b = tmpStage.Last();
                        if (a.StartPoint == start && b.EndPoint == end) return route;
                    }
                }
            }
            return null;
        }
        //case multiples trips
        public List<Route> FindListRouteFromListStage(List<Stage> stages)
        {
            var a = new Route();
            var b = new Route();
            var c = new Route();
            var d = new Route();
            var res = new List<Route>();
            //check whether this is a whole route
            a = FindRouteFromStations(stages.First().StartPoint, stages.Last().EndPoint);
            if (a != null)
            {
                res = new List<Route> { a };
                return res;
            }
            int count = stages.Count;
            //divide into 2 routes
            for (int i = 0; i < count - 1; i++)
            {
                a = FindRouteFromStations(stages[0].StartPoint, stages[i].EndPoint);
                b = FindRouteFromStations(stages[i + 1].StartPoint, stages[count - 1].EndPoint);
                if (a != null && b != null)
                {
                    res = new List<Route> { a, b };
                    return res;
                }
            }
            //divide into 3 routes
            for (int i = 0; i <= count - 1; i++)
            {
                for (int j = i + 1; j < count - 1; j++)
                {
                    a = FindRouteFromStations(stages[0].StartPoint, stages[i].EndPoint);
                    b = FindRouteFromStations(stages[i + 1].StartPoint, stages[j].EndPoint);
                    c = FindRouteFromStations(stages[j + 1].StartPoint, stages[count - 1].EndPoint);
                    if (a != null && b != null && c != null)
                    {
                        res = new List<Route> { a, b, c };
                        return res;
                    }
                }
            }
            //divide into 4 routes
            for (int i = 0; i <= count - 1; i++)
            {
                for (int j = i + 1; j <= count - 1; j++)
                {
                    for (int k = j + 1; k < count - 1; k++)
                    {
                        a = FindRouteFromStations(stages[0].StartPoint, stages[i].EndPoint);
                        b = FindRouteFromStations(stages[i + 1].StartPoint, stages[j].EndPoint);
                        c = FindRouteFromStations(stages[j + 1].StartPoint, stages[k].EndPoint);
                        d = FindRouteFromStations(stages[k + 1].StartPoint, stages[count - 1].EndPoint);
                        if (a != null && b != null && c != null && d != null)
                        {
                            res = new List<Route> { a, b, c, d };
                            return res;
                        }
                    }
                }
            }
            //don't divide more but try divide each stage into a route
            foreach (var stage in stages)
            {
                var route = FindRouteFromStations(stage.StartPoint, stage.EndPoint);
                if (route == null) break;
                res.Add(route);
            }
            if (res.Count == stages.Count) return res;
            return null;
        }

        //find the time the trip come to a station on its route
        public TimeSpan FindTimeTripComeToStation(Trip trip, int stationID)
        {
            var listStage = FindListStageOfTrip(trip);
            var resTime = 0.0;
            foreach (var stage in listStage)
            {
                resTime += stage.Duration;
                if (stage.EndPoint == stationID)
                {
                    return TimeSpan.FromHours(resTime);
                }
            }
            return new TimeSpan(0, 0, 0);
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
            List<Station> nodes = adj.FirstOrDefault(x => x.Key == currNode).Value.ToList();
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
                            var stage = FindStageFromStation(visited[i], visited[i + 1]);
                            if (stage != null) tmpRoutes.Add(stage);
                            else
                            {
                                tmpRoutes.Clear();
                                break;
                            }
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
                double xBC = x[3] - x[2];
                double yBC = y[3] - y[2];
                double zBC = z[3] - z[2];
                double AB = Math.Sqrt(xAB * xAB + yAB * yAB + zAB * zAB);
                double BC = Math.Sqrt(xBC * xBC + yBC * yBC + zBC * zBC);
                //if the dot product of two vectors connect AB and BC is negative, reject immediately
                if (((xAB * xBC + yAB * yBC + zAB * zBC) / (AB * BC)) < Math.Cos(MaxAngle)) return false;
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