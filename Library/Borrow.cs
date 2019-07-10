using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Borrow : IBorrow
    {

        public Borrow(IClient client, DateTime dateOfBorrow, String description, Decimal priceInPLN)
        {
            if (!DateOfVisitIsValid(dateOfBorrow))
            {
                throw new Exception("Date of borrow invalid");
            }
            if (!PriceIsPositive(priceInPLN))
            {
                throw new Exception("Price invalid");
            }
            Client = client;
            DateOfBorrow = dateOfBorrow;
            Description = description;
            PriceInPLN = priceInPLN;
        }

        public Borrow()
        {

        }

        private static bool DateOfVisitIsValid(DateTime dateOfBorrow)
        {
            if (dateOfBorrow <= DateTime.Now)
                return true;
            else
                return false;
        }

        private static bool PriceIsPositive(Decimal priceInPLN)
        {
            if (priceInPLN >= 0M)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            var borrow = obj as Borrow;
            return borrow != null &&
                   EqualityComparer<IClient>.Default.Equals(Client, borrow.Client) &&
                   DateOfBorrow == borrow.DateOfBorrow &&
                   Description == borrow.Description &&
                   PriceInPLN == borrow.PriceInPLN;
        }

        public IClient Client { get; set; }

        public DateTime DateOfBorrow { get; set; }

        public String Description { get; set; }

        public Decimal PriceInPLN { get; set; }
    }
}
