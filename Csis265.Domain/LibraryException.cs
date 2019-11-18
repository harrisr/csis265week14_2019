using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.Domain
{
    public class LibraryException : Exception
    {
        public LibraryException() : base("An exception occurred in the Library")
        {

        }

        public LibraryException(string message) : base(message)
        {

        }
    }
}
