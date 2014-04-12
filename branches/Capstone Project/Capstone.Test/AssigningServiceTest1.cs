using System;
using System.Collections.Generic;
using System.Linq;
using Captone.Models;
using Captone.Repositories;
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
        private readonly Mock<GenericRepository<Route>> _route = new Mock<GenericRepository<Route>>();
        private readonly Mock<GenericRepository<Station>> _station = new Mock<GenericRepository<Station>>();
        private readonly Mock<GenericRepository<Invoice>> _invoice = new Mock<GenericRepository<Invoice>>();
        private readonly Mock<GenericRepository<Trip>> _trip = new Mock<GenericRepository<Trip>>();
        private readonly Mock<GenericRepository<Request>> _request = new Mock<GenericRepository<Request>>();
        private readonly Mock<GenericRepository<Assigning>> _assigning = new Mock<GenericRepository<Assigning>>();
        private readonly Mock<GenericRepository<Stage>> _stage = new Mock<GenericRepository<Stage>>();
        private readonly Mock<GenericRepository<RouteStage>> _routeStage = new Mock<GenericRepository<RouteStage>>();
        private readonly Mock<GenericRepository<Schedule>> _schedules = new Mock<GenericRepository<Schedule>>();
        DateTime now = new DateTime(2014,04,11,12,30, 0);
        DateTime arrival = new DateTime(2014, 04, 13, 11, 30, 0);
        DateTime departure = new DateTime(2014, 04, 12, 08, 30, 0);

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void MainFlow_Test()
        {
            // Description of this test case:
            // There are routes and trips available between:
            // SG - Đà Lạt, SG - Vũng Tàu, Vũng Tàu - Đà Nẵng, SG - Đà Nẵng
            // Request1: SG - Vũng Tàu, Request2: SG - Đà Nẵng
            // Expected result is one request dedicated to one trip
            //setup input / testcase
            List<Request> requests = new List<Request>
                {
                    new Request{RequestID = 1,
                                FromLocation = 1,
                                ToLocation = 2,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 2,
                                FromLocation = 1,
                                ToLocation = 3,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                };
            var trips = new List<Trip>
                {
                    new Trip{TripID = 1, 
                             EstimateArrivalTime = arrival,
                             EstimateDepartureTime = departure,
                             AvailableVolume = 3.0,
                             ScheduleID = 1,
                             IsActive = true,
                    },
                    new Trip{TripID = 2, 
                             EstimateArrivalTime = arrival,
                             EstimateDepartureTime = departure,
                             AvailableVolume = 5.0,
                             ScheduleID = 1,
                             IsActive = true,
                    },
                    new Trip{TripID = 4, 
                             EstimateArrivalTime = arrival,
                             EstimateDepartureTime = departure,
                             AvailableVolume = 1.5,
                             ScheduleID = 2,
                             IsActive = true,
                    },
                    new Trip{TripID = 3, 
                             EstimateArrivalTime = arrival,
                             EstimateDepartureTime = departure,
                             AvailableVolume = 1.5,
                             ScheduleID = 2,
                             IsActive = true,
                    },
                };
            var schedules = new List<Schedule>
                {
                    new Schedule()
                        {
                            ScheduleID = 1,
                            RouteID = 1,
                        },
                    new Schedule()
                        {
                            ScheduleID = 2,
                            RouteID = 2,
                        },
                    new Schedule()
                        {
                            ScheduleID = 3,
                            RouteID = 3,
                        },
                    new Schedule()
                        {
                            ScheduleID = 4,
                            RouteID = 4,
                        },
                    new Schedule()
                        {
                            ScheduleID = 5,
                            RouteID = 5,
                        },
                        new Schedule()
                        {
                            ScheduleID = 6,
                            RouteID = 6,
                        },
                };
            //data set into mock object
            var routes = new List<Route>
                {
                    new Route{RouteID = 1,
                              RouteName = "Sài Gòn - Vũng Tàu",
                              },
                    new Route{RouteID = 2,
                              RouteName = "Sài Gòn - Đà Lạt",
                              },
                };
            var stages = new List<Stage>
                {
                    new Stage{StageID = 1,
                                StartPoint = 1,
                                EndPoint = 2,
                                },
                    new Stage{StageID = 2,
                                StartPoint = 1,
                                EndPoint = 3,
                                },
                    //new Stage{StageID = 1,
                    //            FromLocation = 1,
                    //            ToLocation = 1,
                    //            },
                };
            var routeStage = new List<RouteStage>
                {
                    new RouteStage{StageID = 1,
                                RouteID = 1,
                                },
                    new RouteStage{StageID = 2,
                                RouteID = 2,
                                },
                };
            var stations = new List<Station>
                {
                    new Station{StationID = 1,
                                StationName = "SG.Lê Hồng Phong",
                                StationLocation = "233 Lê Hồng Phong, F4, Q.5, TP Hồ Chí Minh",
                                Coordinate = "10.7584848, 106.6777557",
                                },
                    new Station{StationID = 2,
                                StationName = "Tạ Uyên - VT",
                                StationLocation = "03 Tạ Uyên, Vũng Tàu",
                                Coordinate = "10.3491804, 107.0855695",
                                },
                    new Station{StationID = 3,
                                StationName = "Bến xe liên tỉnh Đà Lạt",
                                StationLocation = "01 Tô Hiến Thành, F3, Đà Lạt",
                                Coordinate = "11.9264949, 108.4460601",
                                },
                    new Station{StationID = 4,
                                StationName = "Bến Xe Trung Tâm Đà Nẵng",
                                StationLocation = "201 Tôn Đức Thắng, Phường Hòa Minh, Liên Chiểu, TP.Đà Nẵng",
                                Coordinate = "16.051571, 108.214897",
                                },
                };
            var invoices = new List<Invoice>
                {
                    new Invoice{InvoiceID = 1,
                                RequestID = 1,
                                Volume = 4,
                                Weight = 2,
                                Price = 5.6},
                    new Invoice{InvoiceID = 2,
                                RequestID = 2,
                                Volume = 1.5,
                                Weight = 2,
                                Price = 5.6},
                };

            //setup system under test
            var sut = new AssigningService(_route.Object, _station.Object, _invoice.Object, _trip.Object,
                                           _request.Object, _assigning.Object, _stage.Object, 
                                           _routeStage.Object, _schedules.Object);

            _route.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Route>(routes));
            _station.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Station>(stations));
            _invoice.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Invoice>(invoices));
            _stage.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Stage>(stages));
            _trip.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Trip>(trips));
            _routeStage.Setup(f => f.GetAll()).Returns(new EnumerableQuery<RouteStage>(routeStage));
            _schedules.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Schedule>(schedules));
            //test it
            var result = sut.Assigning(requests);

            Assert.AreEqual(requests.Count, result.Count);
        }
    }
}
