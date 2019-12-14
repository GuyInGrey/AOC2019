using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOC13
{
    class Program
    {
        static void Main()
        {
            var debug = false;
            var times = new List<long>();
            var score = (long)0;

            for (var i = 0; i < 30; i++)
            {
                var stop = Stopwatch.StartNew();
                var input = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(a => long.Parse(a)).ToArray();
                input[0] = 2;
                var comp = new IntcodeComputer(input);

                var joystick = 0;
                var running = true;

                var ballPos = (0, 0);
                var paddlePos = (0, 0);

                var delay = 5;

                Task.Run(new Action(() =>
                {
                    while (true)
                    {
                        comp.GetInput = new Func<long>(() =>
                        {
                            if (paddlePos.Item1 < ballPos.Item1)
                            {
                                joystick = 1;
                            }
                            else if (paddlePos.Item1 > ballPos.Item1)
                            {
                                joystick = -1;
                            }
                            else
                            {
                                joystick = 0;
                            }

                            if (!debug)
                            {
                                Thread.Sleep(delay);
                            }
                            return joystick;
                        });

                        var var1 = comp.Parse();
                        if (var1 == long.MaxValue) { break; }
                        var var2 = comp.Parse();
                        if (var2 == long.MaxValue) { break; }
                        var var3 = comp.Parse();
                        if (var3 == long.MaxValue) { break; }

                        if (var1 == -1 && var2 == 0)
                        {
                            if (!debug)
                            {
                                Console.Title = "Score: " + var3;
                            }
                            score = var3;
                            if (!debug)
                            {
                                Thread.Sleep(delay);
                            }
                        }
                        else
                        {
                            if (!debug)
                            {
                                Console.SetCursorPosition((int)var1, (int)var2);
                                Console.ForegroundColor =
                                    var3 == 0 ? ConsoleColor.White :
                                    var3 == 1 ? ConsoleColor.White :
                                    var3 == 2 ? ConsoleColor.Blue :
                                    var3 == 3 ? ConsoleColor.Yellow :
                                    var3 == 4 ? ConsoleColor.Red : ConsoleColor.Magenta;
                                Console.Write(
                                    var3 == 0 ? " " :
                                    var3 == 1 ? "█" :
                                    var3 == 2 ? "^" :
                                    var3 == 3 ? "/" :
                                    var3 == 4 ? "*" : "?");
                            }

                            if (var3 == 4)
                            {
                                ballPos = ((int)var1, (int)var2);
                            }
                            else if (var3 == 3)
                            {
                                paddlePos = ((int)var1, (int)var2);
                            }
                        }

                        if (!debug)
                        {
                            Console.SetCursorPosition(0, 0);
                        }
                    }

                    running = false;
                }));

                while (running) { }
                stop.Stop();
                times.Add(stop.ElapsedMilliseconds);
            }

            Console.WriteLine(score + "; Took " + times.Average() + " ms.");
            Console.Read();
        }
    }
}
