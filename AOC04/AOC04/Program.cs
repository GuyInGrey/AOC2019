using System;
using System.Collections;
using System.Collections.Generic;
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
            var input = File.ReadAllText("input.txt");

            var possible = new List<int>();

            for (var i = 146811; i < 612564; i++)
            {
                var n = i.ToString().ToCharArray().ToList().ConvertAll(a => int.Parse(a.ToString()));
                var p = 0;
                foreach (var j in n)
                {
                    if (j < p)
                    {
                        goto a;
                    }
                    p = j;
                }

                var dupes = n.Count != n.Distinct().Count();

                if (dupes)
                {
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
                }

                a:;
            }

            Console.WriteLine(possible.Count);
            Console.Read();
        }
    }
}