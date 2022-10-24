using System;
using PowerSR;

namespace SRTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string SRString = String.Empty;

            Serialize:
            Console.Write("Enter preperty identifier: ");
            string Identifier = Console.ReadLine();

            Console.Write("Enter preperty value: ");
            string Value = Console.ReadLine();

            SRString = SRString.Set(Identifier, Value);
            Console.WriteLine();
            Console.WriteLine($"Serialized properties:{Environment.NewLine}{SRString}");
            Console.WriteLine();
            Console.WriteLine();
            goto Serialize;
        }
    }
}
