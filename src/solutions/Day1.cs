using System;
using System.Linq;

namespace aoc_2021.solutions
{
    public class Day1
    {
        public void Solve(string input)
        {
            Console.WriteLine(SolvePart1(input));
            Console.WriteLine(SolvePart2(input));
        }

        public int SolvePart1(string input)
        {
            var depthMeasurements = ParseInput(input);
            var increases = 0;
            for (int i = 1; i < depthMeasurements.Length; i++)
            {
                if (depthMeasurements[i] > depthMeasurements[i-1])
                {
                    increases++;
                }
            } 
            return increases;
        }

        public int SolvePart2(string input)
        {
            var depthMeasurements = ParseInput(input);
            var increases = 0;
            for (int i = 3; i < depthMeasurements.Length; i++)
            {
                var firstWindow = depthMeasurements[i-3] + depthMeasurements[i-2] + depthMeasurements[i-1];
                var secondWindow = depthMeasurements[i-2] + depthMeasurements[i-1] + depthMeasurements[i];
                if (secondWindow > firstWindow)
                {
                    increases++;
                }
            }
            return increases;
        }

        private int[] ParseInput(string input)
        {
            return input.Split("\n")
                .Select(line => int.Parse(line))
                .ToArray();
        }
    }
}