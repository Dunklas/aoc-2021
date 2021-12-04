using System;
using System.IO;
using aoc_2021.solutions;

namespace aoc_2021;
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

        string input = null;
        try
        {
            input = ReadInput(day);
        }
        catch (FileNotFoundException e)
        {
            Console.Error.WriteLine(e.Message);
            Environment.Exit(1);
        }

        switch (day)
        {
            case "1":
                new Day1().Solve(input);
                break;
            case "2":
                new Day2().Solve(input);
                break;
            case "3":
                new Day3().Solve(input);
                break;
            case "4":
                new Day4().Solve(input);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private static String ReadInput(string dayNr)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "solutions", $"day{dayNr}.input");
        return File.ReadAllText(path);
    }
}
