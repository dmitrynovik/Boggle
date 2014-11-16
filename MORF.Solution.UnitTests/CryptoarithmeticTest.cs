using System;
using NUnit.Framework;

namespace MORF.Solution.UnitTests
{
    [TestFixture]
    public class CryptoarithmeticTest
    {
        [Test]
        public void Send_More_Money_Returns_10652()
        {
            var input = @"SEND
+MORE
------
MONEY";

            var output = "9567\n+1085\n" + CryptoarithmeticProblem.AnswerPad + "\n10652";
            
            var crypto = new CryptoarithmeticProblem(input);
            Assert.AreEqual(output, crypto.Solution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void When_Input_IsNull_ArgumentException_Is_Thrown()
        {
            var crypto = new CryptoarithmeticProblem(null);
            Assert.AreEqual(null, crypto.Solution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void When_Input_IsEmpty_ArgumentException_Is_Thrown()
        {
            var crypto = new CryptoarithmeticProblem("");
            Assert.AreEqual("", crypto.Solution);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void When_Input_Is_TooShort_ArgumentException_Is_Thrown()
        {
            var input = @"SEND
------
MONEY";

            var crypto = new CryptoarithmeticProblem(input);
            Assert.AreEqual("", crypto.Solution);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void When_Not_Addition_NotImplementedException_Is_Thrown()
        {
            var input = @"SEND
/More
------
MONEY";

            var crypto = new CryptoarithmeticProblem(input);
            Assert.AreEqual("", crypto.Solution);
        }
    }
}
