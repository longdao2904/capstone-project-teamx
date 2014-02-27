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
    public class AssigningServiceTest
    {
        //setup mock object
        private readonly Mock<ICoachRepository> _coachRepository = new Mock<ICoachRepository>();
        private readonly Mock<ITripRepository> _tripRepository = new Mock<ITripRepository>();

        [TestMethod]
        public void MainFlow_Test()
        {
            //setup input / testcase
            var request = new Request {Description = "Test1"};

            //fake data set into mock object
            var coaches = new List<Coach>
                { new Coach {CoachID = 0, CoachTypeID = 1},
                new Coach {CoachID = 0, CoachTypeID = 1}};

            //setup system under test
            var sut = new AssigningService(_coachRepository.Object, _tripRepository.Object);
            _coachRepository.Setup(i => i.GetAllCoaches()).Returns(new EnumerableQuery<Coach>(coaches));

            //test it
            var result = sut.ProcessingRequest(request);

            Assert.AreEqual(2, result.Count());
        }
    }
}
