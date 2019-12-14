using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var reactions = File.ReadAllText("input.txt").Split('\n').ToList().ConvertAll(a => Reaction.FromString(a));
            var fuelReaction = reactions.Where(a => a.Output.Item2 == "FUEL").First();
            //Console.WriteLine(string.Join("\n", reactions));
            Console.WriteLine("\n\nFUEL reaction: " + fuelReaction);

            var oreUsed = (long)0;
            var fuelMade = (long)0;


            var neededReactions = new List<Reaction>() { fuelReaction };
            var running = true;

            var excess = new Dictionary<string, int>();

            while (oreUsed < 1000000000000)
            {
                while (running)
                {
                    var newReactions = new List<Reaction>();
                    foreach (var r in neededReactions)
                    {
                        foreach (var i in r.Inputs)
                        {
                            if (i.Item2 == "ORE")
                            {
                                oreUsed += i.Item1;

                                continue;
                            }
                            var quantityNeeded = i.Item1;

                            if (excess.ContainsKey(i.Item2))
                            {
                                quantityNeeded -= excess[i.Item2];
                                excess[i.Item2] = 0;
                            }

                            stillNeeded:;
                            if (quantityNeeded < 0)
                            {
                                if (excess.ContainsKey(i.Item2))
                                {
                                    excess[i.Item2] = -quantityNeeded;
                                }
                                else
                                {
                                    excess.Add(i.Item2, -quantityNeeded);
                                }
                                goto notNeeded;
                            }
                            if (quantityNeeded == 0) { goto notNeeded; }
                            var r2 = reactions.Where(a => a.Output.Item2 == i.Item2).First();
                            newReactions.Add(r2);

                            quantityNeeded -= r2.Output.Item1;
                            if (quantityNeeded != 0) { goto stillNeeded; }
                            notNeeded:;
                        }
                    }

                    neededReactions = newReactions;

                    running = neededReactions.Count > 0;

                    a:;
                    foreach (var e in excess)
                    {
                        if (e.Value == 0) { excess.Remove(e.Key); goto a; }
                    }

                    //Console.WriteLine("\n\n\nOre Used: " + oreUsed + "\nNew list of needed reactions:\n" + string.Join("\n", neededReactions) + "\n\n" + "Excess: " + string.Join(", ", excess));
                    //Console.ReadLine();
                }

                neededReactions = new List<Reaction>() { fuelReaction };
                fuelMade++;
                running = true;
                Console.Title = string.Format("{0:n0}", oreUsed) + " : " + string.Format("{0:n0}", (fuelMade - 1));
            }

            Console.WriteLine(oreUsed + " : " + (fuelMade - 1));

            Console.Read();
        }
    }

    public class Reaction
    {
        public List<(int, string)> Inputs;
        public (int, string) Output;

        public override string ToString()
        {
            return string.Join(" + ", Inputs.ConvertAll(a => a.Item1 + "" + a.Item2)) + " => " + Output.Item1 + "" + Output.Item2;
        }

        public static Reaction FromString(string s)
        {
            var parts1 = Regex.Split(s, " => ").ToList();
            var inputParts = Regex.Split(parts1[0], ", ").ToList().ConvertAll(a => GetChemical(a));

            var r = new Reaction();
            r.Inputs = inputParts;
            r.Output = GetChemical(parts1[1]);
            return r;
        }

        public static (int, string) GetChemical(string s)
        {
            return (int.Parse(s.Split(' ')[0].Trim()), s.Split(' ')[1].Trim());
        }
    }
}