using System;
using Library;
using Microsoft.QualityTools.Testing.Fakes;
using Library.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibraryTest
{
    [TestClass]
    public class LibraryTest
    {
        public TestContext TestContext { get; set; }
        Library.LibraryShop library;
        Client client1;
        Borrow borrow1;

        [TestInitialize]
        public void SetUp()
        {
            library = new Library.LibraryShop();
            client1 = new Client("Zbigniew", "Kolonko", "82070111111", 67, Gender.Male);
            library.AddClient(client1);
            borrow1 = new Borrow(client1, DateTime.Parse("2019-03-01 13:20"), "Podstawy C++", 5);
            library.AddBorrow(client1, borrow1);
            library.AddClient("Rak", "Rakowski", "11111111111", 89, Gender.ApacheHelicopter);
            library.AddClient("Jaś", "Nieboraś", "11111111112", 16, Gender.Male);
            library.AddClient("Janek", "Agent", "11111111113", 18, Gender.Male);
            library.AddClient("Kuba", "Ruczaj", "11111111114", 19, Gender.Male);
            library.AddBorrow((Client)library.Clients[0], DateTime.Parse("2019-03-20 13:20"), "Podstawy C++", 5);
            library.AddBorrow((Client)library.Clients[1], DateTime.Parse("2019-03-20 11:45"), "Dlaczego C# to najlepszy język", 20);
        }

        [TestMethod]
        public void AddBorrowAsObjectInvalid()
        {
            var tested = library.AddBorrow(client1, borrow1);
            Assert.IsFalse(tested);//jest już taki
        }

        [TestMethod]
        public void AddBorrowAsObject()
        {
            var tested = library.FindBorrow("2019-03-01");
            var expected = "82070111111";
            Assert.AreEqual(expected, tested.Client.PESEL);
        }

        [TestMethod]
        public void AddBorrowAsIsInvalid()
        {
            var tested = library.AddBorrow((Client)library.Clients[1], DateTime.Parse("2019-03-20 11:45"), "Dlaczego C# to najlepszy język", 20);
            Assert.IsFalse(tested);//jest już taki
        }

        [TestMethod]
        public void AddBorrowAsIs()
        {
            var tested = library.FindBorrow("2019-03-20 11:45");
            var expected = "11111111111";
            Assert.AreEqual(expected, tested.Client.PESEL);
        }

        [TestMethod]
        public void AddClientAsObjectInvalid()
        {
            var tested = library.AddClient(client1);
            Assert.IsFalse(tested);//jest już taki
        }

        [TestMethod]
        public void AddClientAsObject()
        {
            var tested = library.FindClient("Zbigniew");
            var expected = "82070111111";
            Assert.AreEqual(expected, tested.PESEL);
        }

        [TestMethod]
        public void AddClientAsIsInvalid()
        {
            var tested = library.AddClient("Rak", "Rakowski", "11111111111", 89, Gender.ApacheHelicopter);
            Assert.IsFalse(tested);//jest już taki
        }

        [TestMethod]
        public void AddClientAsIs()
        {
            var tested = library.FindClient("Rak");
            var expected = "11111111111";
            Assert.AreEqual(expected, tested.PESEL);
        }

        [TestMethod]
        public void DeleteClientValid()
        {
            var tested =  library.DeleteClient(client1);
            Assert.IsTrue(tested);
        }

        [TestMethod]
        public void DeleteClientDidSth()
        {
            library.DeleteClient(client1);
            var expected = 4;
            var tested = library.NumberOfClients();
            Assert.AreEqual(expected, tested);
        }

        [TestMethod]
        public void ShimBorrowPayment()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => DateTime.Parse("2019-03-21 11:45");
                var tested = library.BorrowPayment(borrow1);
                var expected = 3;
                Assert.AreEqual(expected, tested);
            }
        }

        [TestMethod]
        public void DisplayStatistics()
        {
            var tested = library.DisplayStatistics();
            var expected = "Liczba klientów w naszej bazie = 5 || Liczba wypożyczonych książek = 3";
            StringAssert.Contains(expected, tested);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",@"C:\Users\mikib\source\repos\Library\LibraryTest\go.csv","go#csv",DataAccessMethod.Sequential)]
        public void DataDrivenTestFromCSVFile()
        {
            library.AddClient(     TestContext.DataRow["FirstName"].ToString(),
                                   TestContext.DataRow["LastName"].ToString(),
                                   TestContext.DataRow["PESEL"].ToString(),
                                   Int32.Parse(TestContext.DataRow["Age"].ToString()),
                                   (Gender)Enum.Parse(typeof(Gender), TestContext.DataRow["Gender"].ToString()));
            var tested = library.NumberOfClients();
            Assert.AreEqual(6, tested);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"C:\Users\mikib\source\repos\Library\LibraryTest\go1.csv", "go1#csv", DataAccessMethod.Sequential)]
        public void DataDrivenTestFromCSVFileDays()
        {
            Assert.AreEqual(TestContext.DataRow["Result"],library.DaysBetween(Convert.ToDateTime(TestContext.DataRow["Date1"].ToString())));
        }

        [TestMethod]
        public void NumberOfUsers()
        {
           var tested = library.NumberOfClients();
           var expected = 5;
           Assert.AreEqual(expected, tested);
        }

        [TestMethod]
        public void NumberOfBorrows()
        {
            var tested = library.NumberOfBorrows();
            var expected = 3;
            Assert.AreEqual(expected, tested);
        }

        [TestMethod]
        public void CollectionFindClients()
        {
            var tested = library.FindClients("Rak", "Jaś");
            var expected = new List<IClient>()
            {
                new Client("Rak", "Rakowski", "11111111111", 89, Gender.ApacheHelicopter),
                new Client("Jaś", "Nieboraś", "11111111112", 16, Gender.Male)
        };
            CollectionAssert.AreEqual(expected, tested);
        }

        [TestMethod]
        public void CollectionFindBorrows()
        {
            var tested = library.FindBorrows("2019-03-20");
            var expected = new List<IBorrow>()
            {
                new Borrow((Client)library.Clients[0], DateTime.Parse("2019-03-20 13:20"), "Podstawy C++", 5M),
                new Borrow((Client)library.Clients[1], DateTime.Parse("2019-03-20 11:45"), "Dlaczego C# to najlepszy język", 20M)
            };
            CollectionAssert.AreEqual(expected, tested);
        }


        [TestMethod]
        public void ShimGetDateTimeFakeTest()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2000, 1, 1);
                var foo = LibraryShop.GetDateTime();
                var expected = new DateTime(2000, 1, 1);
                Assert.AreEqual(expected, foo, "Data nieprawidłowa");
            }
        }

        [TestMethod]
        public void ShimReadFileFakeTest()
        {
            using (Microsoft.QualityTools.Testing.Fakes.ShimsContext.Create())
            {
                const string path = "cośtam123";
                const string expected = "cośtamwpliku";
                System.IO.Fakes.ShimFileStream.ConstructorStringFileMode =
                    (@this, p, f) => {
                        var shim = new System.IO.Fakes.ShimFileStream(@this);
                    };
                System.IO.Fakes.ShimStreamReader.ConstructorStream =
                    (@this, s) => {
                        var shim = new System.IO.Fakes.ShimStreamReader(@this)
                        {
                            ReadToEnd = () => expected
                        };
                    };
                var actual = LibraryShop.Read(path);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SimpleStubTestingNotEssential()
        {
            IStockFeed stockFeed =
                 new Library.Fakes.StubIStockFeed()
                 {
                    GetSharePriceString = (company) => { return 1234; }
                 };
            var componentUnderTest = new StockAnalyzer(stockFeed);
            int actualValue = componentUnderTest.GetContosoPrice();
            Assert.AreEqual(1234, actualValue);
        }

}
}
