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
    public partial class AssigningServiceTest1
    {
        [TestMethod]
        public void MainFlow_Test3()
        {
            /*Mô tả test case:
			Có 1 request đi từ Vũng Tàu đến Đà Lạt
			Có 1 xe chạy theo tuyến: Sài Gòn - Vũng Tàu - Đà Lạt - Đà Nẵng
			Dự kiến là sẽ gửi được request này theo tuyến đã nêu			
			*/
            List<Request> requests = new List<Request>
                {
                    new Request{RequestID = 1,
                                FromLocation = 2,
                                ToLocation = 3,
                                DateRequest = now,
                                DeliveryStatusID = 2},
                };
            var trips = new List<Trip>
                {
                    new Trip{TripID = 1,
                             EstimateArrivalTime = datePost,
                             EstimateDepartureTime = datePost,
                             EstimateVolume = 5.0,
                             AvailableVolume = 1.0,
                             Date = now,
                             RouteID = 1,
                             IsActive = true,
                    },
                };

            //data set into mock object
            var routes = new List<Route>
                {
                    new Route{RouteID = 1,
                              RouteName = "Sài Gòn - Đà Nẵng",
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
                };
            var routeStage = new List<RouteStage>
                {
                    new RouteStage{StageID = 1,
                                RouteID = 1,
                                },
                    new RouteStage{StageID = 2,
                                RouteID = 1,
                                },
                    new RouteStage{StageID = 3,
                                RouteID = 1,
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
