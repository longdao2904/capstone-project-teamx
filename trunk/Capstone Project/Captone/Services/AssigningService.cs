//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Captone.Models;
//using Captone.Repositories;
//using Captone.Repositories.Interfaces;
//using Captone.Services.Interfaces;
//using System.Web.Services;

//namespace Captone.Services
//{
//    public class AssigningService : IAssigningService
//    {
//        #region Declareration
//        //declare interface
//        private readonly GenericRepository<Route> _routeRepository =
//            new GenericRepository<Route>(new iDeliverEntities());
//        private readonly GenericRepository<Station> _stationRepository =
//            new GenericRepository<Station>(new iDeliverEntities());
//        private readonly GenericRepository<Invoice> _invoiceRepository =
//            new GenericRepository<Invoice>(new iDeliverEntities());
//        private readonly GenericRepository<Trip> _tripRepository =
//            new GenericRepository<Trip>(new iDeliverEntities());
//        private readonly GenericRepository<Request> _requestRepository =
//            new GenericRepository<Request>(new iDeliverEntities());
//        private readonly GenericRepository<Assigning> _assigningRepository =
//            new GenericRepository<Assigning>(new iDeliverEntities());
//        //declare class
//        private List<Invoice> _invoices = new List<Invoice>();
//        private List<Station> _stations = new List<Station>();
//        private List<Route> _routes = new List<Route>();
//        private List<Trip> _trips = new List<Trip>();
//        private List<Assigning> _assignings = new List<Assigning>();
//        //declare const
//        private readonly TimeSpan _maxTime = new TimeSpan(5, 0, 0, 0);
//        private readonly int _maxWay = 5;
//        private readonly TimeSpan _deltaTime = new TimeSpan(0,45,0);
//        //result of route
//        List<Dictionary<Request, List<Route>>> _tmpResult = new List<Dictionary<Request, List<Route>>>();
//        //result of trip
//        Dictionary<Request, List<Trip>> _finalResult = new Dictionary<Request, List<Trip>>();
//        //adjancent list of station
//        Dictionary<Station, List<Station>> adj = new Dictionary<Station, List<Station>>();

//        public AssigningService(GenericRepository<Route> routeRepository
//            , GenericRepository<Station> stationRepository
//            , GenericRepository<Invoice> invoiceRepository
//            , GenericRepository<Trip> tripRepository
//            , GenericRepository<Request> requestRepository
//            , GenericRepository<Assigning> assigningRepository)
//        {
//            _routeRepository = routeRepository;
//            _stationRepository = stationRepository;
//            _invoiceRepository = invoiceRepository;
//            _tripRepository = tripRepository;
//            _requestRepository = requestRepository;
//            _assigningRepository = assigningRepository;
//        }

//        #endregion

//        public AssigningService()
//        {
//            //Constructor without argument
//        }
//        private void InitData()
//        {
//            _routes = _routeRepository.GetAll().ToList();
//            _stations = _stationRepository.GetAll().ToList();
//            _invoices = _invoiceRepository.GetAll().ToList();
//            //right after list all trips from database, filter them to receive the availabel trip
//            _trips = _tripRepository.GetAll().ToList();
//            FilterTrip();
//            _assignings = _assigningRepository.GetAll().ToList();
//            //build the adjancient list
//            foreach (var station1 in _stations)
//            {
//                var tmp = new List<Station>();
//                foreach (var station2 in _stations)
//                {
//                    for (int i = 0; i < _routes.Count; i++)
//                    {
//                        //add to the adjance 
//                        if (_routes[i].StartPoint == station1.StationID &&
//                            _routes[i].EndPoint == station2.StationID)
//                        {
//                            tmp.Add(station2);
//                        }
//                    }
//                }
//                //insert the current adjance list of station to the full list
//                adj.Add(station1, tmp);
//            }
//        }

//        //accept trips have departure time later from now 45 min
//        public void FilterTrip()
//        {
//            List<Trip> tmpList = new List<Trip>();
//            foreach (var trip in _trips)
//            {
//                if (trip.EstimateDepartureTime == null) continue;
//                if (ChangeTime(trip.Date, (TimeSpan) trip.EstimateDepartureTime) - DateTime.Now >= _deltaTime 
//                    && trip.IsActive == true)
//                {
//                    tmpList.Add(trip);
//                }
//            }
//            _trips = tmpList;
//        }

//        //sort by the day of request increasing, if two request posted in the same day,
//        //then, sort by the weight of request increasing
//        public int RequestCompare(Request a, Request b)
//        {
//            Invoice tmp1 = FindInvoiceFromRequest(a);
//            Invoice tmp2 = FindInvoiceFromRequest(b);
//            //compare by date post request increasingly
//            if (tmp1.Price > tmp2.Price) return 1;
//            if (tmp1.Price == tmp2.Price)
//            {
//                if (tmp1.Volume > tmp2.Volume) return 1;
//                if (tmp1.Volume == tmp2.Volume) return 0;
//                return -1;
//            }
//            return -1;
//        }

//        public Invoice FindInvoiceFromRequest(Request request)
//        {
//            foreach (var invoice in _invoices)
//            {
//                if (request.RequestID == invoice.RequestID) return invoice;
//            }
//            return null;
//        }

//        //main function of assign, should call this from outside
//        public Dictionary<Request, List<Trip>> Assigning(List<Request> requests, DateTime date)
//        {
//            InitData();
//            if (requests == null) return _finalResult;
//            foreach (var request in requests)
//            {
//                if (FindInvoiceFromRequest(request) == null) return _finalResult;
//            }
//            //sort list request base on the day posted
//            requests.Sort(RequestCompare);
//            foreach (var request in requests)
//            {
//                int tmp = CheckOneTrip(request);
//                if (tmp > 0)
//                {
//                    ProcessingOneTrip(tmp, request, date);
//                }
//                else
//                {
//                    ProcessingMultipleTrip(request, date);
//                }
//            }
//            return _finalResult;
//        }
//        //check if there exist a trip connect two location of request
//        public int CheckOneTrip(Request request)
//        {
//            foreach (Route t in _routes)
//            {
//                if (t.StartPoint == request.FromLocation &&
//                    t.EndPoint == request.ToLocation) return t.RouteID;
//            }
//            return -1;
//        }

//        public Station FindStationFromRoute(Route route)
//        {
//            foreach (var station in _stations)
//            {
//                if (station.StationID == route.EndPoint) return station;
//            }
//            return _stations[0];
//        }
//        public List<Trip> FindTripFromRoute(int routeID)
//        {
//            var tripList = new List<Trip>();
//            for (int i = 0; i < _trips.Count; i++)
//            {
//                if (_trips[i].RouteID == routeID)
//                {
//                    tripList.Add(_trips[i]);
//                }
//            }
//            return tripList;
//        }
//        //processing for just one trip
//        public void ProcessingOneTrip(int routeID, Request request, DateTime date)
//        {
//            DateTime curr = DateTime.Now;
//            List<Trip> candidates = FindTripFromRoute(routeID);
//            foreach (var candidate in candidates)
//            {
//                if (candidate.EstimateArrivalTime != null)
//                {
//                    DateTime departure = ChangeTime(candidate.Date, (TimeSpan)candidate.EstimateArrivalTime);
//                    if (curr >= departure || (departure - curr) > _maxTime) continue;
//                }
//                Invoice invoice = FindInvoiceFromRequest(request);
//                if (candidate.AvailableVolume >= invoice.Volume)
//                {
//                    var tmpList = new List<Trip>();
//                    tmpList.Add(candidate);
//                    candidate.AvailableVolume -= invoice.Volume;
//                    _finalResult.Add(request, tmpList);
//                    return;
//                }
//            }
//        }

//        public void ProcessingMultipleTrip(Request request, DateTime date)
//        {
//            var deliveryTime = new TimeSpan();
//            var resTrip = new List<Trip>();
//            var volume = FindInvoiceFromRequest(request).Volume;
//            if (volume != null)
//            {
//                var tmp = (double)volume;
//                //find the path for delivery
//                FindPath(request);
//                if (_tmpResult == null)
//                    throw new NullReferenceException("Out of available trips");
//                //iterate through each of request to find the found list route
//                foreach (var res in _tmpResult)
//                {
//                    var current = DateTime.Now;
//                    var tmpListRoute = res.FirstOrDefault(x => x.Key == request).Value;
//                    if (tmpListRoute == null)
//                    {
//                        continue;
//                    }
//                    //iterate through each route to find the list of corresponding trips
//                    foreach (var route in tmpListRoute)
//                    {
//                        int flag = 0;
//                        List<Trip> tmpListTrip = FindTripFromRoute(route.RouteID);
//                        //if there is not trip in one of route, reject whole list route of current ways
//                        if (tmpListTrip == null)
//                        {
//                            continue;
//                        }
//                        foreach (var trip in tmpListTrip)
//                        {
//                            if (trip.AvailableVolume < tmp) flag = -1;
//                            if (trip.EstimateDepartureTime != null)
//                            {
//                                var departure = ChangeTime(trip.Date, (TimeSpan) trip.EstimateDepartureTime);
//                                if (departure >= current)
//                                {
//                                    if (trip.EstimateArrivalTime != null)
//                                    {
//                                        var arrival = ChangeTime(trip.Date, (TimeSpan) trip.EstimateArrivalTime);
//                                        deliveryTime += (arrival - current);
//                                        resTrip.Add(trip);
//                                        var breakTime = FindStationFromRoute(route).BreakTime;
//                                        if (breakTime != null)
//                                            current = arrival.AddHours((double) breakTime);
//                                        break;
//                                    }
//                                    else flag = -1;
//                                }
//                                else flag = -1;
//                            }
//                            else flag = -1;
//                        }
//                        if (deliveryTime > _maxTime || flag == -1)
//                        {
//                            resTrip.Clear();
//                            break;
//                        }
//                    }
//                    if (resTrip.Count > 0)
//                    {
//                        _finalResult.Add(request, resTrip);
//                        return;
//                    }
//                }
//            }
//        }

//        public DateTime ChangeTime(DateTime a, TimeSpan b)
//        {
//            DateTime result = a;
//            result = result.AddHours(b.Days);
//            result = result.AddHours(b.Hours);
//            result = result.AddMinutes(b.Minutes);
//            return result;
//        }
//        //find all path between two station of the request

//        public List<Route> FindPath(Request request)
//        {
//            var fromStation = new Station();
//            var toStation = new Station();
//            var listRoute = new List<Route>();
//            int count = 0;
//            for (int i = 0; i < _stations.Count && count < 2; i++)
//            {
//                if (_stations[i].StationID == request.FromLocation)
//                {
//                    fromStation = _stations[i];
//                    count++;
//                }
//                if (_stations[i].StationID == request.ToLocation)
//                {
//                    toStation = _stations[i];
//                    count++;
//                }
//            }
//            //recursion to find all path using BFS + check not go backward
//            var visited = new List<Station>();
//            visited.Add(fromStation);
//            //after call this method, all path connect two station of the request
//            BreadthFirstSearch(request, toStation, visited);
//            return listRoute;
//        }

//        //BreadthFirstSearch to find all path

//        public void BreadthFirstSearch(Request request, Station toStation, List<Station> visited)
//        {
//            Station currNode = visited.Last();
//            List<Station> nodes = adj.FirstOrDefault(x => x.Key == currNode).Value;
//            foreach (var station in nodes)
//            {
//                if (visited.Contains(station)) continue;
//                if (station.Equals(toStation))
//                {
//                    visited.Add(station);
//                    if (CheckForward(visited))
//                    {
//                        var tmpRoutes = new List<Route>();
//                        for (int i = 0; i < visited.Count - 1; i++)
//                        {
//                            tmpRoutes.Add(FindRouteFromStation(visited[i], visited[i + 1]));
//                        }
//                        var tmp = new Dictionary<Request, List<Route>>();
//                        tmp.Add(request, tmpRoutes);
//                        _tmpResult.Add(tmp);
//                        //allow 5 ways in searching -> the complexity of algorithm is approximate O(_maxWay|E||V|)
//                        if (_tmpResult.Count > _maxWay) return;
//                    }
//                    visited.Remove(visited.Last());
//                    break;
//                }
//            }
//            foreach (var station in nodes)
//            {
//                if (visited.Contains(station) || station.Equals(toStation)) continue;
//                visited.Add(station);
//                BreadthFirstSearch(request, toStation, visited);
//                visited.Remove(visited.Last());
//            }
//        }

//        //find the route that connect two station

//        public Route FindRouteFromStation(Station a, Station b)
//        {
//            foreach (Route t in _routes)
//            {
//                if (t.StartPoint == a.StationID &&
//                    t.EndPoint == b.StationID) return t;
//            }
//            return _routes[0];
//        }
        
//        //check the diection always forward

//        public bool CheckForward(List<Station> stationsForCheck)
//        {
//            //iterate through list of station, if there are 3 stations not lie forward, return false
//            for (int i = 0; i < stationsForCheck.Count() - 2; i++)
//            {
//                string coordinateA = stationsForCheck[i].Coordinate;
//                string coordinateB = stationsForCheck[i + 1].Coordinate;
//                string coordinateC = stationsForCheck[i + 2].Coordinate;
//                string tmp = coordinateA + "," + coordinateB + "," + coordinateC;
//                string[] separator = { ",", " " };
//                string[] words = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);
//                var longitude = new double[10];
//                var latitude = new double[10];
//                int c = 0, numLongitude = 0, numLatitude = 0;
//                //convert longitude and latitude from coordinates
//                foreach (var word in words)
//                {
//                    if (c % 2 == 1)
//                    {
//                        longitude[++numLongitude] = double.Parse(word);
//                    }
//                    else
//                    {
//                        latitude[++numLatitude] = double.Parse(word);
//                    }
//                    c++;
//                }
//                var x = new double[10];
//                var y = new double[10];
//                var z = new double[10];
//                double xAB, yAB, zAB, xBC, yBC, zBC;
                
//                //convert longitude and lititude into 3D coordinate
//                //base on the link: http://math.stackexchange.com/questions/441182/how-to-check-if-three-coordinates-form-a-line

//                for (int j = 1; j <= 3; j++)
//                {
//                    x[j] = Math.Cos(Math.PI*longitude[j]/180)*Math.Cos(Math.PI*latitude[j]/180);
//                    y[j] = Math.Sin(Math.PI*longitude[j]/180)*Math.Cos(Math.PI*latitude[j]/180);
//                    z[j] = Math.Sin(Math.PI*latitude[j]/180);
//                }
//                xAB = x[2] - x[1];
//                yAB = y[2] - y[1];
//                zAB = z[2] - z[1];
//                xBC = x[3] - z[2];
//                yBC = y[3] - y[2];
//                zBC = z[3] - z[2];
//                //if the dot product of vector connect AB and BC is negative, reject immediately
//                if (xAB*xBC + yAB*yBC + zAB*zBC < 0) return false;
//            }
//            return true;
//        }
//        //when delete a trip or list of trips, call this method to change the status of
//        //processing request to "Đã xác nhận" then re-schedule

//        [System.Web.Mvc.HttpPost]
//        [WebMethod]
//        public void Update(List<int> requestIDs, int tripID)
//        {
//            var db = new iDeliverEntities();
//            var tmpRequestID = new List<int>();
//            //find the list of request id that related to delete trips

//            foreach (var assigning in _assignings)
//            {
//                if (assigning.TripID == tripID)
//                {
//                    tmpRequestID.Add(assigning.RequestID);
//                }
//            }

//            //update the status of all request in the list
            
//            foreach (var id in requestIDs)
//            {
//                Request request = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
//                foreach (var i in tmpRequestID)
//                {
//                    if (request.RequestID == i)
//                    {
//                        request.DeliveryStatusID = 1;
//                        _requestRepository.Update(request);
//                    }
//                }
//            }
//        }
//    }
//}