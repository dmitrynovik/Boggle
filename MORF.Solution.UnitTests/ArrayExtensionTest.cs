using System;
using System.Linq;
using NUnit.Framework;

namespace MORF.Solution.UnitTests
{
    [TestFixture]
    public class ArrayExtensionTest
    {
        [Test]
        public void When_Array_Has_Multiple_MostCommon_Elements_All_Are_Returned()
        {
            int[] input = new[] { 5, 5, 4, 4, 1 };
            Assert.IsTrue(input.GetMostCommonElements().OrderBy(a => a).SequenceEqual(new[] { 4, 5 }));
        }

        [Test]
        public void When_Array_Has_No_MostCommon_Elements_All_Elements_Are_Returned()
        {
            int[] input = new[] { 1, 2, 3 };
            Assert.IsTrue(input.GetMostCommonElements().OrderBy(a => a).SequenceEqual(new[] { 1, 2, 3 }));
        }

        [Test]
        public void When_Array_Has_Single_MostCommon_Element_Only_Single_RightItem_Is_Returned()
        {
            int[] input = new[] { 1, 1, 2, 3 };
            Assert.AreEqual(1, input.GetMostCommonElements().Single());
        }

        [Test]
        public void When_Array_IsEmpty_EmptySequence_IsReturned()
        {
            int[] input = new int[0];
            Assert.AreEqual(0, input.GetMostCommonElements().Length);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_Array_IsNull_ArgumentNullException_IsThrown()
        {
            int[] input = null;
            input.GetMostCommonElements();
        }
    }
}
