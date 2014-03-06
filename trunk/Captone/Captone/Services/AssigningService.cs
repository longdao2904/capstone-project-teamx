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
        //declare interface
        private readonly IRouteRepository _routeRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITripRepository _tripRepository;
        //declare class
        private List<Invoice> _invoices = new List<Invoice>();
        private List<Station> _stations = new List<Station>();
        private List<Route> _routes = new List<Route>(); 
        private List<Trip> _trips = new List<Trip>();
        private readonly TimeSpan _maxTime = new TimeSpan(2,0,0,0);
        //result of route
        List<Dictionary<Request, List<Route>>> _tmpResult = new List<Dictionary<Request, List<Route>>>();
        //result of trip
        Dictionary<Request, List<Trip>> _finalResult = new Dictionary<Request, List<Trip>>();
        //adjancent list of station
        Dictionary<Station, List<Station>> adj = new Dictionary<Station, List<Station>>();

        public AssigningService(IRouteRepository routeRepository
            , IStationRepository stationRepository
            , IInvoiceRepository invoiceRepository
            , ITripRepository tripRepository)
        {
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;
            _invoiceRepository = invoiceRepository;
            _tripRepository = tripRepository;
        }

        private void InitData()
        {
            _routes = _routeRepository.GetAllRoutes().ToList();
            _stations = _stationRepository.GetAllStations().ToList();
            _invoices = _invoiceRepository.GetAllInvoices().ToList();
            _trips = _tripRepository.GetAllTrips().ToList();
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
            return _invoices[0];
        }

        //main function of assign, should call this from outside
        public Dictionary<Request, List<Trip>> Assigning(List<Request> requests, List<Trip> trips, DateTime date)
        {
            InitData();
            //list all station
            //list all pending request
            //sort list request base on the day posted
            requests.Sort(RequestCompare);
            foreach (var request in requests)
            {
                int tmp = CheckOneTrip(request);
                if (tmp > 0)
                {
                    ProcessingOneTrip(tmp, request, trips, date);
                }
                else
                {
                    ProcessingMultipleTrip(request, trips, date);
                }
            }
            return _finalResult;
        }
        //check if there exist a trip connect two location of request
        public int CheckOneTrip(Request request)
        {
            for (int i = 0; i < _routes.Count; i++)
            {
                if (_routes[i].StartPoint == request.FromLocation &&
                    _routes[i].EndPoint == request.ToLocation) return _routes[i].RouteID;
            }
            return -1;
        }

        public List<Trip> FindTripFromRoute(int routeID, List<Trip> trips)
        {
            List<Trip> tripList = new List<Trip>();
            for (int i = 0; i < trips.Count; i++)
            {
                if (trips[i].RouteID == routeID)
                {
                    tripList.Add(trips[i]);
                }
            }
            return tripList;
        }
        //processing for just one trip
        public void ProcessingOneTrip(int routeID, Request request, List<Trip> trips, DateTime date)
        {
            List<Trip> candidates = FindTripFromRoute(routeID, trips);
            int flag = -1;
            foreach (var candidate in candidates)
            {
                Invoice invoice = FindInvoiceFromRequest(request);
                if (candidate.AvailableVolume >= invoice.Volume)
                {
                    flag = 0;
                    List<Trip> tmpList = new List<Trip>();
                    tmpList.Add(candidate);
                    candidate.AvailableVolume -= invoice.Volume;
                    _finalResult.Add(request, tmpList);
                }
            }
            if (flag == -1)
            {
                ProcessingMultipleTrip(request, trips, date);
            }
        }

        public void ProcessingMultipleTrip(Request request, List<Trip> trips, DateTime date)
        {
            var current = new DateTimeOffset(date);
            var deliveryTime = new TimeSpan();
            var resTrip = new List<Trip>();
            FindPath(request);
            foreach (var res in _tmpResult)
            {
                var tmpListRoute = new List<Route>();
                tmpListRoute = res.FirstOrDefault(x => x.Key == request).Value;
                foreach (var route in tmpListRoute)
                {
                    List<Trip> tmpListTrip = FindTripFromRoute(route.RouteID, trips);
                    foreach (var trip in tmpListTrip)
                    {
                        var tmp = new DateTimeOffset(trip.Date);
                        tmp.Add((TimeSpan) trip.EstimateDepartureTime);
                        if (tmp >= current)
                        {
                            deliveryTime += (tmp - current);
                            resTrip.Add(trip);
                        }
                    }
                    if (deliveryTime > _maxTime)
                    {
                        //deliveryTime = TimeSpan(0, 0, 0);
                        resTrip.Clear();
                        break;
                    }
                }
                if(resTrip.Count > 0) _finalResult.Add(request, resTrip);
            }
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
                            tmpRoutes.Add(FindRouteFromStation(visited[i], visited[i+1]));
                        }
                        var tmp = new Dictionary<Request, List<Route>>();
                        tmp.Add(request, tmpRoutes);
                        _tmpResult.Add(tmp);
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
            for (int i = 0; i < _routes.Count; i++)
            {
                if (_routes[i].StartPoint == a.StationID &&
                    _routes[i].EndPoint == b.StationID) return _routes[i];
            }
            return _routes[0];
        }

        //check the diection always forward

        public bool CheckForward(List<Station> stationsForCheck)
        {
            for (int i = 0; i < stationsForCheck.Count() - 2; i++)
            {
                string coordinateA = stationsForCheck[i].Coordinate;
                string coordinateB = stationsForCheck[i+1].Coordinate;
                string coordinateC = stationsForCheck[i+2].Coordinate;
                string tmp = coordinateA + "," + coordinateB + "," + coordinateC;
                string[] separator = { ",", " " };
                string[] words = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                var longitude = new double[10];
                var latitude = new double[10];
                int c = 0, numLongitude = -1, numLatitude = -1;
                foreach (var word in words)
                {
                    c++;
                    if (c % 2 == 1)
                    {
                        longitude[++numLongitude] = double.Parse(word);
                    }
                    else
                    {
                        latitude[++numLatitude] = double.Parse(word);
                    }
                }
                //find the cross product of two vector
                double X_AB = (longitude[1] - longitude[2]), X_BC = longitude[2] - longitude[3];
                double Y_AB = (latitude[1] - latitude[2]), Y_BC = latitude[2] - latitude[3];
                //if the value is negative, then the angle is obtuse and reject this case
                if (X_AB * X_BC + Y_AB * Y_BC <= 0) return false;
            }
            return true;
        }

        //not implemement yet
        public void Update(List<Request> requests, List<Trip> trips, DateTime date)
        {
            var result = new Dictionary<Request, List<Trip>>();
        }
    }
}