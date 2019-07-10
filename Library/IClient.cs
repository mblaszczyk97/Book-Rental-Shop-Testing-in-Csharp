using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IClient
    {

        String FirstName { get; set; }

        String LastName { get; set; }

        String PESEL { get; set; }

        Int32 Age { get; set; }

        Gender Sex { get; set; }

        List<IBorrow> Borrowed { get; set; }
    }
}
