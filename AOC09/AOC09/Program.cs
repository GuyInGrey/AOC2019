using System;
using System.Collections.Generic;
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
            comp.Inputs.Add(8);
            while (val != long.MaxValue)
            {
                val = comp.Parse();
                Console.WriteLine(val == long.MaxValue ? "Terminated." : val.ToString());
            }
            Console.WriteLine(string.Join(",", comp.ExecutedOpcodes));
            Console.WriteLine("");
            Console.WriteLine(string.Join(",", comp.PointersWhereRelativeBaseWasTouched));
            Console.Read();
        }
    }
}