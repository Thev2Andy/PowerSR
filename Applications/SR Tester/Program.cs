using System;
using PowerSR;

namespace SRTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string SRString = String.Empty;

            SRString = SRString.Set("Demo Property", $"Hello{Environment.NewLine}World!{Environment.NewLine}{Environment.NewLine}" + "${Newline}${Newline1}");
            Console.WriteLine(SRString.Get("Demo Property"));

            while (true)
            {
                Console.Write("Enter preperty identifier: ");
                string Identifier = Console.ReadLine();

                Console.Write("Enter preperty value: ");
                string Value = Console.ReadLine();

                SRString = SRString.Set(Identifier, Value);
                Console.WriteLine();
                Console.WriteLine($"Serialized properties:{Environment.NewLine}{SRString}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Serialized property count: {SRString.Length()}");
            }
        }
    }
}
