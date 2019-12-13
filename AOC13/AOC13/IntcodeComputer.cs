using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC13
{
    class IntcodeComputer
    {
        public Dictionary<long, long> Program;
        public long instructionPointer = -2;
        public List<long> Inputs;
        public long RelativeBase = 0;
        public List<long> ExecutedOpcodes = new List<long>();

        public Func<long> GetInput;

        public IntcodeComputer(long[] program)
        {
            Program = new Dictionary<long, long>();
            var i = 0;
            foreach (var p in program)
            {
                Program.Add(i, p);
                i++;
            }
            Inputs = new List<long>();
        }

        public IntcodeComputer(long[] program, params long[] input) : this(program)
        {
            Inputs = input.ToList();
        }

        public long Parse()
        {
            instructionPointer += 2;

            while (true)
            {
                var current = Program[instructionPointer];
                var currentS = current.ToString("D5");
                var opcode = current % 100;
                var modes = currentS.Substring(0, currentS.Length - 2).Select(d => int.Parse(d.ToString())).ToList();
                modes.Reverse();

                ExecutedOpcodes.Add(opcode);
                switch (opcode)
                {
                    case 1: // a * b at c
                        var a = GetParameter(0, false);
                        var b = GetParameter(1, false);
                        var c = GetParameter(2, true);
                        SetToMemory(c, a + b);
                        instructionPointer += 4;
                        break;
                    case 2:
                        a = GetParameter(0, false);
                        b = GetParameter(1, false);
                        c = GetParameter(2, true);
                        SetToMemory(c, a * b);
                        instructionPointer += 4;
                        break;
                    case 3:
                        if (Inputs.Count == 0)
                        {
                            Inputs.Add(GetInput.Invoke());
                        }
                        SetToMemory(GetParameter(0, true), Inputs[0]);
                        Inputs.RemoveAt(0);
                        instructionPointer += 2;
                        break;
                    case 4:
                        return GetParameter(0, false);
                    case 5:
                        if (GetParameter(0, false) != 0)
                        {
                            instructionPointer = GetParameter(1, false);
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        if (GetParameter(0, false) == 0)
                        {
                            instructionPointer = GetParameter(1, false);
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        SetToMemory(GetParameter(2, true), GetParameter(0, false) < GetParameter(1, false) ? 1 : 0);
                        instructionPointer += 4;
                        break;
                    case 8:
                        SetToMemory(GetParameter(2, true), GetParameter(0, false) == GetParameter(1, false) ? 1 : 0);
                        instructionPointer += 4;
                        break;
                    case 99:
                        return long.MaxValue; // MaxValue signifies the program's end.
                    case 9:
                        RelativeBase += GetParameter(0, false);
                        instructionPointer += 2;
                        break;
                    default:
                        instructionPointer++;
                        break;
                }

                //Gets a value from memory at the given index.
                long GetFromMemory(long index)
                {
                    if (Program.ContainsKey(index)) { return Program[index]; }
                    return 0;
                }

                //Sets a value in memory at the given index.
                void SetToMemory(long index, long value)
                {
                    if (Program.ContainsKey(index)) { Program[index] = value; }
                    else { Program.Add(index, value); }
                    if (index < 0) { Console.WriteLine("Invalid address: " + index); }
                }

                long GetParameter(long i, bool writeParameter)
                {
                    if (modes[(int)i] == 0 && !writeParameter)
                    {
                        return GetFromMemory(GetFromMemory(instructionPointer + i + 1));
                    }
                    else if (modes[(int)i] == 1 || (modes[(int)i] == 0 && writeParameter))
                    {
                        return GetFromMemory(instructionPointer + i + 1);
                    }
                    else if (modes[(int)i] == 2 && !writeParameter)
                    {
                        return GetFromMemory(GetFromMemory(instructionPointer + i + 1) + RelativeBase);
                    }
                    else if (modes[(int)i] == 2 && writeParameter)
                    {
                        return GetFromMemory(instructionPointer + i + 1) + RelativeBase;
                    }

                    Console.WriteLine("Invalid parameter modes.");
                    return long.MaxValue;
                }
            }
        }
    }
}