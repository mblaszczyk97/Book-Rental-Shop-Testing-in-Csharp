using System;
using System.Text.RegularExpressions;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class ClientTest
    {
        Client client;

        [TestInitialize]
        public void SetUp()
        {
            client = new Client("Zbigniew", "Kolonko", "82070111111", 67, Gender.Male);

        }

        [TestMethod]
        public void FullNameTest()
        {
            var tested = "Zbigniew Kolonko";
            var actual = client.FullName;
            Assert.AreEqual(tested, actual);
        }

        [TestMethod]
        public void NameTest()
        {
            var tested = "Zbigniew";
            var substring = client.FirstName;
            StringAssert.Contains(tested, substring);
        }

        [TestMethod]
        public void NameTestFirst()
        {
            var tested = "Zbigniew";
            var substring = client.FirstName;
            StringAssert.StartsWith(tested, "Z");
        }

        [TestMethod]
        public void FamilyNameTestFull()
        {
            var tested = "Kolonko";
            var substring = client.LastName;
            StringAssert.StartsWith(tested, substring);
        }

        [TestMethod]
        public void FamilyNameTestFirst()
        {
            var tested = "Kolonko";
            var substring = client.LastName;
            StringAssert.StartsWith(tested, "K");
        }

        [TestMethod]
        public void IsValidPesel()
        {
            var value = client.PESEL;
            var tested = new Regex("^[0-9]{11}$");
            StringAssert.Matches(value, tested);
        }

        [TestMethod]
        public void AgeGood()
        {
            var Expected = 67;
            var tested = client.Age;
            Assert.AreEqual(Expected, tested);
        }
    }
}
