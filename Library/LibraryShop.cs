using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library
{

        
        public class LibraryShop
        {

            public List<IClient> Clients { get; private set; }  
            public List<IBorrow> Borrows { get; private set; }

            public LibraryShop()
            {
                Clients = new List<IClient>();
                Borrows = new List<IBorrow>();
            }

            private LibraryShop(string path)
            {
            }

            public bool AddClient(String firstName, String lastName, String pesel, Int32 age, Gender sex)
            {
                var client = new Client(firstName, lastName, pesel, age, sex);
                if (!Clients.Contains(client))
                {
                    //.Remove(client);
                    Clients.Add(client);
                    return true;
                }
                else
                    return false;
            }

            public bool AddClient(Client client)
            {
                var clientAdded = client;
                if (!Clients.Contains(clientAdded))
                {
                    //.Remove(client);
                    Clients.Add(clientAdded);
                    return true;
                }
                else
                    return false;
            }

            public bool DeleteClient(Client client)
            {
                if (Clients.Contains(client))
                {
                    DeleteAllBorrows(client);
                    Clients.Remove(client);
                    return true;
                }
                else
                    return false;
            }
            
            public List<IClient> FindClients(params String[] list)
            {
                var items = new List<IClient>();
                foreach (String input in list)
                {
                    items.AddRange(Clients.FindAll(
                            x => x.FirstName.ToLower().Contains(input.ToLower())
                         || x.LastName.ToLower().Contains(input.ToLower())
                         || x.PESEL.Contains(input)));
                }
                var result = items.Distinct().ToList();
                return result;
            }

            public IClient FindClient(params String[] list)
            {
                Client item = null;
                foreach (String input in list)
                {
                    var itemNext = (Client)Clients.Find(
                               x => x.FirstName.ToLower().Contains(input.ToLower())
                            || x.LastName.ToLower().Contains(input.ToLower())
                            || x.PESEL.Contains(input));
                    if (item == null)
                    {
                      item = itemNext;
                    }
                    else
                    {
                        return null;
                    }
                }
                return item;
            }
            

            public bool AddBorrow(Client client, DateTime dateOfVisit, String description, Decimal price)
            {
                var borrowAdded = new Borrow(client, dateOfVisit, description, price);
                if (!Borrows.Contains(borrowAdded))
                {
                    Borrows.Add(borrowAdded);
                    client.Borrowed.Add(borrowAdded);
                    return true;
                }
                else
                    return false;
            }

            public bool AddBorrow(Client client, Borrow borrow)
            {
                var borrowAdded = borrow;
                if (!Borrows.Contains(borrowAdded))
                {
                    Borrows.Add(borrowAdded);
                    client.Borrowed.Add(borrowAdded);
                    return true;
                }
                else
                    return false;
            }

            //add test
            public bool DeleteBorrow(Borrow borrow, Client client)
            {
                if (Borrows.Contains(borrow))
                {
                    client.Borrowed.Remove(borrow);
                    Borrows.Remove(borrow);
                    return true;
                }
                else
                    return false;
            }

            //add test
            public void DeleteAllBorrows(Client client)
            {
                for(int i=0; i <= client.Borrowed.Count()-1; i++)
                {
                    Borrows.Remove(client.Borrowed.ElementAt(i));
                }
                client.Borrowed.Clear();
            }

            public List<IBorrow> FindBorrows(params String[] list)
            {
                var items = new List<IBorrow>();
                foreach (String input in list)
                {
                     items.AddRange(Borrows.FindAll(
                          x => x.Client.FirstName.ToLower().Contains(input.ToLower())
                            || x.Client.LastName.ToLower().Contains(input.ToLower())
                            || x.Client.PESEL.Contains(input)
                            || x.DateOfBorrow.Equals(DateTime.Parse(input))
                            || x.DateOfBorrow.Date == DateTime.Parse(input)));
                }
                var result = items.Distinct().ToList();
                return result;
            }


            public IBorrow FindBorrow(params String[] list)
            {
                Borrow item = null;
                foreach (String input in list)
                {
                    var itemNext = (Borrow)Borrows.Find(
                              x => x.DateOfBorrow.Equals(DateTime.Parse(input))
                                || x.DateOfBorrow.Date == DateTime.Parse(input));
                    if (item == null)
                    {
                        item = itemNext;
                    }
                    else
                    return null;
                }
                return item;
            }

            public int BorrowPayment(Borrow borrow)
            {
                double time = GetDateTime().Subtract(borrow.DateOfBorrow).TotalDays;
                time = time * 0.20;
                int payment = (int)Math.Floor(time);
                return payment;
            }

            public int DaysBetween(DateTime date1)
            {
                double time = GetDateTime().Subtract(date1).TotalDays;
                int payment = (int)Math.Floor(time);
                return payment;
            }

            public int NumberOfBorrows()
            {
                return Borrows.Count;
            }

            public int NumberOfClients()
            {
                return Clients.Count;
            }

            //test
            public string DisplayStatistics()
            {
                string statistics = "Liczba klientów w naszej bazie = " + NumberOfClients() + " || Liczba wypożyczonych książek = "+ NumberOfBorrows();
                return statistics;

            }

            public static DateTime GetDateTime()
            {
                DateTime Time = DateTime.Now;
                return Time;
            }

            public static string Read(string path)
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var sr = new StreamReader(fs);
                    return sr.ReadToEnd();
                }
            }

        
        }

        public interface IStockFeed
        {
            int GetSharePrice(string company);
        }

        public class StockAnalyzer
        {
            private IStockFeed stockFeed;
            public StockAnalyzer(IStockFeed feed)
            {
                stockFeed = feed;
            }
            public int GetContosoPrice()
            {
                return stockFeed.GetSharePrice("COOO");
            }
        }

}
