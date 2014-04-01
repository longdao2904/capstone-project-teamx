using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Test
{
    public static class Serialize
    {
        public static void AreDeeplyEquals(this object value, object target)
        {
            var serializer = new JavaScriptSerializer();
            var ass1 = serializer.Serialize(value);
            var ass2 = serializer.Serialize(target);
            Assert.AreEqual(ass1, ass2);
        }
    }
}
