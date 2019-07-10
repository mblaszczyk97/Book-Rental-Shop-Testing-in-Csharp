using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExecute
{
    class Program
    {
        public static void Main()
        {
            LibraryShop ba = new LibraryShop();
            Client client = new Client("Zbigniew", "Kolonko", "82070111111", 67, Gender.Male);
            ba.AddClient(client);
            Borrow borrow = new Borrow(client, DateTime.Parse("2019-02-12 12:33"), "Władca Pierścieni - 2 miesiące", 5M);
            Borrow borrow1 = new Borrow(client, DateTime.Parse("2019-01-11 12:37"), "Gra o Tron", 5M);
            ba.AddBorrow(client, borrow);
            ba.AddBorrow(client, borrow1);
            System.Diagnostics.Debug.WriteLine("WYPISYWANIE TESTÓW:");
            System.Console.WriteLine("Godzina: "+LibraryShop.GetDateTime());
            System.Console.WriteLine();
            System.Console.WriteLine(ba.FindClient("Zbigniew").FirstName+" "+ba.FindClient("Zbigniew").LastName);
            System.Console.WriteLine("Wypożyczonych książek "+ ba.FindBorrows("Zbigniew").Count);
            System.Console.WriteLine("Pieniądze do zapłaty za wypożyczenie klienta (20 groszy za dzień): "+ba.BorrowPayment(borrow) +"zł " + ba.BorrowPayment(borrow1) + "zł");
            System.Console.WriteLine();
            System.Console.WriteLine(ba.DisplayStatistics());
            ba.DeleteClient(client);
            System.Console.WriteLine(ba.DisplayStatistics());
            System.Console.ReadLine();
            //.Diagnostics.Debug.WriteLine(ba.BorrowPayment(borrow)+" "+"zł");
          
        }

    }
}
