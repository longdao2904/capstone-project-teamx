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

        DateTime now = new DateTime(2014,04,05,10,30,00);
        TimeSpan datePost = new TimeSpan(05,0,0);

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
                                FromLocation = 6,
                                ToLocation = 8,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 2,
                                FromLocation = 6,
                                ToLocation = 9,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 3,
                                FromLocation = 4,
                                ToLocation = 8,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 4,
                                FromLocation = 2,
                                ToLocation = 3,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 5,
                                FromLocation = 10,
                                ToLocation = 3,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 6,
                                FromLocation = 10,
                                ToLocation = 8,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                };
            var trips = new List<Trip>
                {
                    new Trip{TripID = 1,
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 7,
                             IsActive = true,
                    },
                    new Trip{TripID = 2, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 14,
                             IsActive = true,
                    },
                    new Trip{TripID = 3, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 23,
                             IsActive = true,
                    },
                    new Trip{TripID = 4, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 24,
                             IsActive = true,
                    },
                    new Trip{TripID = 5, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 25,
                             IsActive = true,
                    },
                    new Trip{TripID = 6, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 26,
                             IsActive = true,
                    },
                    new Trip{TripID = 7, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 28,
                             IsActive = true,
                    },
                    new Trip{TripID = 8, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 29,
                             IsActive = true,
                    },
                    new Trip{TripID = 9, 
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now,
                             RouteID = 30,
                             IsActive = true,
                    },
                };

            //data set into mock object
            var routes = new List<Route>
                {
                    new Route{RouteID = 7,
                              RouteName = "Sài Gòn - Đà Nẵng",
                              },
                    new Route{RouteID = 14,
                              RouteName = "Cần Thơ - Sài Gòn",
                              },
                    new Route{RouteID = 23,
                              RouteName = "Cần Thơ - Đà Lạt",
                              },
                    new Route{RouteID = 24,
                              RouteName = "Cà Mau - Sài Gòn",
                              },
                    new Route{RouteID = 25,
                              RouteName = "Cà Mau - Đà Lạt",
                              },
                    new Route{RouteID = 26,
                              RouteName = "Đà Lạt - Quy Nhơn",
                              },
                    new Route{RouteID = 28,
                              RouteName = "Sài Gòn - Đà Lạt",
                              },
                    new Route{RouteID = 29,
                              RouteName = "Quy Nhơn - Đà Nẵng",
                              },
                    new Route{RouteID = 30,
                              RouteName = "Nha Trang - Đà Nẵng",
                              },
                };
            var stages = new List<Stage>
                {
                    new Stage{StageID = 6,
                                StartPoint = 6,
                                EndPoint = 9,
                                },
                    new Stage{StageID = 7,
                                StartPoint = 6,
                                EndPoint = 2,
                                },
                    new Stage{StageID = 8,
                                StartPoint = 2,
                                EndPoint = 3,
                                },
                    new Stage{StageID = 14,
                                StartPoint = 7,
                                EndPoint = 6,
                                },
                    new Stage{StageID = 16,
                                StartPoint = 10,
                                EndPoint = 7,
                                },
                    new Stage{StageID = 24,
                                StartPoint = 6,
                                EndPoint = 3,
                                },
                    new Stage{StageID = 25,
                                StartPoint = 3,
                                EndPoint = 9,
                                },
                    new Stage{StageID = 26,
                                StartPoint = 9,
                                EndPoint = 8,
                                },
                    new Stage{StageID = 28,
                                StartPoint = 7,
                                EndPoint = 5,
                                },
                    new Stage{StageID = 30,
                                StartPoint = 4,
                                EndPoint = 8,
                                },
                };
            #region routeStage
            var routeStage = new List<RouteStage>
                {
                    new RouteStage{StageID = 6,
                                RouteID = 7,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 26,
                                RouteID = 7,
                                StageIndex = 2,
                                },
                    new RouteStage{StageID = 28,
                                RouteID = 14,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 14,
                                RouteID = 23,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 24,
                                RouteID = 23,
                                StageIndex = 2,
                                },
                    new RouteStage{StageID = 14,
                                RouteID = 24,
                                StageIndex = 2,
                                },
                    new RouteStage{StageID = 16,
                                RouteID = 24,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 14,
                                RouteID = 25,
                                StageIndex = 2,
                                },
                    new RouteStage{StageID = 16,
                                RouteID = 25,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 24,
                                RouteID = 25,
                                StageIndex = 3,
                                },
                    new RouteStage{StageID = 25,
                                RouteID = 26,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 7,
                                RouteID = 28,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 8,
                                RouteID = 28,
                                StageIndex = 2,
                                },
                    new RouteStage{StageID = 25,
                                RouteID = 28,
                                StageIndex = 3,
                                },
                    new RouteStage{StageID = 26,
                                RouteID = 29,
                                StageIndex = 1,
                                },
                    new RouteStage{StageID = 30,
                                RouteID = 30,
                                StageIndex = 1,
                                },
                };
            #endregion 
            var stations = new List<Station>
                {
                    new Station{StationID = 1,
                                StationName = "Lê Hồng Phong - Sài Gòn",
                                Coordinate = "10.7584848, 106.6777557",
                                },
                    new Station{StationID = 2,
                                StationName = "Tạ Uyên - VT",
                                Coordinate = "10.3491804, 107.0855695",
                                },
                    new Station{StationID = 3,
                                StationName = "Bến xe liên tỉnh Đà Lạt",
                                Coordinate = "11.9264949, 108.4460601",
                                },
                    new Station{StationID = 4,
                                StationName = "Bến xe Phía Nam Nha Trang",
                                Coordinate = "11.9264949, 108.4460601",
                                },
                    new Station{StationID = 5,
                                StationName = "Bến xe Miền Tây - Sài Gòn",
                                Coordinate = "10.740993, 106.617980",
                                },
                    new Station{StationID = 6,
                                StationName = "Bến xe Miền Đông - Sài Gòn",
                                Coordinate = "10.815213, 106.710674",
                                },
                    new Station{StationID = 7,
                                StationName = "Bến xe Hùng Vương - Cần Thơ",
                                Coordinate = "10.0449994, 105.7799216",
                                },
                    new Station{StationID = 8,
                                StationName = "Bến Xe Trung Tâm Đà Nẵng",
                                Coordinate = "16.051571, 108.214897",
                                },
                    new Station{StationID = 9,
                                StationName = "Bến xe Quy Nhơn",
                                Coordinate = "13.753419, 109.2095464",
                                },
                    new Station{StationID = 10,
                                StationName = "Bến xe Cà Mau",
                                Coordinate = "9.1760935, 105.1706709",
                                },
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
                                Volume = 1,
                                Weight = 2,
                                Price = 5.6},
                    new Invoice{InvoiceID = 3,
                                RequestID = 3,
                                Volume = 1,
                                Weight = 2,
                                Price = 5.6},
                    new Invoice{InvoiceID = 4,
                                RequestID = 4,
                                Volume = 1,
                                Weight = 2,
                                Price = 5.6},
                    new Invoice{InvoiceID = 5,
                                RequestID = 5,
                                Volume = 1,
                                Weight = 2,
                                Price = 5.6},
                    new Invoice{InvoiceID = 6,
                                RequestID = 6,
                                Volume = 1,
                                Weight = 2,
                                Price = 5.6},
                };

            //setup system under test
            var sut = new AssigningService(_route.Object, _station.Object, _invoice.Object, _trip.Object,
                                           _request.Object, _assigning.Object, _stage.Object, _routeStage.Object);

            _route.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Route>(routes));
            _station.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Station>(stations));
            _invoice.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Invoice>(invoices));
            _stage.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Stage>(stages));
            _trip.Setup(f => f.GetAll()).Returns(new EnumerableQuery<Trip>(trips));
            _routeStage.Setup(f => f.GetAll()).Returns(new EnumerableQuery<RouteStage>(routeStage));

            //test it
            var result = sut.Assigning(requests);

            Assert.AreEqual(requests.Count, result.Count);
        }
    }
}
