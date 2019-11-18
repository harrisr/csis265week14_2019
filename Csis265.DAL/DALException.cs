using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.DAL
{
    public class DALException : Exception
    {
        public DALException() : this("An error occurred in the CSIS265 Data Access Layer")
        {   }

        public DALException(string message) : base(message)
        {   }
    }
}
