using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOC06
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var lines = input.Split('\n');

            var orbits = lines.ToList().ConvertAll(a => a.Split(')').ToList().ConvertAll(b => b.Trim()));

            var COM = new Node() { ID = "COM", Parent = null };
            GetOrbits(COM, orbits);

            Console.WriteLine("Enter first ID.");
            var id1 = Console.ReadLine();
            //var id1 = "YOU";
            Console.WriteLine("Enter second ID.");
            var id2 = Console.ReadLine();
            //ar id2 = "SAN";

            var counts = new List<long>();

            for (var i = 0; i < 1; i++)
            {
                var s = Stopwatch.StartNew();
                var one = SearchForNode(COM, id1);
                var two = SearchForNode(COM, id2);
                var d = Distance(one, two) - 2;
                Console.WriteLine("Distance: " + d);
                Console.WriteLine("Total Direct And Indirect: " + Node.TotalChildren(COM));
                s.Stop();
                counts.Add(s.ElapsedMilliseconds);
            }
            Console.WriteLine("Took " + counts.Average() + " milliseconds.");
            Console.Read();
        }

        public static Node GetOrbits(Node parent, List<List<string>> orbits)
        {
            foreach (var o in orbits)
            {
                if (o[0] == parent.ID)
                {
                    var n = new Node() { ID = o[1], Parent = parent };
                    parent.Children.Add(n);
                    n = GetOrbits(n, orbits);
                }
            }

            return parent;
        }

        public static Node SearchForNode(Node top, string id)
        {

            if (top.ID == id) { return top; }
            foreach (var c in top.Children)
            {
                var n = SearchForNode(c, id);
                if (n != null) { return n; }
            }

            return null;
        }
        
        public static int Distance(Node a, Node b)
        {
            if (a == null || b == null) { return -1; }
            NodesChecked = 0;
            return Search(0, a, b, new List<Node>());
        }

        public static int NodesChecked = 0;

        public static int Search(int depth, Node a, Node b, List<Node> Searched)
        {
            Console.WriteLine("Search: a is " + a.ID + "; Relatives are: " + string.Join(",", a.Relatives));
            Thread.Sleep(25);
            NodesChecked++;
            foreach (var r in a.Relatives)
            {
                if (r.ID == b.ID)
                {
                    return depth + 1;
                }
            }

            Searched.Add(a);
            foreach (var r in a.Relatives)
            {
                if (!Searched.Contains(r))
                {
                    var s = Search(depth + 1, r, b, Searched);
                    if (s != -1)
                    {
                        return s;
                    }
                }
            }

            return -1;
        }
    }

    public class Node
    {
        public string ID;
        public List<Node> Children = new List<Node>();
        public Node Parent;
        public List<Node> Relatives => Children.Concat(new List<Node>() { Parent }).ToList();

        public int StepsToTop()
        {

            var cnt = 0;
            var parent = Parent;
            
            while (true)
            {
                if (parent == null) { goto exit; }
                cnt++;
                parent = parent.Parent;
            }

            exit:;
            return cnt;
        }

        public static int TotalChildren(Node n)
        {
            var cnt = 0;
            n.Children.ForEach(ch =>
            {
                cnt += ch.StepsToTop();
                cnt += TotalChildren(ch);
            });
            return cnt;
        }

        public override string ToString()
        {
            return ID;
        }
    }
}