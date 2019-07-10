using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IBorrow
    {
        IClient Client { get; set; }

        DateTime DateOfBorrow{ get; set; }

        String Description { get; set; }

        Decimal PriceInPLN { get; set; }

    }
}
