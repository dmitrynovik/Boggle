using NUnit.Framework;
using System;
using System.Linq;

namespace MORF.Solution.UnitTests
{
    [TestFixture]
    public class NumberExtensionsTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void When_Negative_Throws_ArgumentException()
        {
            int num = -1;
            num.GetAllPositiveDivisors().ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void When_Zero_Throws_ArgumentException()
        {
            int num = 0;
            num.GetAllPositiveDivisors().ToList();
        }

        [Test]
        public void When_Prime_Returns_Only_1_And_Self()
        {
            int num = 13;
            var result = num.GetAllPositiveDivisors().ToArray();
            Assert.Contains(1, result);
            Assert.Contains(13, result);
        }

        [Test]
        public void When_Even_And_Positive_Returns_Expected_Divisors()
        {
            int num = 6;
            var result = num.GetAllPositiveDivisors().ToArray();
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);
            Assert.Contains(6, result);
        }

        [Test]
        public void When_Odd_And_Positive_Returns_Expected_Divisors()
        {
            int num = 15;
            var result = num.GetAllPositiveDivisors().ToArray();
            Assert.Contains(1, result);
            Assert.Contains(3, result);
            Assert.Contains(5, result);
            Assert.Contains(15, result);
        }

        [Test]
        public void Contains_Unique_Divisors_Only()
        {
            int num = 15;
            var result = num.GetAllPositiveDivisors();
            Assert.AreEqual(result.Count(), result.Distinct().Count());
        }
    }
}
