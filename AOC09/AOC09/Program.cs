using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC09
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(a => long.Parse(a.Trim())).ToArray();
            var val = (long)0;
            var comp = new IntcodeComputer((long[])program.Clone());
            comp.Inputs.Add(2);
            var output = new List<long>();
            var s = Stopwatch.StartNew();
            while (val != long.MaxValue)
            {
                val = comp.Parse();
                if (val != long.MaxValue)
                {
                    output.Add(val);
                }
            }
            s.Stop();

            Console.WriteLine("Terminated. Took " + s.ElapsedMilliseconds + " ms. Output: " + string.Join(", ", output));
            Console.Read();
        }
    }
}