using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeMachine;

namespace ScheduleUnitTests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void Different_times_should_not_be_equal()
        {
            Time timeA = new Time(8, 0);
            Time timeB = new Time(20, 0);

            Assert.AreNotEqual(timeA, timeB);
            Assert.IsTrue(timeA != timeB);
        }

        [TestMethod]
        public void Identical_times_should_be_equal()
        {
            Time timeA = new Time(11, 0);
            Time timeB = new Time(11, 0);

            Assert.AreEqual(timeA, timeB);
            Assert.IsTrue(timeA == timeB);
        }

        [TestMethod]
        public void Can_perform_double_equals_on_null_values()
        {
            Time timeA = null;
            Time timeB = null;

            Assert.IsTrue(timeA == timeB);
            Assert.IsFalse(timeA == new Time(11, 0));
            Assert.IsFalse(new Time(11, 0) == timeB);
        }

        [TestMethod]
        public void Can_perform_not_equals_on_null_values()
        {
            Time timeA = null;
            Time timeB = null;

            Assert.IsFalse(timeA != timeB);
            Assert.IsTrue(new Time(11, 0) != timeB);
            Assert.IsTrue(timeA != new Time(11, 0));
        }
    }
}
