using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC09
{
    class Program
    {
        static void Main()
        {
            var program = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(a => long.Parse(a.Trim()));
            var comp = new IntcodeComputer(program.ToArray(), 2);

            var benchmark = Stopwatch.StartNew();

            var output = new List<long>();
            var val = (long)0;
            while (val != long.MaxValue)
            {
                val = comp.Parse();
                if (val != long.MaxValue)
                {
                    output.Add(val);
                }
            }

            benchmark.Stop();

            Console.WriteLine("Terminated. Took " + benchmark.ElapsedMilliseconds + " ms. Output: " + string.Join(", ", output));
            Console.Read();
        }
    }
}