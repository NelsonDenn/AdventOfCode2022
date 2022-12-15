﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day2
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			var result = RunPart1(data);
			//var result = RunPart2(data);

			return result;
		}

		private static int RunPart2(List<Round> rounds)
		{
			return 0;
		}

		private static int RunPart1(List<Round> rounds)
		{
			return rounds.Sum(r => r.MyScore);
			// Test: 15
			// Actual: 12156
		}

		private static List<Round> GetTestData()
		{
			string data =
@"A Y
B X
C Z";

			return ParseData(data);
		}

		private static List<Round> GetData()
		{
			string data =
@"C Z
C Z
A Y
A X
C Z
C Z
A X
C X
A X
C X
A X
A X
A X
A X
C Y
A X
A X
A Y
C Z
C X
C Z
C Y
B Y
C Y
C X
C X
A Z
C Y
A Z
A Z
B X
A Y
A X
B Y
C X
A X
A X
C Z
C Y
B X
A Y
C Z
C Y
C Z
C Z
A X
C Z
C Y
C X
C Z
A X
C Z
A Y
A X
A X
A X
A X
C X
A X
A X
C X
A X
C Y
C Y
A X
C Z
C Y
A X
C Z
C Y
C Z
C Z
B X
C Z
C X
C X
B Z
A X
A X
C Z
C X
C X
A X
A Y
C X
C Y
A X
C Z
A Z
C Z
A Z
A Z
C X
A X
A X
C Z
C Y
C Z
A X
A X
C X
A X
C X
A X
A Z
A Y
C Y
A Y
C Z
A X
A Y
C X
A Z
C Y
C Z
C Z
A Y
C Z
A Z
C Z
A Z
A Z
A X
C X
A Y
A X
C Z
A X
C Z
C Z
A X
C Z
C X
A Z
A X
A Z
A X
C Z
A X
C X
A X
A X
C X
C Z
C Y
C Z
A Y
C Z
C Z
A X
C X
B X
C X
A X
C Y
C Z
C Z
C Z
A Z
C Y
A X
C X
B X
B X
C Y
C X
A X
C X
C X
A Y
C Z
A X
A X
A X
C Z
A X
C Z
C Y
C Z
C X
A X
C Z
A X
C Z
C X
C X
A X
C Z
C Z
C X
A Y
B X
B Z
A Z
C Z
A X
A X
A X
C X
C X
C Y
C Z
A Y
B Z
A X
C X
C X
C X
C Y
C X
A X
A X
A Z
A X
A X
C Z
C Z
C Z
A X
C X
C X
C Y
A X
C Z
A Z
C X
A X
C X
C Z
A X
A X
C Z
C X
C Y
A X
C Y
C Z
A Z
C X
A X
A X
A X
A Z
C X
C X
A X
A X
B X
C Y
C X
C Z
C Y
A X
C X
C Z
C Y
C Y
C X
C Y
B X
C X
C Y
A X
C Z
C Y
A X
A X
C X
A X
A X
A Z
C X
A Z
C X
C X
C X
C Y
A Z
C Z
A X
B X
C X
A X
A Z
C X
C Z
B X
C X
A X
A Y
A Z
C Z
C Z
C X
A Z
C Z
C Z
C X
C Y
C Z
C Z
C X
A Z
C Z
C X
A X
A X
A X
A Z
A Z
C Z
A X
C X
A X
C X
C Z
C X
C X
A X
C X
B Y
A X
B X
A X
C X
C Z
A X
C X
A X
A X
C Z
C X
C X
C X
C Z
A X
B X
A Z
C X
B Z
C Z
B Z
C Z
A Y
A X
B Y
A X
A X
C Z
C X
A X
B X
C X
B Z
C Z
A X
A X
A X
A X
A X
A X
A X
C Y
A Y
C X
C Z
C X
C Y
C Z
C X
A X
C Z
A X
A Z
C X
C Z
C Y
A X
C X
C Z
A X
A Y
C X
A X
C Y
A Y
C Y
A Z
C Z
A X
C Z
C Z
A X
C Z
A Z
C X
C Z
B X
C X
A X
A Y
A X
C Z
A Y
A Y
C X
B Z
C X
A X
A X
A X
A X
A X
C X
C Z
A Z
A X
C Y
C Z
A X
A Z
B Z
A Z
A X
C Y
C X
C X
C X
B Z
A X
C Y
A X
A X
A X
C Y
A X
B X
C X
C X
C Z
C Z
A X
C Y
A Z
A X
C X
A X
C X
C X
C Z
A X
C Y
C Y
A X
A Z
A Z
A X
C X
A X
C Y
B X
A X
C Y
C X
B X
C Z
A X
A X
C X
C Z
C Z
C X
C Y
C Y
A X
C Z
C X
C Y
C X
A X
A X
A X
B Y
C X
A X
C X
C X
A X
C Z
C Z
C X
B X
A Y
A Y
C Z
C Y
C X
C Y
B Y
A Z
C X
A X
A Z
A Y
C X
A Z
C Z
C Y
C Z
A X
C X
C Z
C X
C X
A X
C Z
A Z
C X
C Y
C X
A X
A Z
C Z
C X
A Z
B Z
C Y
B X
A Y
B X
B Y
A Y
A X
A Z
C X
C X
A X
C X
C Z
C Y
A Z
A X
C Z
A X
A X
A Z
A X
A X
C Z
A X
C X
A X
A X
A Y
C Z
A X
C Z
A Z
A X
A X
A Y
C Z
A Z
A Z
A X
C X
A X
A X
C Z
C X
C X
C X
C Y
C X
A X
B X
B X
C Z
C Z
C Z
C X
B Y
A Y
B X
B X
C X
C X
C X
A X
C Y
A Z
A X
C X
A X
A X
A X
B X
C Y
A X
C Z
A X
C X
A X
C Z
C X
A X
A Z
A X
A X
A X
C Z
A Y
B X
A X
B X
C Z
A X
A X
C Y
C Z
C Z
A X
C X
A X
A X
A X
A X
A X
C X
A X
C Y
C X
B X
A X
C Y
C Z
A X
A X
C X
A Z
A X
A Z
A X
C Z
A X
B X
C X
A X
C X
A X
C Y
C Z
A X
C Z
C Z
C X
B X
C Z
A X
A Z
C X
A X
C X
C Z
C Z
C Y
C Z
A X
C X
C Z
A Z
C Z
A Z
C X
C Z
A X
A X
B X
A Z
B X
C Y
C Z
C X
A X
C Z
C Z
C Z
B X
C X
C X
A Z
A X
C Y
A X
B Z
B X
A X
C Y
B X
C Z
C Y
C Z
A X
C Y
A X
A X
A Z
A X
A X
C X
C Y
C Z
C Z
C Y
B Y
C Z
A X
A X
C X
C X
C X
A X
C Y
A X
A Y
A X
C X
C Y
C Y
B X
A X
C Z
A X
C Z
C X
A X
A Z
C X
C Y
A X
A Z
A Z
C Y
A X
C Z
C Z
A X
A Z
B X
A X
C Y
C X
B Z
C Z
A X
C Y
A X
C Y
A Z
A X
C X
C Y
B X
A X
B X
A X
A X
B Y
A X
A X
C X
A X
C X
A X
A X
C Y
A X
C Z
C Z
A Z
C X
A Z
C Y
C Z
C X
A X
C Z
A X
C Y
A X
A Z
C Y
C X
C X
C Z
B X
A X
A X
A Z
C Z
C X
A X
C X
A Z
A Z
A X
C X
C Z
B Z
C X
C X
A X
A X
A X
C Y
C X
A X
C Z
A X
B Y
A X
A X
C X
B X
A X
C X
B Y
A Z
C X
C X
A X
A X
A X
C Y
C X
A Z
C X
A X
C Z
C Z
C Y
A X
A X
B Z
C Y
C Z
A X
C Z
A X
C Z
B X
A Z
A Z
C X
A X
C Z
C X
A X
A X
C X
C Z
C X
A X
C X
A X
A X
C Z
C X
A X
A X
A X
A X
C Y
A X
C X
C Z
C Y
C Z
C X
C Z
A X
C X
C Z
C Z
C Z
B X
C X
A Z
A Z
A X
C X
C Z
B Z
C Z
C Z
A Y
A Y
C X
A X
A X
A Z
A X
A Y
A Z
A X
A X
C Z
C Z
A Y
A X
B X
A X
A X
C X
A X
A Z
C Z
C X
C Z
C X
A Z
B X
A X
A X
C X
B Y
A X
C Z
A X
A Y
A Z
A X
C Z
A Z
B X
C Z
C Y
A X
C Y
A X
C X
C X
C X
A X
C X
C Z
C Y
A Z
C X
C Y
A X
A X
C Z
A X
C X
C X
A X
A X
A Z
C X
C Z
A Z
A Z
A X
B X
B X
A X
C Z
A Z
C Z
C X
C Z
B X
C X
C X
B Z
A X
A Z
B Z
B Z
C X
A Z
A X
A Y
C X
A X
C Z
A X
C X
A X
C Z
C X
C X
C X
C Z
B Z
A X
A X
A X
C Z
A X
A X
C X
B Z
C X
C X
A X
B Y
C X
C X
C X
A X
C X
A Z
C Y
B Z
A X
C Z
C Z
C Z
C Z
A Y
C Z
A X
A X
C X
C X
A X
C X
A Z
A X
C Z
C X
A X
C Z
C X
C Z
B Z
A X
C Y
C Z
A X
A X
C Y
A Z
C X
C Z
C Z
C Y
C Z
A X
C X
A X
A X
C X
A X
C Z
A X
A Z
A X
C Z
C Z
B X
C Z
A Y
A Z
A X
C Y
C X
C Y
A X
A Z
A X
A X
A X
C X
B X
C X
A Y
A Y
C Z
A X
A X
A Z
C X
A X
A Z
C Z
C Z
C X
C X
A X
A X
C Z
A X
C Z
C Z
B Z
A X
A X
C Z
A X
C Z
C Y
C X
A X
A Z
A X
A X
A X
C Y
C Z
A X
C Y
C Z
C Z
A X
A Z
A X
C Z
C Y
C Z
C Y
A X
C X
A X
A X
A X
C X
A X
A X
C Y
C X
A X
C X
A Z
A X
C Z
C X
B Z
C Z
A X
C Y
C Z
C X
A X
A X
B Y
A X
B X
C X
A X
A X
C X
C Y
A X
A X
A X
C X
C Z
A X
B X
C Y
C Y
A X
C Y
C X
A X
C X
A X
A X
A X
C X
C X
B Z
A X
C X
A X
C Z
A X
C Y
A X
C Z
C Z
C Y
A X
A Z
A X
C X
C Z
A X
C Z
C X
C Z
A X
C Y
A X
C Z
C Y
C X
A X
A Y
B X
A X
A Y
A X
A X
A X
A X
C Z
C Y
B X
A X
C X
A X
C X
A X
A Y
A X
C Z
A Z
C X
A X
A X
A X
C Z
C Z
C Y
C X
A X
C X
C Y
A Z
C X
A X
C Y
C Z
C Z
A X
A X
C Z
C Z
C X
C X
C X
C Z
C Y
C X
B Z
C Z
A X
C Z
A X
A X
C X
C X
C Y
A Y
A X
C X
A X
C X
C X
A X
A X
A Y
C Z
C Y
A X
A X
A X
A X
C X
C Y
A X
A Y
A X
A X
C X
A Z
C Z
C Z
C Z
C Z
A X
C Z
C X
C X
B X
C X
C Z
C X
A Z
C Z
B X
B Z
C Y
A X
B X
A X
A X
A X
C X
C X
A X
C Y
A X
C Y
B Y
C Y
A X
C X
C Y
C Z
C X
A X
A X
C X
B X
C Z
C X
A X
A Z
A X
C Z
C X
B X
A Z
A X
B Z
C Z
A X
C Y
A X
C Z
B X
C X
C X
C Y
A X
A X
A X
C Z
C X
B Z
C Z
B X
B Z
A X
A Z
A X
C X
C X
C Z
C X
C X
B Y
A X
A X
A X
A X
A X
A X
C X
A Z
A Z
C X
C X
A X
B Y
C X
C X
A Z
A X
B X
A X
C X
C Z
C Y
C Z
C Z
B X
C Y
C X
A X
A X
C X
C Y
C Z
A X
C X
A X
B Z
A X
C Y
C X
C X
A X
C Z
C Z
A X
C X
C X
C X
A X
A Z
C X
A X
C Z
A X
C Z
C Z
A X
C Z
A Z
C X
A X
A X
C Z
A X
A X
A X
C X
A X
A X
C Z
C X
C Y
B X
C X
C Y
C X
A X
A Y
A X
A X
A Z
C Z
A X
C X
C X
C X
C X
B Z
A X
B Y
C X
C X
C Y
C X
C Z
C Z
A X
C X
A X
C X
A Z
C Y
B X
A X
C X
C Z
C Y
B X
C Y
A X
C X
A X
A X
A Z
A X
B X
C X
A X
A Y
A X
C X
C Z
A Z
A X
A X
C Z
A X
C X
A X
C X
A X
C Z
C Y
A X
C X
A X
C Z
A Y
B X
C Z
C Z
A X
A X
A Y
A Z
A X
C X
C Y
C Z
A X
C Z
C Z
B X
A X
A X
A X
C Y
A Z
C Z
C Z
A Z
A X
A X
B X
C X
B X
A X
C Z
A Y
C Z
A X
C X
C Z
C X
A X
C X
A X
A X
C Z
A Z
C X
C X
B Z
C Z
A Y
A X
C X
C Z
A X
C Z
C X
C Y
C X
A Y
C Z
C Z
A X
A X
A X
C Z
C Y
C Z
A X
C Z
A X
C X
A X
A Y
A X
A Z
A X
A Y
A X
A X
A X
A Z
A X
C Z
C Z
C Z
C X
A X
A Z
C X
C Z
A X
C X
C Y
A X
C X
A X
C Y
A X
A X
C Z
C X
A X
C Z
A X
A X
C Z
C X
C Z
C Y
A Y
C Z
C X
B X
A X
A X
A Z
A X
A X
C X
C Z
A X
C X
A Z
B X
A Z
C Z
C Z
A X
B X
A X
A Y
A X
A X
C Z
A X
C Y
C Z
C Y
A X
A X
A X
A X
B Z
B Z
A X
C X
C X
A X
C X
C Y
B X
C Y
B X
A X
B Z
C Z
A X
A X
C X
A Z
A X
C X
C Z
A X
C X
C X
C X
C Z
C Y
A X
A X
A X
C Z
C Z
C X
C Z
B X
A X
C Z
C Z
C Y
A X
A X
C Z
A Z
C X
C Z
C Y
C Z
C Z
A X
C Z
C Y
C X
B X
C Z
A Y
C Z
C X
C X
A X
A X
A X
C Z
B Z
A X
A Z
B X
A Z
A Z
C Z
C Z
C X
C Y
C Y
C Z
A Z
C Y
C X
C Y
A Z
B Y
A X
C Y
A X
A X
A Z
C Z
C Z
C Z
C X
C X
C Z
C Z
A X
C X
A Y
A Y
A X
B Z
A X
C X
A X
C X
A X
A X
A X
C X
C Y
C Z
C Z
A X
C Z
A X
C X
C X
C Z
A X
A X
A X
C X
C Z
A X
A X
A X
A X
C Z
B Y
A X
A X
A X
C Y
A X
A X
A Z
C X
A Z
C Z
A Z
C Z
A X
A Z
A X
A X
A X
A X
A Y
C X
C Z
A X
A Z
C X
A Y
C Z
C Y
A X
B X
A Y
C X
A X
C Z
C X
A X
C Z
C X
A X
C X
C X
C Z
A X
A Y
C Y
C Y
A X
A X
A X
A X
A X
C X
A Y
C Z
C Z
C Y
A X
C Z
C Z
A X
C X
C X
C Y
A X
A X
C X
C X
A Z
A X
C Z
A X
C Z
A X
B X
A Z
B X
C Z
C Z
C X
A X
A Z
C X
A X
A X
C X
C Y
C Z
C X
C X
A X
C Z
C Z
C X
B X
C Z
A X
B X
A Z
C X
A Z
A X
A X
C X
A X
C Z
A X
A X
C Z
C Z
C Z
A X
C X
A Y
C Y
A X
A X
A X
A X
C X
C Z
A X
A X
A X
C X
C Z
A X
C Z
A X
A Y
C Y
C X
C X
A X
A X
A X
A Z
A X
C Z
A X
C Y
B Z
B X
A X
C X
C Z
A X
A X
C Z
B X
B X
C X
C Y
C X
B X
A X
A X
C X
A Z
A X
C X
C X
C X
A X
C Z
A Y
A X
C Y
A X
A X
C Z
A Z
A Z
C X
C X
C Y
C Y
C X
C X
C Z
A X
C X
A X
A X
A X
A X
C Y
C Z
A X
C Z
C X
C X
A X
A X
A Z
C Y
C X
A X
C X
A X
A Z
A X
A Z
A X
A X
A X
C X
A Z
A X
C X
C Y
A Z
A X
B X
C X
C Y
C Z
C X
C Y
C X
C X
A X
C Y
B X
A Z
C Z
B Z
C X
A X
C Z
A X
C X
A X
A X
A X
C Z
C Y
B Z
A X
C X
A Y
C Z
A X
A X
A Z
A Z
A Y
C X
C Z
C X
A Z
C X
C Z
C X
B X
A X
C Z
A X
A X
C Z
A Z
C X
C Y
C X
C X
C Y
A X
A X
C X
A Z
C X
C Z
C X
A Z
A X
A Y
A X
A X
B Z
C Z
C Z
A Y
C X
A X
B X
A X
A Z
C Y
C Z
C X
C Z
C Z
C X
C X
A X
C X
A Z
C Z
C Z
C Z
C Y
C X
A X
C Z
A Z
C Z
C Z
A X
C X
A X
C Z
A X
B X
A X
C Z
C X
C X
A X
C Y
A X
C Z
B X
C Z
C X
A X
C X
C X
C Z
A X
A X
C Z
C Z
C X
C Z
C X
A X
A X
C Z
C X
C X
A X
C Z
A Z
C Z
B X
C X
C Y
C X
C Z
A X
B X
C Z
A X
C Z
A Z
C Z
C X
A X
C Z
C Z
A X
A X
C Z
A X
C Y
A Z
C X
C Z
A X
B Z
B X
A X
A Z
A X
A Z
A X
A X
C Y
A X
C Y
C X
C Y
C Y
C X
A X
A Z
A Z
C X
C Z
B X
A X
C Z
A Z
C X
C Z
C Y
A X
A X
C Z
C Z
C Y
A Z
C Z
C Y
A X
C X
A X
C X
C Z
A Z
B Z
A X
A X
A X
C X
C X
A Y
A Z
C X
C Z
A X
B X
A X
A X
C X
C X
B X
A X
B X
C Z
A Z
C Z
A X
C X
C Z
C Y
A Y
A Z
C Y
C Z
A X
A X
C X
A X
C Z
C Y
A X
C Z
A X
A X
B X
A X
A Z
C X
C Y
A Z
C Z
A X
A Z
C X
C Z
C X
C Z
A X
A X
A X
C X
A X
C Z
C X
C X
C X
C X
A X
C X
B Y
A Z
A Z
C Z
C X
A Y
C X
A X
C X
C Z
B Z
C Z
B X
A X
A Z
A X
C X
A X
C Z
C Z
C Z
A X
C Z
A X
C Z
C X
C X
C X
B X
A X
A X
B X
C X
A X
C Z
A Z
C Y
C Z
A Z
B Z
A Z
C Z
C Z
C X
A X
A X
C X
C X
C Y
A Y
A Z
A X
C Z
C Z
C Y
C X
A X
A X
C Y
A Z
C X
C X
C X
A X
C X
C X
A Z
C X
C Y
A Z
C Z
C Y
A Z
C Z
A X
C X
C Y
C X
C X
C Z
C X
C X
C Z
C X
A X
B Y
A X
C X
B Z
C Z
C X
C Z
C Z
A Y
A Y
A X
A Z
C Z
A X
C X
C Z
A Z
A X
A X
A X
B X
A X
B Z
C X
A X
C X
A Z
C X
A X
C Y
A X
C X
C Y
A Z
C Z
C Z
B X
A X
C X
A X
C X
C Y
A X
C Y
A X
B X
C Y
A X
A X
C X
A X
B Y
C Y
C Y
C Y
C X
A Y
C X
C Z
A X
A Z
A X
A X
C Z
A Z
A Z
C X
A X
A X
B X
C X
C Y
C X
B Z
A X
C X
C Y
C X
C Z
C Y
A X
C X
A X
A X
A Y
C Z
C Z
C Z
A X
A X
A Z
A X
A X
A Z
A X
C Y
C Y
A X
A X
A Z
C X
A X
C Z
A Z
C Z
C Y
C Z
A X
C Z
C X
C Z
A X
C Y
C Y
B X
A X
A Z
C X
A X
A X
C Z
A X
C X
C Z
A Z
A Y
A Y
A X
C X
A Y
A X
A X
B Z
A X
C X
C X
C X
A X
C X
C Z
C Z
A Z
C X
C X
C Y
A X
C X
A X
A X
C Y
C X
A X
C Y
C X
C X
A Y
C Z
C X
C X
C Y
C Z
A Y
A X
C Z
C Z
C Z
C X
C Z
A X
A X
A Z
A X
C Y
C X
C Z
A X
A X
C Z
A X
C X
A X
A Y
C X";

			return ParseData(data);
		}

		private static List<Round> ParseData(string data)
		{
			var rows = data.Split("\r\n");

			var dataToReturn = new List<Round>();

			foreach (string row in rows)
			{
				var characters = row.ToCharArray();
				var round = new Round()
				{
					TheirChoice = characters[0],
					MyChoice = characters[2]
				};
				dataToReturn.Add(round);
			}

			return dataToReturn;
		}

		class Round
		{
			public char TheirChoice;
			public char MyChoice;

			public int MyScore
			{
				get
				{
					if (TheirChoice == Rock)
					{
						if (MyChoice == MyRockChoice)
						{
							// Draw
							return RockScore + DrawScore;
						}
						else if (MyChoice == MyPaperChoice)
						{
							// Win
							return PaperScore + WinScore;
						}
						else
						{
							// Lose
							return ScissorsScore;
						}
					}
					else if (TheirChoice == Paper)
					{
						if (MyChoice == MyRockChoice)
						{
							// Lose
							return RockScore;
						}
						else if (MyChoice == MyPaperChoice)
						{
							// Draw
							return PaperScore + DrawScore;
						}
						else
						{
							// Win
							return ScissorsScore + WinScore;
						}
					}
					else if (TheirChoice == Scissors)
					{
						if (MyChoice == MyRockChoice)
						{
							// Win
							return RockScore + WinScore;
						}
						else if (MyChoice == MyPaperChoice)
						{
							// Lose
							return PaperScore;
						}
						else
						{
							// Draw
							return ScissorsScore + DrawScore;
						}
					}

					return 0;
				}
			}
		}

		const char Rock = 'A';
		const char Paper = 'B';
		const char Scissors = 'C';

		const char MyRockChoice = 'X';
		const char MyPaperChoice = 'Y';
		const char MyScissorsChoice = 'Z';

		const int RockScore = 1;
		const int PaperScore = 2;
		const int ScissorsScore = 3;

		const int DrawScore = 3;
		const int WinScore = 6;
	}
}
