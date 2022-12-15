using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day5
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			//var result = RunPart1(data.Item1, data.Item2);
			var result = RunPart2(data.Item1, data.Item2);

			Console.WriteLine($"Result: {result}");
			return 0;
		}

		private static string RunPart2(List<Stack<char>> stacks, List<Movement> movements)
		{
			// Execute each movement
			foreach (var movement in movements)
			{
				var fromStack = stacks.ElementAt(movement.FromStackIndex - 1);
				var toStack = stacks.ElementAt(movement.ToStackIndex - 1);
				var tempStack = new Stack<char>();

				// Move a character from the top of the from stack to the top of the temp stack x times
				for (int i = 0; i < movement.NumberOfStacks; i++)
				{
					// Move the character
					var character = fromStack.Pop();
					tempStack.Push(character);
				}

				// Move all characters from the temp stack to the to stack
				while (tempStack.Count > 0)
				{
					// Move the character
					var character = tempStack.Pop();
					toStack.Push(character);
				}
			}

			// Collect the characters at the top of each stack to form a message
			var message = "";
			foreach (var stack in stacks)
			{
				message += stack.Peek();
			}

			return message;
			// Test: MCD
			// Actual: CNSCZWLVT
		}

		private static string RunPart1(List<Stack<char>> stacks, List<Movement> movements)
		{
			// Execute each movement
			foreach (var movement in movements)
			{
				var fromStack = stacks.ElementAt(movement.FromStackIndex - 1);
				var toStack = stacks.ElementAt(movement.ToStackIndex - 1);

				// Move a character from the top of the from stack to the top of the to stack x times
				for (int i = 0; i < movement.NumberOfStacks; i++)
				{
					// Move the character
					var character = fromStack.Pop();
					toStack.Push(character);
				}
			}

			// Collect the characters at the top of each stack to form a message
			var message = "";
			foreach (var stack in stacks)
			{
				message += stack.Peek();
			}

			return message;
			// Test: CMZ
			// Actual: CVCWCRTVQ
		}

		private static (List<Stack<char>>, List<Movement>) GetTestData()
		{
			string stackData =
@"    [D]    
[N] [C]    
[Z] [M] [P]";

			string movementData =

@"move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

			var stacks = ParseStackData(stackData);
			var movements = ParseMovementData(movementData);
			return (stacks, movements);
		}

		private static (List<Stack<char>>, List<Movement>) GetData()
		{
			string stackData =
@"    [P]                 [C] [C]    
    [W]         [B]     [G] [V] [V]
    [V]         [T] [Z] [J] [T] [S]
    [D] [L]     [Q] [F] [Z] [W] [R]
    [C] [N] [R] [H] [L] [Q] [F] [G]
[F] [M] [Z] [H] [G] [W] [L] [R] [H]
[R] [H] [M] [C] [P] [C] [V] [N] [W]
[W] [T] [P] [J] [C] [G] [W] [P] [J]";

			string movementData =

@"move 2 from 4 to 9
move 5 from 2 to 9
move 1 from 5 to 1
move 3 from 1 to 4
move 2 from 4 to 6
move 7 from 6 to 9
move 5 from 3 to 9
move 1 from 8 to 6
move 3 from 2 to 3
move 12 from 9 to 3
move 4 from 9 to 7
move 15 from 3 to 9
move 1 from 1 to 5
move 2 from 6 to 5
move 18 from 9 to 1
move 6 from 8 to 1
move 1 from 8 to 7
move 5 from 7 to 2
move 6 from 1 to 2
move 7 from 9 to 6
move 6 from 1 to 3
move 5 from 3 to 9
move 3 from 9 to 1
move 1 from 7 to 9
move 4 from 2 to 1
move 5 from 6 to 3
move 1 from 3 to 4
move 1 from 5 to 9
move 2 from 9 to 6
move 5 from 5 to 9
move 10 from 1 to 8
move 4 from 3 to 8
move 3 from 4 to 9
move 4 from 6 to 9
move 14 from 8 to 6
move 1 from 3 to 8
move 14 from 9 to 4
move 6 from 1 to 6
move 1 from 8 to 2
move 3 from 5 to 8
move 1 from 8 to 9
move 1 from 8 to 1
move 5 from 4 to 9
move 1 from 8 to 4
move 3 from 9 to 4
move 3 from 7 to 5
move 7 from 6 to 3
move 7 from 4 to 1
move 3 from 9 to 1
move 7 from 2 to 3
move 1 from 4 to 8
move 8 from 6 to 2
move 2 from 7 to 4
move 1 from 7 to 4
move 1 from 7 to 9
move 1 from 5 to 9
move 1 from 9 to 4
move 1 from 4 to 2
move 8 from 4 to 9
move 1 from 4 to 2
move 5 from 9 to 4
move 2 from 6 to 9
move 1 from 6 to 9
move 1 from 8 to 1
move 13 from 3 to 2
move 1 from 3 to 9
move 2 from 6 to 8
move 1 from 8 to 1
move 14 from 1 to 7
move 4 from 2 to 1
move 2 from 9 to 5
move 3 from 9 to 7
move 1 from 8 to 2
move 4 from 1 to 5
move 1 from 4 to 7
move 3 from 9 to 1
move 7 from 7 to 4
move 14 from 2 to 8
move 3 from 1 to 7
move 3 from 5 to 4
move 2 from 1 to 9
move 11 from 8 to 9
move 3 from 7 to 8
move 3 from 8 to 6
move 6 from 4 to 3
move 2 from 6 to 8
move 8 from 4 to 3
move 3 from 8 to 7
move 2 from 8 to 2
move 2 from 3 to 9
move 1 from 6 to 8
move 5 from 2 to 7
move 10 from 9 to 7
move 1 from 8 to 5
move 3 from 5 to 2
move 6 from 7 to 5
move 19 from 7 to 3
move 9 from 5 to 9
move 6 from 2 to 6
move 2 from 7 to 3
move 29 from 3 to 8
move 2 from 7 to 9
move 5 from 8 to 1
move 12 from 9 to 6
move 1 from 3 to 8
move 1 from 2 to 7
move 1 from 3 to 1
move 10 from 6 to 1
move 1 from 6 to 7
move 9 from 1 to 9
move 2 from 1 to 2
move 12 from 9 to 4
move 7 from 6 to 3
move 8 from 3 to 7
move 5 from 7 to 6
move 19 from 8 to 3
move 10 from 4 to 6
move 1 from 4 to 6
move 6 from 8 to 6
move 1 from 4 to 2
move 6 from 6 to 3
move 3 from 2 to 7
move 13 from 6 to 3
move 1 from 9 to 1
move 6 from 1 to 8
move 1 from 6 to 5
move 1 from 5 to 4
move 3 from 7 to 1
move 2 from 1 to 3
move 11 from 3 to 8
move 1 from 4 to 3
move 3 from 8 to 4
move 1 from 7 to 5
move 3 from 8 to 9
move 2 from 9 to 2
move 7 from 8 to 3
move 1 from 7 to 9
move 1 from 1 to 4
move 32 from 3 to 4
move 1 from 5 to 9
move 2 from 8 to 3
move 2 from 6 to 4
move 1 from 9 to 4
move 1 from 9 to 2
move 3 from 3 to 1
move 1 from 8 to 6
move 1 from 6 to 2
move 1 from 9 to 3
move 1 from 1 to 7
move 1 from 8 to 7
move 2 from 3 to 8
move 1 from 8 to 4
move 1 from 1 to 2
move 2 from 4 to 8
move 1 from 1 to 8
move 26 from 4 to 6
move 3 from 8 to 5
move 3 from 7 to 6
move 7 from 6 to 3
move 18 from 6 to 8
move 16 from 8 to 9
move 1 from 5 to 1
move 2 from 8 to 3
move 3 from 9 to 8
move 3 from 6 to 4
move 2 from 5 to 4
move 1 from 6 to 4
move 2 from 7 to 2
move 2 from 3 to 9
move 4 from 8 to 3
move 1 from 1 to 2
move 6 from 9 to 7
move 2 from 2 to 5
move 12 from 3 to 1
move 9 from 9 to 2
move 10 from 1 to 3
move 2 from 5 to 9
move 8 from 4 to 7
move 13 from 7 to 6
move 6 from 6 to 5
move 4 from 5 to 3
move 2 from 5 to 4
move 8 from 4 to 3
move 1 from 7 to 2
move 15 from 2 to 7
move 8 from 3 to 7
move 1 from 1 to 6
move 7 from 7 to 1
move 5 from 1 to 6
move 7 from 3 to 2
move 3 from 1 to 6
move 12 from 7 to 9
move 12 from 9 to 8
move 1 from 7 to 1
move 2 from 9 to 5
move 1 from 1 to 9
move 4 from 4 to 2
move 4 from 8 to 4
move 2 from 7 to 2
move 4 from 6 to 5
move 4 from 8 to 9
move 1 from 8 to 4
move 5 from 5 to 3
move 5 from 2 to 4
move 5 from 9 to 5
move 1 from 3 to 6
move 1 from 7 to 8
move 12 from 3 to 9
move 4 from 2 to 6
move 7 from 4 to 9
move 13 from 6 to 4
move 3 from 6 to 9
move 4 from 4 to 2
move 1 from 3 to 4
move 21 from 9 to 7
move 4 from 2 to 1
move 3 from 5 to 4
move 8 from 7 to 6
move 2 from 7 to 2
move 11 from 4 to 2
move 1 from 9 to 7
move 1 from 5 to 7
move 1 from 1 to 8
move 5 from 2 to 5
move 1 from 3 to 5
move 2 from 4 to 9
move 3 from 4 to 8
move 3 from 1 to 8
move 1 from 9 to 6
move 8 from 7 to 8
move 9 from 6 to 5
move 1 from 9 to 6
move 1 from 6 to 4
move 3 from 7 to 5
move 1 from 6 to 9
move 12 from 5 to 1
move 2 from 5 to 8
move 1 from 9 to 6
move 2 from 7 to 6
move 9 from 1 to 8
move 1 from 6 to 9
move 1 from 9 to 2
move 1 from 4 to 2
move 2 from 6 to 7
move 5 from 8 to 3
move 2 from 7 to 4
move 16 from 8 to 5
move 2 from 3 to 8
move 7 from 5 to 1
move 3 from 3 to 8
move 7 from 5 to 7
move 4 from 5 to 2
move 6 from 7 to 9
move 2 from 9 to 6
move 2 from 9 to 2
move 1 from 6 to 8
move 12 from 2 to 6
move 2 from 9 to 6
move 1 from 5 to 2
move 3 from 5 to 4
move 9 from 2 to 6
move 6 from 8 to 3
move 1 from 7 to 5
move 1 from 6 to 7
move 1 from 7 to 8
move 1 from 5 to 8
move 5 from 1 to 2
move 3 from 4 to 5
move 4 from 6 to 8
move 5 from 2 to 9
move 5 from 8 to 4
move 1 from 1 to 4
move 9 from 8 to 4
move 1 from 2 to 3
move 3 from 6 to 8
move 4 from 9 to 2
move 2 from 6 to 4
move 2 from 3 to 1
move 4 from 4 to 7
move 6 from 4 to 5
move 10 from 6 to 8
move 4 from 1 to 9
move 4 from 7 to 5
move 3 from 3 to 9
move 6 from 9 to 8
move 2 from 2 to 9
move 8 from 4 to 3
move 2 from 2 to 7
move 1 from 4 to 9
move 6 from 3 to 8
move 2 from 7 to 8
move 6 from 5 to 9
move 5 from 5 to 6
move 2 from 5 to 9
move 7 from 9 to 5
move 2 from 1 to 9
move 6 from 5 to 8
move 1 from 5 to 1
move 2 from 3 to 6
move 1 from 3 to 6
move 4 from 9 to 5
move 1 from 3 to 4
move 1 from 1 to 2
move 1 from 2 to 1
move 1 from 6 to 8
move 14 from 8 to 5
move 6 from 5 to 1
move 16 from 8 to 3
move 2 from 8 to 2
move 10 from 6 to 7
move 1 from 6 to 9
move 2 from 2 to 9
move 2 from 7 to 3
move 1 from 8 to 5
move 3 from 9 to 1
move 4 from 9 to 5
move 9 from 3 to 8
move 2 from 3 to 6
move 5 from 3 to 8
move 1 from 4 to 2
move 12 from 8 to 4
move 1 from 8 to 9
move 4 from 5 to 9
move 7 from 7 to 1
move 10 from 5 to 2
move 2 from 5 to 2
move 1 from 6 to 5
move 2 from 5 to 2
move 5 from 2 to 6
move 4 from 9 to 6
move 6 from 4 to 9
move 2 from 3 to 4
move 6 from 4 to 7
move 6 from 7 to 5
move 10 from 1 to 5
move 4 from 1 to 2
move 4 from 6 to 3
move 6 from 9 to 7
move 2 from 4 to 9
move 7 from 7 to 6
move 1 from 9 to 7
move 2 from 9 to 8
move 2 from 8 to 2
move 1 from 2 to 5
move 3 from 8 to 4
move 4 from 2 to 7
move 3 from 4 to 7
move 2 from 3 to 5
move 2 from 3 to 2
move 18 from 5 to 3
move 6 from 3 to 1
move 8 from 3 to 1
move 8 from 7 to 9
move 9 from 2 to 5
move 3 from 2 to 3
move 7 from 3 to 7
move 3 from 6 to 4
move 1 from 7 to 1
move 7 from 6 to 7
move 1 from 2 to 9
move 1 from 4 to 2
move 13 from 7 to 2
move 10 from 5 to 3
move 1 from 2 to 9
move 7 from 1 to 5
move 8 from 9 to 5
move 1 from 9 to 5
move 1 from 9 to 8
move 1 from 8 to 2
move 8 from 5 to 3
move 18 from 3 to 5
move 2 from 4 to 1
move 3 from 2 to 5
move 27 from 5 to 1
move 17 from 1 to 5
move 2 from 2 to 3
move 1 from 6 to 5
move 2 from 2 to 5
move 1 from 6 to 4
move 1 from 6 to 9
move 2 from 3 to 5
move 17 from 5 to 6
move 1 from 9 to 3
move 6 from 2 to 4
move 1 from 3 to 2
move 3 from 4 to 9
move 1 from 2 to 9
move 1 from 4 to 7
move 3 from 5 to 2
move 2 from 5 to 1
move 1 from 5 to 2
move 1 from 7 to 3
move 18 from 1 to 4
move 1 from 3 to 1
move 5 from 4 to 2
move 1 from 5 to 1
move 9 from 2 to 7
move 1 from 4 to 5
move 1 from 2 to 9
move 8 from 6 to 2
move 13 from 4 to 2
move 2 from 4 to 9
move 1 from 5 to 2
move 1 from 6 to 8
move 6 from 7 to 5
move 1 from 8 to 4
move 1 from 7 to 6
move 1 from 6 to 1
move 7 from 6 to 5
move 1 from 7 to 9
move 6 from 9 to 3
move 2 from 9 to 7
move 2 from 5 to 7
move 4 from 7 to 8
move 4 from 5 to 4
move 1 from 6 to 7
move 3 from 3 to 8
move 6 from 5 to 9
move 2 from 3 to 5
move 4 from 4 to 7
move 1 from 3 to 1
move 2 from 2 to 3
move 6 from 9 to 6
move 1 from 7 to 1
move 19 from 2 to 4
move 2 from 5 to 6
move 2 from 8 to 9
move 2 from 1 to 2
move 2 from 2 to 5
move 2 from 4 to 3
move 4 from 6 to 2
move 1 from 7 to 8
move 6 from 1 to 8
move 3 from 5 to 1
move 5 from 2 to 5
move 1 from 6 to 7
move 9 from 8 to 1
move 2 from 3 to 6
move 4 from 6 to 5
move 1 from 6 to 2
move 9 from 5 to 2
move 3 from 4 to 6
move 12 from 4 to 6
move 1 from 9 to 4
move 1 from 3 to 1
move 3 from 4 to 8
move 1 from 3 to 6
move 6 from 6 to 2
move 1 from 4 to 5
move 3 from 6 to 2
move 4 from 1 to 5
move 1 from 5 to 1
move 2 from 8 to 9
move 7 from 6 to 3
move 1 from 3 to 1
move 1 from 8 to 1
move 3 from 8 to 9
move 4 from 3 to 5
move 3 from 7 to 3
move 5 from 3 to 7
move 1 from 9 to 1
move 4 from 9 to 2
move 15 from 2 to 7
move 14 from 1 to 7
move 5 from 5 to 1
move 9 from 7 to 2
move 1 from 9 to 6
move 1 from 7 to 4
move 1 from 4 to 6
move 2 from 6 to 2
move 9 from 2 to 5
move 4 from 2 to 4
move 4 from 7 to 5
move 6 from 5 to 9
move 7 from 1 to 8
move 6 from 2 to 8
move 1 from 1 to 2
move 3 from 9 to 5
move 18 from 7 to 8
move 2 from 4 to 6
move 2 from 4 to 6
move 3 from 7 to 6
move 3 from 5 to 3
move 1 from 2 to 6
move 5 from 6 to 8
move 29 from 8 to 1
move 2 from 3 to 5
move 25 from 1 to 6
move 2 from 9 to 5
move 1 from 7 to 8
move 6 from 8 to 2
move 1 from 9 to 1
move 15 from 6 to 8
move 1 from 3 to 8
move 14 from 8 to 7
move 5 from 1 to 3
move 1 from 6 to 2
move 2 from 5 to 7
move 10 from 6 to 2
move 4 from 5 to 7
move 6 from 5 to 1
move 2 from 1 to 4
move 19 from 7 to 9";

			var stacks = ParseStackData(stackData);
			var movements = ParseMovementData(movementData);
			return (stacks, movements);
		}

		private static List<Stack<char>> ParseStackData(string data)
		{
			var stacks = new List<Stack<char>>();

			var rows = data.Split("\r\n");//, StringSplitOptions.None);
			int numRows = rows.Length;
			int numColumns = (rows[0].Length + 1) / 4; // Each stack is 4 characters, except the last stack is 3 characters

			for (int j = 0; j < numColumns; j++)
			{
				stacks.Add(new Stack<char>());
			}

			// Loop over each row, starting at the bottom
			for (int i = numRows - 1; i >= 0; i--)
			{
				var row = rows[i];

				// Parse the characters for each stack
				for (int j = 0; j < numColumns; j++)
				{
					var stackCharacters = row.Substring(j * 4, 3); // Should look like "[P]" or "   "
					if (stackCharacters.Contains('['))
					{
						// Parse the character and add to the stack
						var character = stackCharacters.ToCharArray()[1];
						stacks.ElementAt(j).Push(character);
					}
				}
			}

			return stacks;
		}

		private static List<Movement> ParseMovementData(string data)
		{
			var movements = new List<Movement>();

			var rows = data.Split("\r\n");

			foreach (var row in rows)
			{
				// Example: move 2 from 4 to 9
				var parts = row.Split(' ');
				var movement = new Movement
				{
					NumberOfStacks = int.Parse(parts[1]),
					FromStackIndex = int.Parse(parts[3]),
					ToStackIndex = int.Parse(parts[5])
				};

				movements.Add(movement);
			}

			return movements;
		}

		class Movement
		{
			public int NumberOfStacks { get; set; }
			public int FromStackIndex { get; set; } // One-based index
			public int ToStackIndex { get; set; } // One-based index
		}
	}
}
