using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AOC07
{
    public class Amplifier
    {
        public int[] Program;
        public int instructionPointer = 0;
        public List<int> Inputs;
        public int Phase;
        public bool PhaseUsed = false;

        public int Cnt = 0;

        public Amplifier(int[] program, int phase)
        {
            Program = program;
            Inputs = new List<int>();
            Phase = phase;
        }

        public int ParseIntCode2()
        {
            Cnt = 0;

            var output = new List<int>();

            while (true)
            {
                var current = Program[instructionPointer];
                var currentS = current.ToString("D5");
                var opcode = current % 100;
                var modes = currentS.Substring(0, currentS.Length - 2).Select(d => int.Parse(d.ToString())).ToList().ConvertAll(a => a == 1 ? true : false); // true = immediate
                modes.Reverse();

                switch (opcode)
                {
                    case 1: // a * b at c
                        var a = GetParameter(0);
                        var b = GetParameter(1);
                        var c = Program[instructionPointer + 3];
                        Program[c] = a + b;
                        instructionPointer += 4;
                        break;
                    case 2:
                        a = GetParameter(0);
                        b = GetParameter(1);
                        c = Program[instructionPointer + 3];
                        Program[c] = a * b;
                        instructionPointer += 4;
                        break;
                    case 3:
                        //input[input[instructionPointer + 1]] = int.Parse(Console.ReadLine());
                        Cnt++;
                        if (!PhaseUsed)
                        {
                            Program[Program[instructionPointer + 1]] = Phase;
                            PhaseUsed = true;
                        }
                        else
                        {
                            Program[Program[instructionPointer + 1]] = Inputs[0];
                            Inputs.RemoveAt(0);
                        }
                        instructionPointer += 2;
                        break;
                    case 4:
                        //Console.WriteLine("Output: " + GetParameter(0));
                        instructionPointer += 2;
                        return GetParameter2(0, -2);
                    case 5:
                        if (GetParameter(0) != 0)
                        {
                            instructionPointer = GetParameter(1);
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        if (GetParameter(0) == 0)
                        {
                            instructionPointer = GetParameter(1);
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        Program[Program[instructionPointer + 3]] = GetParameter(0) < GetParameter(1) ? 1 : 0;
                        instructionPointer++;
                        break;
                    case 8:
                        Program[Program[instructionPointer + 3]] = GetParameter(0) == GetParameter(1) ? 1 : 0;
                        instructionPointer++;
                        break;
                    case 99:
                        return int.MaxValue;
                    default:
                        instructionPointer++;
                        break;
                }

                int GetParameter(int i)
                {
                    return modes[i] ? Program[instructionPointer + i + 1] : Program[Program[instructionPointer + i + 1]];
                }

                int GetParameter2(int i, int i2)
                {
                    return modes[i] ? Program[instructionPointer + i2 + 1] : Program[Program[instructionPointer + i2 + 1]];
                }
            }
        }
    }
}