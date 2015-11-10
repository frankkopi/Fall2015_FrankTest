using System;
using Fall2015.TDD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fall2015.Tests.Controllers
{
    [TestClass]
    public class UnitTest3
    {
        //Arrange
        OurTimeSpan our1 = new OurTimeSpan
        {
            FromTime = new DateTime(2015, 1, 1, 12, 0, 0),
            ToTime = new DateTime(2015, 1, 1, 13, 30, 0)
        };

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //"Some message")]
        public void TestMethod1()
        {
            OurTimeSpan our2 = new OurTimeSpan {
                FromTime = new DateTime(2015, 1, 1, 10, 0, 0),
                ToTime = new DateTime(2015,1,1, 11,0,0)
            };

            Boolean result = our1.Overlap(our2);

            Assert.IsFalse(result);

        }

        
        [TestMethod]
        public void TestMethod2()
        {
            OurTimeSpan our2 = new OurTimeSpan
            {
                FromTime = new DateTime(2015, 1, 1, 12, 0, 0),
                ToTime = new DateTime(2015, 1, 1, 13, 30, 0)
            };

            Boolean result = our1.Overlap(our2);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestMethod3()
        {
            OurTimeSpan our2 = new OurTimeSpan
            {
                FromTime = new DateTime(2015, 1, 1, 10, 0, 0),
                ToTime = new DateTime(2015, 1, 1, 12, 15, 0)
            };

            Boolean result = our1.Overlap(our2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            OurTimeSpan our2 = new OurTimeSpan
            {
                FromTime = new DateTime(2015, 1, 1, 12, 15, 0),
                ToTime = new DateTime(2015, 1, 1, 13, 15, 0)
            };

            Boolean result = our1.Overlap(our2);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestMethod5()
        {
            OurTimeSpan our2 = new OurTimeSpan
            {
                FromTime = new DateTime(2015, 1, 1, 10, 0, 0),
                ToTime = new DateTime(2015, 1, 1, 14, 0, 0)
            };

            Boolean result = our1.Overlap(our2);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestMethod6()
        {
            OurTimeSpan our2 = new OurTimeSpan
            {
                FromTime = new DateTime(2015, 1, 1, 13, 0, 0),
                ToTime = new DateTime(2015, 1, 1, 14, 0, 0)
            };

            Boolean result = our1.Overlap(our2);

            Assert.IsTrue(result);
        }
    }
}
