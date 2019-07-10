using System;
using System.Text.RegularExpressions;
using Library;
using Library.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class BorrowingTest
    {
        Borrow borrow;

        [TestInitialize]
        public void SetUp()
        {
            //IClient client = new StubIClient();
            borrow = new Borrow(new StubIClient(), DateTime.Parse("2019-02-12 12:33"), "Władca Pierścieni - 2 miesiące", 5M);
        }

        [TestMethod]
        public void StubBorrowedIsAsClient()
        {
            var expected = new Client("Zbigniew", "Kolonko", "82070111111", 67, Gender.Male);
            //borrow.Client = client;
            var tested = borrow.Client;
            StringAssert.Equals(expected, tested);
        }

        [TestMethod]
        public void ShimCheckGoodYearOfBorrow()
        {
            using (ShimsContext.Create())
            {
                ShimBorrow.AllInstances.DateOfBorrowGet = (Borrow) => DateTime.Parse("2010-01-01 15:23");
                int expected = 2010;
                int tested = borrow.DateOfBorrow.Year;
                Assert.AreEqual(expected, tested);
            }
        }

        [TestMethod]
        public void ShimValidDescription()
        {
            using (ShimsContext.Create())
            {
                ShimBorrow.AllInstances.DescriptionGet = (Visit) => "coś tam pisze";
                var expected = borrow.Description;
                var teseted = "coś tam pisze";
                StringAssert.Contains(expected, teseted);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void StubThrowExceptionOnInvalidPricePLN()
        {
            var pricePLN = -5M;
            var borr = new Borrow(new StubIClient(), new DateTime(), "", pricePLN);
        }
    }
}
