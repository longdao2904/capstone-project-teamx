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
        private readonly ICoachRepository _coachRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IRequestRepository _requestRepository;
        //declare class
        private List<Invoice> invoices = new List<Invoice>();
        private List<Request> requests = new List<Request>();
        private List<Station> stations = new List<Station>();
        private List<Route> routes = new List<Route>(); 
        private List<Coach> coaches = new List<Coach>();
        private List<Trip> trips = new List<Trip>();
        private int max = 20, maxVolume = 100, maxWeight = 100;

        private void InitData()
        {
            coaches = _coachRepository.GetAllCoaches().ToList();
            trips = _tripRepository.GetAllTrips().ToList();
            routes = _routeRepository.GetAllRoutes().ToList();
            stations = _stationRepository.GetAllStations().ToList();
            invoices = _invoiceRepository.GetAllInvoices().ToList();
            requests = _requestRepository.GetAllRequests().ToList();
        }
        //sort by the day of request increasing, if two request posted in the same day,
        //then, sort by the weight of request increasing
        public int RequestCompare(Request a, Request b)
        {
            Invoice tmp1 = new Invoice();
            Invoice tmp2 = new Invoice();
            //find the invoice suitable with request
            for (int i = 0; i < invoices.Count; i++)
            {
                if (invoices[i].RequestID == a.RequestID)
                {
                    tmp1 = invoices[i];
                }
                if (invoices[i].RequestID == b.RequestID)
                {
                    tmp2 = invoices[i];
                }
            }
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

        //main function of assign, should call this from outside
        public Dictionary<Request, List<Trip>> Assigning()
        {
            var result = new Dictionary<Request, List<Trip>>();
            //list all station
            List<Station> station = new List<Station>();
            station = _stationRepository.GetAllStations();
            //list all pending request
            List<Request> requests = new List<Request>();
            requests = _requestRepository.GetPendingRequest().ToList();
            //sort list request base on the day posted
            requests.Sort(RequestCompare);
            //iterate through the list of station and find out the list of request corresponding
            for (int i = 0; i < station.Count; i++)
            {
                List<Request> tmp = new List<Request>();
                for (int j = 0; j < requests.Count; j++)
                {
                    if (requests[j].ToLocation == station[i].StationID)
                    {
                        tmp.Add(requests[j]);   
                    }
                }
                //processing for one station
                var list = ProcessingForStation(station[i], tmp);
                foreach (var pair in list)
                {
                    result.Add(pair.Key, pair.Value);
                }
            }
            return result;
        }

        public Dictionary<Request, List<Trip>> ProcessingForStation(Station station, List<Request> requests)
        {
            var result = new Dictionary<Request, List<Trip>>();
            Queue<Trip> trip = new Queue<Trip>();
            while (trip.Count > 0)
            {
                Trip tmp = trip.First();
                //dynamic programming here
                int[,] dp = new int[maxVolume,maxWeight];
                dp[0, 0] = 0;
                for (int i = 0; i < requests.Count; i++)
                {
                    dp[0, i] = 0;
                }
                //round to 1 digit after , 
                int volume = (int)(Math.Round((double)tmp.AvailableVolume*10));
                for (int i = 0; i < requests.Count; i++)
                {
                    for (int j = 0; j < volume; j++)
                    {
                        string[] words = requests[j].EstimateVolume.Split('-');
                        int requestVolume = Int32.Parse(words[1])*10;
                        if (requestVolume > i) dp[i, j] = dp[i - 1, j];
                        else dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - requestVolume]);
                    }
                }
                List<int> removeList = new List<int>();
                for (int i = 0; i < requests.Count; i++)
                {
                    for (int j = 0; j < volume; j++)
                    {
                        if (dp[i, j] != dp[i - 1, j]) removeList.Add(i);
                    }
                }
                //remove the processed request
                for (int i = removeList.Count - 1; i >= 0; i--)
                {
                    requests[i].DeliveryStatusID = 2;
                    requests.RemoveAt(removeList[i]);
                }
            }

            return result;
        }

        public List<Route> FindPath(Request request)
        {
            List<Route> listRoute = new List<Route>();
            int fromStation = request.FromLocation;
            int toStation = request.ToLocation;
            //construct the graph G=(V,E) with V is list of staion, E is list of route connect two station
            var routes = _routeRepository.GetAllRoutes().ToList();
            var station = _stationRepository.GetAllStations().ToList();
            int node = station.Count();
            int[,] adj = new int[max,max];
            //init the adjancient matrix
            for (int i = 0; i < node; i++)
            {
                for (int j = 0; j < node; j++)
                {
                    adj[i, j] = 0;
                }
            }
            //build graph with real information
            for (int i = 0; i < node; i++)
            {
                for (int j = 0; j < node; j++)
                {
                    if(i == j) continue;
                    for (int k = 0; k < routes.Count; k++)
                    {
                        if (routes[k].StartPoint == station[i].StationID &&
                            routes[k].EndPoint == station[j].StationID) adj[i, j] = 1;
                    }
                }
            }

            //using bfs without recursion to find the path
            Queue<int> queue = new Queue<int>();
            int[] trace = new int[max];

            queue.Enqueue(fromStation);
            while (queue.Count > 0)
            {
                int u = queue.First(); queue.Dequeue();
                for (int v = 0; v < queue.Count; v++)
                {
                    if (adj[u,v] != 0 && trace[v] == 0)
                    {
                        queue.Enqueue(v);
                        trace[v] = u;
                    }
                }
            }
            int temp = toStation;
            while (temp != fromStation)
            {
                for (int i = 0; i < routes.Count; i++)
                {
                    if (routes[i].RouteID == temp)
                    {
                        listRoute.Add(routes[i]);
                    }
                }
                temp = trace[temp];
            }
            return listRoute;
        }

        //check the diection always forward
        public bool CheckForward(string coordinateA, string coordinateB, string coordinateC)
        {
            string tmp = coordinateA + "," + coordinateB + "," + coordinateC;
            string[] separator = {",", " "};
            string[] words = tmp.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            double[] longitude = new double[10];
            double[] latitude = new double[10];
            int c = 0, numLongitude = -1, numLatitude = -1;
            foreach (var word in words)
            {
                c++;
                if (c%2 == 1)
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
            if (X_AB*X_BC + Y_AB*Y_BC <= 0) return false;
            return true;
        }
    }
}