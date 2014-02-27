﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services.Interfaces;

namespace Captone.Services
{
    public class AssigningService : IAssigningService
    {
        private readonly ICoachRepository _coachRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IStationRepository _stationRepository;
        private int max = 20;
        public AssigningService(ICoachRepository coachRepository, ITripRepository tripRepository,
                                IRouteRepository routeRepository, IStationRepository stationRepository)
        {
            _coachRepository = coachRepository;
            _tripRepository = tripRepository;
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;
        }

        public List<Route> ProcessingRequest(Request request)
        {
            List<Route> listRoute = new List<Route> {};
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
        public bool checkForward(string coordinateA, string coordinateB, string coordinateC)
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