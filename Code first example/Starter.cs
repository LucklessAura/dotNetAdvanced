using System;
using Database;

namespace EFCore
{
    public class Starter
    { 
         public static int Main()
        {
            Services service = new Services();
            service.addTestValue();
            Console.WriteLine("This works fine");
            return 0;
        }
    }

   
}


