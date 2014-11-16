using NUnit.Framework;

namespace MORF.Solution.UnitTests
{
    [TestFixture]
    public class StringExtenionsTest
    {
        [Test]
        public void When_Input_Null_Returns_True()
        {
            string s = null;
            Assert.IsTrue(s.IsNullOrEmpty());
        }

        [Test]
        public void When_Input_Empty_Returns_True()
        {
            Assert.IsTrue("".IsNullOrEmpty());
        }

        [Test]
        public void When_Input_IsSpace_Returns_False()
        {
            Assert.IsFalse(" ".IsNullOrEmpty());
        }

        [Test]
        public void When_Input_Non_Empty_Returns_False()
        {
            Assert.IsFalse("x".IsNullOrEmpty());
        }
    }
}
