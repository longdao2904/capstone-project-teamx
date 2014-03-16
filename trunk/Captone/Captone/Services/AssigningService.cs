using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

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
        //declare class
        private List<Invoice> _invoices = new List<Invoice>();
        private List<Station> _stations = new List<Station>();
        private List<Route> _routes = new List<Route>();
        private List<Trip> _trips = new List<Trip>();
        private List<Assigning> _assignings = new List<Assigning>();
        //declare const
        private readonly TimeSpan _maxTime = new TimeSpan(5, 0, 0, 0);
        private readonly int _maxWay = 5;
        //result of route
        List<Dictionary<Request, List<Route>>> _tmpResult = new List<Dictionary<Request, List<Route>>>();
        //result of trip
        Dictionary<Request, List<Trip>> _finalResult = new Dictionary<Request, List<Trip>>();
        //adjancent list of station
        Dictionary<Station, List<Station>> adj = new Dictionary<Station, List<Station>>();

        public AssigningService(GenericRepository<Route> routeRepository
            , GenericRepository<Station> stationRepository
            , GenericRepository<Invoice> invoiceRepository
            , GenericRepository<Trip> tripRepository
            , GenericRepository<Request> requestRepository
            , GenericRepository<Assigning> assigningRepository)
        {
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;
            _invoiceRepository = invoiceRepository;
            _tripRepository = tripRepository;
            _requestRepository = requestRepository;
            _assigningRepository = assigningRepository;
        }

        #endregion

        public AssigningService()
        {
            //Constructor without argument
        }
        private void InitData()
        {
            _routes = _routeRepository.GetAll().ToList();
            _stations = _stationRepository.GetAll().ToList();
            _invoices = _invoiceRepository.GetAll().ToList();
            _trips = _tripRepository.GetAll().ToList();
            _assignings = _assigningRepository.GetAll().ToList();
            //build the adjancient list
            foreach (var station1 in _stations)
            {
                var tmp = new List<Station>();
                foreach (var station2 in _stations)
                {
                    for (int i = 0; i < _routes.Count; i++)
                    {
                        //add to the adjance 
                        if (_routes[i].StartPoint == station1.StationID &&
                            _routes[i].EndPoint == station2.StationID)
                        {
                            tmp.Add(station2);
                        }
                    }
                }
                //insert the current adjance list of station to the full list
                adj.Add(station1, tmp);
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
                if (tmp1.Weight > tmp2.Weight) return 1;
                if (tmp1.Weight == tmp2.Weight) return 0;
                return -1;
            }
            return -1;
        }

        public Invoice FindInvoiceFromRequest(Request request)
        {
            foreach (var invoice in _invoices)
            {
                if (request.RequestID == invoice.RequestID) return invoice;
            }
            return null;
        }

        //main function of assign, should call this from outside
        public Dictionary<Request, List<Trip>> Assigning(List<Request> requests, DateTime date)
        {
            InitData();
            if (requests == null) return _finalResult;
            foreach (var request in requests)
            {
                if (FindInvoiceFromRequest(request) == null) return _finalResult;
            }
            //sort list request base on the day posted
            requests.Sort(RequestCompare);
            foreach (var request in requests)
            {
                int tmp = CheckOneTrip(request);
                if (tmp > 0)
                {
                    ProcessingOneTrip(tmp, request, date);
                }
                else
                {
                    ProcessingMultipleTrip(request, date);
                }
            }
            return _finalResult;
        }
        //check if there exist a trip connect two location of request
        public int CheckOneTrip(Request request)
        {
            foreach (Route t in _routes)
            {
                if (t.StartPoint == request.FromLocation &&
                    t.EndPoint == request.ToLocation) return t.RouteID;
            }
            return -1;
        }

        public Station FindStationFromRoute(Route route)
        {
            foreach (var station in _stations)
            {
                if (station.StationID == route.EndPoint) return station;
            }
            return _stations[0];
        }
        public List<Trip> FindTripFromRoute(int routeID)
        {
            var tripList = new List<Trip>();
            for (int i = 0; i < _trips.Count; i++)
            {
                if (_trips[i].RouteID == routeID)
                {
                    tripList.Add(_trips[i]);
                }
            }
            return tripList;
        }
        //processing for just one trip
        public void ProcessingOneTrip(int routeID, Request request, DateTime date)
        {
            DateTime curr = DateTime.Now;
            List<Trip> candidates = FindTripFromRoute(routeID);
            foreach (var candidate in candidates)
            {
                if (candidate.EstimateArrivalTime != null)
                {
                    DateTime departure = ChangeTime(candidate.Date, (TimeSpan)candidate.EstimateArrivalTime);
                    if (curr >= departure || (departure - curr) > _maxTime) continue;
                }
                Invoice invoice = FindInvoiceFromRequest(request);
                if (candidate.AvailableVolume >= invoice.Volume)
                {
                    var tmpList = new List<Trip>();
                    tmpList.Add(candidate);
                    candidate.AvailableVolume -= invoice.Volume;
                    _finalResult.Add(request, tmpList);
                    return;
                }
            }
            ProcessingMultipleTrip(request, date);
        }

        public void ProcessingMultipleTrip(Request request, DateTime date)
        {
            var deliveryTime = new TimeSpan();
            var resTrip = new List<Trip>();
            var volume = FindInvoiceFromRequest(request).Volume;
            if (volume != null)
            {
                var tmp = (double)volume;
                FindPath(request);
                if (_tmpResult == null)
                    throw new NullReferenceException("Out of available trips");
                foreach (var res in _tmpResult)
                {
                    var current = DateTime.Now;
                    var tmpListRoute = res.FirstOrDefault(x => x.Key == request).Value;
                    if (tmpListRoute == null)
                    {
                        continue;
                    }
                    foreach (var route in tmpListRoute)
                    {
                        int flag = 0;
                        List<Trip> tmpListTrip = FindTripFromRoute(route.RouteID);
                        if (tmpListTrip == null)
                        {
                            continue;
                        }
                        foreach (var trip in tmpListTrip)
                        {
                            if (trip.AvailableVolume < tmp) flag = -1;
                            if (trip.EstimateDepartureTime != null)
                            {
                                var departure = ChangeTime(trip.Date, (TimeSpan)trip.EstimateDepartureTime);
                                if (departure >= current)
                                {
                                    if (trip.EstimateArrivalTime != null)
                                    {
                                        var arrival = ChangeTime(trip.Date, (TimeSpan)trip.EstimateArrivalTime);
                                        deliveryTime += (arrival - current);
                                        resTrip.Add(trip);
                                        var breakTime = FindStationFromRoute(route).BreakTime;
                                        if (breakTime != null)
                                            current = arrival.AddHours((double)breakTime);
                                    }
                                }
                            }
                        }
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

        public DateTime ChangeTime(DateTime a, TimeSpan b)
        {
            DateTime result = a;
            result = result.AddHours(b.Days);
            result = result.AddHours(b.Hours);
            result = result.AddMinutes(b.Minutes);
            return result;
        }
        //find all path between two station of the request

        public List<Route> FindPath(Request request)
        {
            var fromStation = new Station();
            var toStation = new Station();
            var listRoute = new List<Route>();

            for (int i = 0; i < _stations.Count; i++)
            {
                if (_stations[i].StationID == request.FromLocation)
                {
                    fromStation = _stations[i];
                }
                if (_stations[i].StationID == request.ToLocation)
                {
                    toStation = _stations[i];
                }
            }
            //recursion to find all path using BFS + check not go backward
            var visited = new List<Station>();
            visited.Add(fromStation);
            //after call this method, all path connect two station of the request
            BreadthFirstSearch(request, toStation, visited);
            return listRoute;
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
                        var tmpRoutes = new List<Route>();
                        for (int i = 0; i < visited.Count - 1; i++)
                        {
                            tmpRoutes.Add(FindRouteFromStation(visited[i], visited[i + 1]));
                        }
                        var tmp = new Dictionary<Request, List<Route>>();
                        tmp.Add(request, tmpRoutes);
                        _tmpResult.Add(tmp);
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

        //find the route that connect two station

        public Route FindRouteFromStation(Station a, Station b)
        {
            foreach (Route t in _routes)
            {
                if (t.StartPoint == a.StationID &&
                    t.EndPoint == b.StationID) return t;
            }
            return _routes[0];
        }

        //check the diection always forward

        public bool CheckForward(List<Station> stationsForCheck)
        {
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
                //double X_AB = (longitude[1] - longitude[2]), X_BC = longitude[2] - longitude[3];
                //double Y_AB = (latitude[1] - latitude[2]), Y_BC = latitude[2] - latitude[3];
                //if (X_AB * X_BC + Y_AB * Y_BC <= 0) return true;
                if ((longitude[1] - longitude[2]) * (longitude[2] - longitude[3]) < 0) return false;
            }
            return true;
        }

        //when delete a trip or list of trips, call this method to change the status of
        //processing request to "Đã xác nhận" then re-schedule
        public void Update(List<Request> requests, List<Trip> trips, DateTime date)
        {
            var tmpRequestID = new List<int>();
            //find the list of request id that related to delete trips
            foreach (var trip in trips)
            {
                foreach (var assigning in _assignings)
                {
                    if (assigning.TripID == trip.TripID)
                    {
                        tmpRequestID.Add(assigning.RequestID);
                    }
                }
            }
            //update the status of all request in the list
            foreach (var request in requests)
            {
                foreach (var i in tmpRequestID)
                {
                    if (request.RequestID == i)
                    {
                        request.DeliveryStatusID = 1;
                        _requestRepository.Update(request);
                    }
                }
            }
        }
    }
}