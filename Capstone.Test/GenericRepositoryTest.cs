using System;
using System.Linq;
using Captone.Models;
using Captone.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Test
{
    [TestClass]
    public class GenericRepositoryTest
    {
        private readonly GenericRepository<Route> _route = new GenericRepository<Route>(new iDeliverEntities());
        
        [TestMethod]
        public void TestGetAll()
        {
            var output = _route.GetAll();
            Assert.IsTrue(output.Any());
        }
    }
}
