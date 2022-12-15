using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day10
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			//var result = RunPart1(data);
			var result = RunPart2(data);

			return result;
		}

		private static int RunPart2(List<Instruction> instructions)
		{
			int spritePosition = 1; // The middle of the sprite starts at position 1, and includes positions 0 and 2
			int cycle = 1; // Loop counter
			int pixelIndex = 0;

			var pixels = new List<char>(); // (#) if list, (.) if dark

			// Loop over each instruction, double-counting AddX operations
			for (int i = 0; i < instructions.Count; i++)
			{
				// If the current index is within one count of the sprite's position
				if (Math.Abs(spritePosition - pixelIndex) <= 1)
				{
					// Light the pixel
					pixels.Add('#');
				}
				else
				{
					// Leave the pixel dark
					pixels.Add('.');
				}

				var instruction = instructions[i];

				if (instruction.IsNoOp)
				{
					// Do nothing
				}
				else if (instruction.IsFirstCycle)
				{
					instruction.IsFirstCycle = false;
					i--; // Complete this instruction in the next cycle
				}
				else
				{
					spritePosition += instruction.AddX;
				}

				// Increment the cycle
				cycle++;

				// Increment the pixel index. Reset to 0 at 40
				pixelIndex++;
				if (pixelIndex == 40)
				{
					pixelIndex = 0;
				}
			}

			// Draw the pixels
			DrawCrt(pixels);

			return 0;
			/*
				Test:
					##..##..##..##..##..##..##..##..##..##..
					###...###...###...###...###...###...###.
					####....####....####....####....####....
					#####.....#####.....#####.....#####.....
					######......######......######......####
					#######.......#######.......#######.....
			 */
			/*
				Actual:
					###...##..###....##..##..###..#..#.###..
					#..#.#..#.#..#....#.#..#.#..#.#..#.#..#.
					#..#.#..#.#..#....#.#....###..####.#..#.
					###..####.###.....#.#....#..#.#..#.###..
					#....#..#.#....#..#.#..#.#..#.#..#.#....
					#....#..#.#.....##...##..###..#..#.#....
				PAPJCBHP
			 */
		}

		private static void DrawCrt(List<char> pixels)
		{
			int count = 0;

			foreach (var pixel in pixels)
			{
				Console.Write(pixel);

				count++;
				if (count % 40 == 0)
				{
					Console.WriteLine();
				}
			}
		}

		private static int RunPart1(List<Instruction> instructions)
		{
			int x = 1; // x starts at 1
			int cycle = 1; // Loop counter

			var signalStrengths = new List<int>();

			// Loop over each instruction, double-counting AddX operations
			for (int i = 0; i < instructions.Count; i++)
			{
				// Calculate the signal strength at certain cycles
				if ((cycle - 20) % 40 == 0)
				{
					int signalStrength = x * cycle;
					signalStrengths.Add(signalStrength);
				}

				var instruction = instructions[i];

				if (instruction.IsNoOp)
				{
					// Do nothing
				}
				else if (instruction.IsFirstCycle)
				{
					instruction.IsFirstCycle = false;
					i--; // Complete this instruction in the next cycle
				}
				else
				{
					x += instruction.AddX;
				}

				// Increment the cycle
				cycle++;
			}

			return signalStrengths.Sum();
			// Test: 13140
			// Actual: 14340
		}

		private static List<Instruction> GetTestData()
		{
			string data =
@"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";

			return ParseData(data);
		}

		private static List<Instruction> GetData()
		{
			string data =
@"noop
noop
noop
addx 6
addx -1
noop
addx 5
noop
noop
addx -12
addx 19
addx -1
noop
addx 4
addx -11
addx 16
noop
noop
addx 5
addx 3
addx -2
addx 4
noop
noop
noop
addx -37
noop
addx 3
addx 2
addx 5
addx 2
addx 10
addx -9
noop
addx 1
addx 4
addx 2
noop
addx 3
addx 2
addx 5
addx 2
addx 3
addx -2
addx 2
addx 5
addx -40
addx 25
addx -22
addx 2
addx 5
addx 2
addx 3
addx -2
noop
addx 23
addx -18
addx 2
noop
noop
addx 7
noop
noop
addx 5
noop
noop
noop
addx 1
addx 2
addx 5
addx -40
addx 3
addx 8
addx -4
addx 1
addx 4
noop
noop
noop
addx -8
noop
addx 16
addx 2
addx 4
addx 1
noop
addx -17
addx 18
addx 2
addx 5
addx 2
addx 1
addx -11
addx -27
addx 17
addx -10
addx 3
addx -2
addx 2
addx 7
noop
addx -2
noop
addx 3
addx 2
noop
addx 3
addx 2
noop
addx 3
addx 2
addx 5
addx 2
addx -5
addx -2
addx -30
addx 14
addx -7
addx 22
addx -21
addx 2
addx 6
addx 2
addx -1
noop
addx 8
addx -3
noop
addx 5
addx 1
addx 4
noop
addx 3
addx -2
addx 2
addx -11
noop
noop
noop";

			return ParseData(data);
		}

		private static List<Instruction> ParseData(string data)
		{
			var rows = data.Split("\r\n");

			var instructions = new List<Instruction>();

			foreach (string row in rows)
			{
				bool isNoOp = row == "noop";

				var instruction = new Instruction
				{
					IsNoOp = isNoOp,
					AddX = !isNoOp ? int.Parse(row.Split(' ')[1]) : 0
				};

				instructions.Add(instruction);
			}

			return instructions;
		}

		class Instruction
		{
			public bool IsNoOp { get; set; }
			public int AddX { get; set; }

			public bool IsFirstCycle { get; set; } = true;
		}
	}
}
