using NUnit.Framework;

namespace MORF.Solution.UnitTests
{
    [TestFixture]
    public class TriangleTest
    {
        [Test]
        [ExpectedException(typeof(InvalidTriangleException))]
        public void When_Edge_Non_Positive_Throws_Exception()
        {
            Triangle.CalculateArea(0, 1, 2);
        }

        [Test]
        [ExpectedException(typeof(InvalidTriangleException))]
        public void When_Triangle_Inequality_Broken_Throws_Exception()
        {
            Triangle.CalculateArea(1, 1, 10);
        }

        [Test]
        public void When_Triangle_IsValid_TheArea_Is_Calculated_Correctly()
        {
            Assert.AreEqual(6, Triangle.CalculateArea(3, 4, 5));
        }
    }
}
