using System;
using System.Collections.Generic;
using System.Linq;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capstone.Test
{
    [TestClass]
    public partial class AssigningServiceTest1
    {
        //setup mock object
        private readonly Mock<IRouteRepository> _route = new Mock<IRouteRepository>();
        private readonly Mock<IStationRepository> _station = new Mock<IStationRepository>();
        private readonly Mock<IInvoiceRepository> _invoice = new Mock<IInvoiceRepository>();
        private readonly Mock<ITripRepository> _trip = new Mock<ITripRepository>();
        DateTime now = new DateTime(2014,03,07);

        [TestMethod]
        public void MainFlow_Test()
        {
            // Description of this test case:
            // There are routes and trips available between:
            // SG - Đà Lạt, SG - Vũng Tàu, Vũng Tàu - Đà Nẵng, SG - Đà Nẵng
            // Request1: SG - Vũng Tàu, Request2: SG - Đà Nẵng
            // Expected result is one request dedicated to one trip
            //setup input / testcase
            var requests = new List<Request>
                {
                    new Request{RequestID = 1,
                                FromLocation = 1,
                                ToLocation = 2,
                                DateRequest = now,
                                DeliveryStatusID = 1},
                    new Request{RequestID = 2,
                                FromLocation = 1,
                                ToLocation = 4,
                                DateRequest = now,
                                DeliveryStatusID = 1},
                };
            var trips = new List<Trip>
                {
                    new Trip{TripID = 1, 
                             EstimateArrivalTime = new TimeSpan(10, 10, 10),
                             EstimateDepartureTime = new TimeSpan(10, 10, 10),
                             AvailableVolume = 1.0,
                             Date = now,
                             RouteID = 1,
                    },
                    new Trip{TripID = 2, 
                             EstimateArrivalTime = new TimeSpan(10, 10, 10),
                             EstimateDepartureTime = new TimeSpan(10, 10, 10),
                             AvailableVolume = 1.0,
                             Date = now,
                             RouteID = 2,
                    },
                    new Trip{TripID = 3, 
                             EstimateArrivalTime = new TimeSpan(10, 10, 10),
                             EstimateDepartureTime = new TimeSpan(10, 10, 10),
                             AvailableVolume = 2.5,
                             Date = now,
                             RouteID = 3,
                    },
                    new Trip{TripID = 4, 
                             EstimateArrivalTime = new TimeSpan(10, 10, 10),
                             EstimateDepartureTime = new TimeSpan(10, 10, 10),
                             EstimateVolume = 1.1,
                             AvailableVolume = 1.5,
                             Date = now,
                             RouteID = 4,
                    },
                };
            var date = DateTime.Now;

            //data set into mock object
            var routes = new List<Route>
                {
                    new Route{RouteID = 1,
                              RouteName = "Sài Gòn - Vũng Tàu",
                              StartPoint = 1, 
                              EndPoint = 2,
                              Duration = 1.3,
                              Distance = 5},
                    new Route{RouteID = 2,
                              RouteName = "Sài Gòn - Đà Lạt",
                              StartPoint = 1, 
                              EndPoint = 3,
                              Duration = 8.1,
                              Distance = 6},
                    new Route{RouteID = 3,
                              RouteName = "Sài Gòn - Đà Nẵng",
                              StartPoint = 1, 
                              EndPoint = 4,
                              Duration = 8.1,
                              Distance = 6},
                    new Route{RouteID = 4,
                              RouteName = "Vũng Tàu - Đà Nẵng",
                              StartPoint = 3, 
                              EndPoint = 4,
                              Duration = 8.1,
                              Distance = 6},
                };
            var stations = new List<Station>
                {
                    new Station{StationID = 1,
                                StationName = "SG.Lê Hồng Phong",
                                StationLocation = "233 Lê Hồng Phong, F4, Q.5, TP Hồ Chí Minh",
                                BreakTime = 2,
                                Coordinate = "10.7584848, 106.6777557",
                                ProvinceID = 1},
                    new Station{StationID = 2,
                                StationName = "Tạ Uyên - VT",
                                StationLocation = "03 Tạ Uyên, Vũng Tàu",
                                BreakTime = 1.5,
                                Coordinate = "10.3491804, 107.0855695",
                                ProvinceID = 5},
                    new Station{StationID = 3,
                                StationName = "Bến xe liên tỉnh Đà Lạt",
                                StationLocation = "01 Tô Hiến Thành, F3, Đà Lạt",
                                BreakTime = 2.5,
                                Coordinate = "11.9264949, 108.4460601",
                                ProvinceID = 4},
                    new Station{StationID = 4,
                                StationName = "Bến Xe Trung Tâm Đà Nẵng",
                                StationLocation = "201 Tôn Đức Thắng, Phường Hòa Minh, Liên Chiểu, TP.Đà Nẵng",
                                BreakTime = 2.5,
                                Coordinate = "16.051571, 108.214897",
                                ProvinceID = 4},
                };
            var invoices = new List<Invoice>
                {
                    new Invoice{InvoiceID = 1,
                                RequestID = 1,
                                Volume = 1,
                                Weight = 2,
                                Price = 5.6},
                    new Invoice{InvoiceID = 2,
                                RequestID = 2,
                                Volume = 1.5,
                                Weight = 2,
                                Price = 5.6},
                };

            //setup system under test
            var sut = new AssigningService(_route.Object, _station.Object, _invoice.Object, _trip.Object);


            _route.Setup(f => f.GetAllRoutes()).Returns(new EnumerableQuery<Route>(routes));
            _station.Setup(f => f.GetAllStations()).Returns(stations);
            _invoice.Setup(f => f.GetAllInvoices()).Returns(new EnumerableQuery<Invoice>(invoices));

            //test it
            var result = sut.Assigning(requests, trips, date);

            Assert.AreEqual(requests.Count, result.Count);
        }
    }
}
