using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC01
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt");
            var ar1 = input.Split('\n').ToList().ConvertAll(a => float.Parse(a)).ToArray();

            var lastArrays = new List<float[]>();
            loop:;
            ar1 = Maths(ar1);
            lastArrays.Add(new List<float>(ar1).ToArray());
            if (lastArrays.Count < 2) goto loop;

            var equals = true;
            for (var i = 0; i < ar1.Count(); i++)
            {
                if (ar1[i] != lastArrays[lastArrays.Count - 2][i]) { equals = false; }
            }

            if (!equals) goto loop;

            var sum = 0f;
            lastArrays.ForEach(a => sum += a.Sum());

            Console.WriteLine(sum);
            Console.Read();
        }

        public static float[] Maths(float[] in1)
        {
            for (var i = 0; i < in1.Length; i++)
            {
                in1[i] = (float)Math.Floor(in1[i] / 3f) - 2;
                if (in1[i] < 0) { in1[i] = 0; }
            }
            return in1;
        }
    }
}