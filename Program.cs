using System;

namespace ActiveDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PrincipalContext(ContextType.Domain, "dcs.azdcs.gov");

            Console.WriteLine("Hello World!");
        }
    }
}
