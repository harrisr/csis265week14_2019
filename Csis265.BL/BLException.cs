using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.BL
{
    public class BLException : Exception
    {
        public BLException() : base("Something Bad Happened in the Business Layer")
        {    }

        public BLException(string message) : base(message)
        {    }
    }
}
