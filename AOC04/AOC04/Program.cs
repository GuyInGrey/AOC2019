using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC04
{
    class Program
    {
        static void Main()
        {
            var s = Stopwatch.StartNew();
            var possible = new List<int>();
            var range = (146810, 612564);

            for (var i = range.Item1 + 1; i < range.Item2; i++)
            {
                var n = i.ToString().Select(d => int.Parse(d.ToString())).ToList();
                var p = 0;
                foreach (var j in n)
                {
                    if (j < p)
                    {
                        goto a;
                    }
                    p = j;
                }

                var t = new Dictionary<int, int>();
                foreach (var l in n)
                {
                    if (t.ContainsKey(l))
                    {
                        t[l] = t[l] + 1;
                    }
                    else
                    {
                        t.Add(l, 1);
                    }
                }

                if (t.ContainsValue(2))
                {
                    possible.Add(int.Parse(n[0].ToString() + n[1].ToString() + n[2].ToString() + n[3].ToString() + n[4].ToString() + n[5].ToString()));
                }

                a:;
            }
            s.Stop();

            Console.WriteLine(possible.Count);
            Console.WriteLine(s.ElapsedMilliseconds + "ms elapsed.");
            Console.Read();
        }
    }
}