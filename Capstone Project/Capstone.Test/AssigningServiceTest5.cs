﻿using System;
using System.Collections.Generic;
using System.Linq;
using Captone.Models;
using Captone.Repositories.Interfaces;
using Captone.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capstone.Test
{
    public partial class AssigningServiceTest1
    {
        [TestMethod]
        public void MainFlow_Test5()
        {
			/*
			Có tổng cộng 5 request, dự kiến là sẽ gán được hết, 
            có một số request được gán qua đến 4 trips
			*/
            List<Request> requests = new List<Request>
                {
                    new Request{RequestID = 1,
                                FromLocation = 1,
                                ToLocation = 6,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 2,
                                FromLocation = 1,
                                ToLocation = 5,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 3,
                                FromLocation = 3,
                                ToLocation = 5,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 4,
                                FromLocation = 4,
                                ToLocation = 5,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                    new Request{RequestID = 5,
                                FromLocation = 3,
                                ToLocation = 7,
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
                             RouteID = 1,
                             IsActive = true,
                    },
                    new Trip{TripID = 2,
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now.AddHours(3),
                             RouteID = 2,
                             IsActive = true,
                    },
                    new Trip{TripID = 4,
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now.AddHours(9),
                             RouteID = 4,
                             IsActive = true,
                    },
                    new Trip{TripID = 6,
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 5.0,
                             Date = now.AddHours(15),
                             RouteID = 6,
                             IsActive = true,
                    },
                };

            //data set into mock object
            var routes = new List<Route>
                {
                    new Route{RouteID = 1,
                              RouteName = "Cần Thơ - Gò Công",
                              },
                    new Route{RouteID = 2,
                              RouteName = "Gò Công - Sài Gòn",
                              },
                    new Route{RouteID = 4,
                              RouteName = "Sài Gòn - Vũng Tàu",
                              },
                    new Route{RouteID = 6,
                              RouteName = "Biên Hòa - Đà Nẵng",
                              },
                };
            var stages = new List<Stage>
                {
                    new Stage{StageID = 1,
                                StartPoint = 1,
                                EndPoint = 2,
                                },
                    new Stage{StageID = 2,
                                StartPoint = 2,
                                EndPoint = 3,
                                },
                    new Stage{StageID = 3,
                                StartPoint = 3,
                                EndPoint = 4,
                                },
                    new Stage{StageID = 4,
                                StartPoint = 4,
                                EndPoint = 5,
                                },
                    new Stage{StageID = 5,
                                StartPoint = 5,
                                EndPoint = 6,
                                },
                    new Stage{StageID = 6,
                                StartPoint = 6,
                                EndPoint = 7,
                                },
                };
            var routeStage = new List<RouteStage>
                {
                    new RouteStage
                        {
                            StageID = 1,
                            RouteID = 1,
                            StageIndex = 1,
                        },
                    new RouteStage
                        {
                            StageID = 2,
                            RouteID = 2,
                            StageIndex = 1,
                        },
                    new RouteStage
                        {
                            StageID = 3,
                            RouteID = 4,
                            StageIndex = 1,
                        },
                    new RouteStage
                        {
                            StageID = 4,
                            RouteID = 4,
                            StageIndex = 2,
                        },
                    new RouteStage
                        {
                            StageID = 4,
                            RouteID = 6,
                            StageIndex = 1,
                        },
                    new RouteStage
                        {
                            StageID = 5,
                            RouteID = 6,
                            StageIndex = 2,
                        },
                    new RouteStage
                        {
                            StageID = 6,
                            RouteID = 6,
                            StageIndex = 3,
                        },
                };
            var stations = new List<Station>
                {
                    new Station{StationID = 1,
                                StationName = "Bến xe Hùng Vương - Cần Thơ",
                                StationLocation = "01 Hùng Vương, Thới Bình, Ninh Kiều, TP.Cần Thơ ",
                                Coordinate = "10.0449994, 105.7799216",
                                },
                    new Station{StationID = 2,
                                StationName = "Bến xe Gò Công",
                                StationLocation = "Đồng Khởi - Phường 4 - Gò Công - Tiền Giang",
                                Coordinate = "10.360669, 106.6628406",
                                },
                    new Station{StationID = 3,
                                StationName = "SG.Lê Hồng Phong",
                                StationLocation = "233 Lê Hồng Phong, F4, Q.5, TP Hồ Chí Minh",
                                Coordinate = "10.7584848, 106.6777557",
                                },
                    new Station{StationID = 4,
                                StationName = "Suối Linh - Biên Hòa",
                                StationLocation = "D9 - Tổ 2 - KP.4 - P. Long Bình - Đồng Nai",
                                Coordinate = "10.9413981, 106.9004472",
                                },
                    new Station{StationID = 5,
                                StationName = "Tạ Uyên - VT",
                                StationLocation = "03 Tạ Uyên, Vũng Tàu",
                                Coordinate = "10.3491804, 107.0855695",
                                },
                    new Station{StationID = 6,
                                StationName = "Bến xe liên tỉnh Đà Lạt",
                                StationLocation = "01 Tô Hiến Thành, F3, Đà Lạt",
                                Coordinate = "11.9264949, 108.4460601",
                                },
                    new Station{StationID = 7,
                                StationName = "Bến Xe Trung Tâm Đà Nẵng",
                                StationLocation = "201 Tôn Đức Thắng, Phường Hòa Minh, Liên Chiểu, TP.Đà Nẵng",
                                Coordinate = "16.051571, 108.214897",
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
