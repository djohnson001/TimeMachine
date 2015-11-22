using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeMachine;

namespace TimeMachineTests
{
    [TestClass]
    public class TimeParserTests
    {
        [TestMethod]
        public void Should_be_able_to_filter_out_non_digits()
        {
            string result = new TimeParser().FilterOutNonDigits("  234sd4 5 gfw");
            Assert.AreEqual("23445", result);
        }

        [TestMethod]
        public void Can_parse_single_digit_am_time()
        {
            Time result = new TimeParser().Parse("5");
            Assert.AreEqual(new Time(5, 0), result);
        }

        [TestMethod]
        public void Can_parse_single_letter_meridium_without_space()
        {
            Time result = new TimeParser().Parse("5p");
            Assert.AreEqual(new Time(17, 0), result);
        }

        [TestMethod]
        public void Can_parse_multi_digit_am_time()
        {
            Time result = new TimeParser().Parse("535");
            Assert.AreEqual(new Time(5, 35), result);
        }

        [TestMethod]
        public void Can_parse_well_formed_am_time()
        {
            Time result = new TimeParser().Parse("5:35 AM");
            Assert.AreEqual(new Time(5, 35), result);
        }

        [TestMethod]
        public void Can_parse_well_formed_pm_time()
        {
            Time result = new TimeParser().Parse("5:35 PM");
            Assert.AreEqual(new Time(17, 35), result);
        }

        [TestMethod]
        public void Can_parse_four_digits_together()
        {
            Time result = new TimeParser().Parse("2351");
            Assert.AreEqual(new Time(23, 51), result);
        }

        [TestMethod]
        public void Can_parse_four_digits_with_meridium()
        {
            Time result = new TimeParser().Parse("1045p");
            Assert.AreEqual(new Time(22, 45), result);
        }

        [TestMethod]
        public void Can_handle_invalid_time_too_large()
        {
            Time result = new TimeParser().Parse("25:99");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_parse_12()
        {
            Time result = new TimeParser().Parse("12");
            Assert.AreEqual(result, new Time(0, 0));
        }

        [TestMethod]
        public void Can_parse_12_00_am()
        {
            Time result = new TimeParser().Parse("12:00 AM");
            Assert.AreEqual(result, new Time(0, 0));
        }

        [TestMethod]
        public void Can_parse_1230p()
        {
            Time result = new TimeParser().Parse("1230p");
            Assert.AreEqual(result, new Time(12, 30));
        }

        [TestMethod]
        public void Properly_ignore_seconds()
        {
            Time result = new TimeParser().Parse("22:27:51");
            Assert.AreEqual(new Time(22, 27), result);
        }
    }
}
