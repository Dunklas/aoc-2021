using System;

namespace aoc_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("No day provided");
                Environment.Exit(1);
            }
            var day = args[0];

            switch (day)
            {
                case "1":
                    Console.WriteLine("Yay");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
