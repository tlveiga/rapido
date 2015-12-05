using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rapido.Common;

namespace Tests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void PointTest()
        {
            Point p1 = new Point();
            Point p2 = new Point(1, 1);
            Point p3 = new Point(-1, 1);

            Assert.AreEqual(p1, new Point(), "Equality failed");
            Assert.AreNotEqual(p2, new Point(), "NonEquality failed");

            Assert.AreEqual(p1 + p2, new Point(1, 1), "Sum failed");
            Assert.AreEqual(p1 - p2, new Point(-1, -1), "Subtraction failed");
            Assert.AreEqual(p2 * p3, new Point(-1, 1), "Multiplication failed");

        }

        [TestMethod]
        public void VectorTest()
        {


            Assert.AreEqual(new Vector(new Point(), new Point(1, 1)), new Vector(new Point(), new Point(1, 1)), "Equality failed");
            Assert.AreNotEqual(new Vector(new Point(1, 1), new Point()), new Vector(new Point(), new Point(1, 1)), "NonEquality failed");
            Assert.AreNotEqual(new Vector(), new Vector(new Point(), new Point(1, 1)), "NonEquality failed");


            Point p34 = new Point(3, 4);
            Point p51 = new Point(5, -1);
            //(3, 4) + (5,-1) = (8, 3)
            Assert.AreEqual(new Vector(new Point(), p34) + new Vector(new Point(), p51), new Vector(new Point(), new Point(8, 3)), "Sum failed");
            //(3, 4) + (5,-1) = (-2, 5)
            Assert.AreEqual(new Vector(new Point(), p34) - new Vector(new Point(), p51), new Vector(new Point(), new Point(-2, 5)), "Subtraction failed");

        }
    }
}
