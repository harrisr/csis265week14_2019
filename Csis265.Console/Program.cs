using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MyGenre temp = new MyGenre();

            temp.Id = 42;
            temp.Name = "Aardvark";
            temp.DateCreated = DateTime.Now;

            System.Console.WriteLine(temp.Id);
            System.Console.WriteLine(temp.Name);
            System.Console.WriteLine(temp.DateCreated);

            System.Console.ReadLine();
        }
    }
}
