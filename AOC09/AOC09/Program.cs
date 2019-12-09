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
            var output = new List<long>();

            var benchmark = Stopwatch.StartNew();

            for (var i = 0; i < 100; i++)
            {
                var comp = new IntcodeComputer((long[])program.ToArray().Clone(), 2);
                output.Clear();
                var val = (long)0;
                while (val != long.MaxValue)
                {
                    val = comp.Parse();
                    if (val != long.MaxValue)
                    {
                        output.Add(val);
                    }
                }
            }

            benchmark.Stop();

            Console.WriteLine("Terminated. Took " + (benchmark.ElapsedMilliseconds/100) + " ms average. Output: " + string.Join(", ", output));
            Console.Read();
        }
    }
}