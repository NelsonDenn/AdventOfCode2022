using System;
using System.Collections.Generic;

namespace AdventOfCode2022
{
	public class Day9
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			//var result = RunPart1(data);
			var result = RunPart2(data);

			return result;
		}

		private static int RunPart2(List<Movement> movements)
		{
			return GetTailUniquePositionsCount(movements, 9);
			// Test: 36
			// Actual: 2627
		}

		private static int RunPart1(List<Movement> movements)
		{
			return GetTailUniquePositionsCount(movements, 1);
			// Test: 13
			// Actual: 6357
		}

		private static int RunPart1_BeforePart2(List<Movement> movements)
		{
			// Let's start the head at (0, 0)
			var head = new Knot()
			{
				X = 0,
				Y = 0
			};

			// The tail starts in the same position as the head
			var tail = new Knot()
			{
				X = head.X,
				Y = head.Y
			};

			// Coordinates that the tail has been to
			var visitedCoordinates = new HashSet<Coordinate>
			{
				tail.GetCoordinate() // Add the tail's starting coordinate
			};

			foreach (var movement in movements)
			{
				// Move the head
				if (movement.IsRight)
				{
					head.X += movement.Distance;

					// Keep the tail within one column of the head
					while (head.X - tail.X > 1)
					{
						tail.Y = head.Y; // Move diagonally on the first move
						tail.X += 1;
						visitedCoordinates.Add(tail.GetCoordinate());
					}
				}
				else if (movement.IsLeft)
				{
					head.X -= movement.Distance;

					// Keep the tail within one column of the head
					while (head.X - tail.X < -1)
					{
						tail.Y = head.Y; // Move diagonally on the first move
						tail.X -= 1;
						visitedCoordinates.Add(tail.GetCoordinate());
					}
				}
				else if (movement.IsUp)
				{
					head.Y += movement.Distance;

					// Keep the tail within one row of the head
					while (head.Y - tail.Y > 1)
					{
						tail.X = head.X; // Move diagonally on the first move
						tail.Y += 1;
						visitedCoordinates.Add(tail.GetCoordinate());
					}
				}
				else if (movement.IsDown)
				{
					head.Y -= movement.Distance;

					// Keep the tail within one row of the head
					while (head.Y - tail.Y < -1)
					{
						tail.X = head.X; // Move diagonally on the first move
						tail.Y -= 1;
						visitedCoordinates.Add(tail.GetCoordinate());
					}
				}
			}

			return visitedCoordinates.Count;
			// Test: 13
			// Actual: 6357
		}

		private static int GetTailUniquePositionsCount(List<Movement> movements, int numFollowingKnots)
		{
			// List of knots following the head knot (excludes the head knot)
			var followingKnots = new List<Knot>();

			// Let's start the head at (0, 0)
			var head = new Knot()
			{
				X = 0,
				Y = 0
			};

			var previousKnot = head;

			// For each knot after the head knot, start them off where the head is
			for (int i = 0; i < numFollowingKnots; i++)
			{
				var knot = new Knot
				{
					X = head.X,
					Y = head.Y,
					KnotToFollow = previousKnot
				};

				previousKnot = knot;
				followingKnots.Add(knot);
			}

			// The final knot is the tail
			var tail = previousKnot;

			// Coordinates that the tail has been to
			var visitedCoordinates = new HashSet<Coordinate>
			{
				tail.GetCoordinate() // Add the tail's starting coordinate
			};

			foreach (var movement in movements)
			{
				for (int i = 0; i < movement.Distance; i++)
				{
					// Move the head knot
					if (movement.IsRight)
					{
						head.X += 1;
					}
					else if (movement.IsLeft)
					{
						head.X -= 1;
					}
					else if (movement.IsUp)
					{
						head.Y += 1;
					}
					else if (movement.IsDown)
					{
						head.Y -= 1;
					}

					// Move each following knot if the knot it follows is out of range
					foreach (var knot in followingKnots)
					{
						// Whether the knot moved horizontally or vertically
						bool movedX = false;
						bool movedY = false;

						// If the knot is out of range in one direction, close the distance in that direction by one
						if (knot.KnotToFollow.X - knot.X > 1)
						{
							knot.X += 1;
							movedX = true;
						}
						else if (knot.KnotToFollow.X - knot.X < -1)
						{
							knot.X -= 1;
							movedX = true;
						}
						else if (knot.KnotToFollow.Y - knot.Y > 1)
						{
							knot.Y += 1;
							movedY = true;
						}
						else if (knot.KnotToFollow.Y - knot.Y < -1)
						{
							knot.Y -= 1;
							movedY = true;
						}

						// If the knot moved horizontally, also move vertically if not already aligned to the same row as the knot to follow
						if (movedX)
						{
							if (knot.KnotToFollow.Y > knot.Y)
							{
								knot.Y++;
							}
							else if (knot.KnotToFollow.Y < knot.Y)
							{
								knot.Y--;
							}
						}

						// If the knot moved vertically, also move horizontally if not already aligned to the same column as the knot to follow
						if (movedY)
						{
							if (knot.KnotToFollow.X > knot.X)
							{
								knot.X++;
							}
							else if (knot.KnotToFollow.X < knot.X)
							{
								knot.X--;
							}
						}
					}

					// Save the tail knot's position
					visitedCoordinates.Add(tail.GetCoordinate());
				}
			}

			return visitedCoordinates.Count;
		}

		private static List<Movement> GetTestData()
		{
//			string data =
//@"R 4
//U 4
//L 3
//D 1
//R 4
//D 1
//L 5
//R 2";
			string data =
@"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

			return ParseData(data);
		}

		private static List<Movement> GetData()
		{
			string data =
@"L 1
D 2
L 2
U 2
D 2
U 2
D 1
R 1
U 1
R 1
L 2
R 1
U 1
R 2
L 1
D 2
R 2
L 2
D 1
U 2
R 2
L 1
U 2
D 2
U 2
D 2
L 1
R 2
L 1
R 1
L 1
D 2
L 1
U 1
L 2
U 1
L 2
R 2
D 1
L 2
R 1
D 1
U 1
L 1
U 2
D 2
L 1
U 1
R 2
D 1
L 1
U 1
R 2
D 1
R 2
D 2
U 2
R 2
U 2
R 1
L 1
R 1
L 1
D 1
U 1
R 2
D 2
L 2
U 1
D 1
R 1
U 1
L 1
D 2
U 2
D 1
L 2
U 1
D 1
L 2
U 1
R 2
D 1
L 1
R 1
L 1
D 1
L 1
R 1
L 2
R 2
D 1
R 2
D 1
L 1
D 1
L 2
R 2
U 2
L 2
R 1
U 2
R 2
D 1
U 1
L 2
D 2
U 2
L 2
R 1
U 2
D 2
L 3
D 1
R 2
D 2
U 2
L 3
U 1
D 1
R 1
D 2
L 2
D 2
R 3
D 1
U 1
L 3
R 2
L 1
U 2
D 1
R 1
U 1
D 1
L 3
U 3
R 3
U 1
L 1
U 1
R 3
U 3
L 2
R 1
L 2
U 1
R 3
L 3
U 1
D 2
U 3
R 3
D 1
L 1
R 2
L 1
U 1
R 2
D 1
U 1
D 1
R 3
D 3
R 1
U 3
D 2
R 2
L 2
U 1
R 2
U 2
D 2
R 1
U 1
R 3
D 2
U 1
D 2
L 3
R 3
U 3
R 3
L 3
U 3
L 2
R 1
U 2
D 2
R 2
L 1
U 3
L 2
U 1
R 1
U 3
D 3
R 2
L 1
U 3
D 3
L 2
R 2
D 3
L 3
R 2
D 3
U 3
L 1
D 1
L 2
R 3
U 2
D 2
U 3
R 2
L 2
U 3
D 1
L 3
U 1
L 1
U 1
D 4
L 3
U 3
D 2
R 3
U 4
D 2
U 1
R 3
L 4
R 1
D 2
R 1
D 4
U 1
D 3
U 4
L 3
D 4
R 1
U 3
R 1
U 2
L 2
R 1
D 2
U 1
D 4
R 1
D 1
R 4
L 2
D 2
R 2
L 3
U 1
L 4
D 1
U 2
L 4
D 3
R 4
L 4
D 3
L 4
R 1
D 3
U 1
L 1
D 4
R 1
U 4
L 2
U 3
R 4
L 4
D 4
U 3
R 2
U 2
L 2
R 1
U 2
L 3
U 4
D 4
L 4
R 4
L 3
D 4
U 3
L 3
U 2
L 3
R 3
D 4
R 4
L 1
U 1
R 1
D 2
U 3
D 4
L 4
D 3
U 4
L 1
R 4
U 2
D 4
R 2
L 3
R 2
U 4
R 3
U 1
L 2
U 4
R 4
L 1
D 2
R 1
U 4
R 4
U 1
L 1
U 1
R 1
L 4
U 2
R 2
L 5
R 5
L 1
R 4
U 5
L 4
R 3
D 1
R 4
L 5
R 2
L 1
R 5
D 4
R 4
L 1
U 2
D 4
U 4
L 5
D 3
U 2
D 5
L 5
D 3
U 1
R 5
U 4
R 1
U 4
D 1
R 2
D 2
U 3
D 3
R 5
U 2
D 2
U 5
L 4
R 1
D 4
U 3
L 4
D 1
L 4
D 5
L 5
U 3
L 2
R 4
L 5
U 2
L 1
D 5
U 2
R 5
L 1
D 2
R 3
D 5
U 2
D 3
R 4
L 3
R 5
L 5
U 3
L 2
D 5
U 5
R 4
U 5
D 5
L 1
D 4
U 4
L 4
U 3
D 3
L 2
R 1
L 5
D 3
L 1
U 4
D 5
L 5
R 1
U 4
D 5
U 3
D 5
L 1
R 5
L 1
D 3
L 2
D 3
L 2
D 2
U 3
D 2
L 3
R 2
U 3
L 4
D 4
U 4
D 4
R 2
D 3
U 4
D 6
L 2
D 4
U 4
L 2
D 4
U 1
D 1
U 1
R 3
U 3
L 1
U 6
R 5
L 1
U 5
L 2
D 1
U 3
D 2
U 3
D 2
R 2
U 6
L 5
D 4
L 2
D 6
U 5
D 3
R 2
L 6
U 5
R 2
U 3
L 2
R 3
D 6
U 5
D 1
R 5
L 3
U 5
R 3
L 5
R 3
L 4
U 6
D 3
U 6
L 3
R 5
U 6
L 1
D 5
L 3
R 5
L 3
D 4
R 4
D 6
L 1
R 4
U 4
D 4
U 5
D 6
L 6
R 2
L 4
D 3
R 4
U 5
L 5
D 4
R 6
D 6
R 2
U 4
D 5
L 6
R 3
U 6
R 6
D 3
R 6
L 1
D 3
U 2
R 2
D 2
U 6
D 4
U 5
D 1
L 6
U 2
D 4
R 2
D 3
U 4
L 1
D 2
L 2
U 2
D 3
L 5
U 6
L 5
R 2
L 2
D 7
R 2
D 6
U 5
L 4
U 2
R 1
D 1
L 7
D 4
R 2
D 6
U 4
L 1
U 2
R 4
D 4
R 3
L 3
U 4
R 3
L 2
U 6
D 1
L 3
U 6
D 2
R 2
L 5
U 7
R 5
L 2
D 5
U 4
D 3
L 1
D 5
L 5
U 4
R 1
D 4
L 2
D 7
L 7
D 3
U 4
L 5
D 3
L 7
R 3
D 4
L 5
R 7
L 2
U 4
R 5
L 2
R 2
D 1
R 1
U 3
R 4
L 1
U 4
L 2
R 1
U 4
D 7
U 2
D 2
R 2
L 3
D 3
R 1
L 2
R 2
U 6
R 1
U 1
R 7
U 3
D 2
L 5
U 7
D 7
U 3
L 2
U 7
R 3
D 3
U 2
D 5
R 6
D 5
U 1
R 7
D 6
R 6
U 3
L 1
R 6
D 6
R 1
L 6
R 6
D 7
R 3
L 5
R 5
D 4
U 7
L 8
D 2
U 3
D 5
L 4
D 2
R 1
L 5
D 8
U 6
R 2
D 2
L 2
R 3
L 2
R 3
L 6
R 4
U 6
D 7
R 6
L 7
U 5
L 6
R 6
D 5
R 5
D 5
L 4
D 4
U 7
L 3
U 7
L 4
U 8
R 7
D 4
L 2
D 6
R 2
U 3
L 6
D 1
R 4
U 8
D 3
U 2
L 6
R 1
L 2
U 4
L 1
R 1
U 8
D 2
L 5
D 6
L 5
R 2
L 5
U 6
D 1
L 7
U 4
D 5
L 3
R 5
D 5
R 3
L 2
D 3
L 3
D 5
L 8
R 7
D 5
L 2
R 3
U 5
L 1
D 6
R 5
L 6
R 1
U 1
R 4
L 6
U 1
D 6
L 7
D 8
R 1
L 5
D 2
R 2
D 2
R 7
L 7
U 7
R 4
D 5
R 4
U 8
R 3
D 6
R 1
D 1
U 8
D 4
U 7
R 3
L 9
D 3
L 9
U 1
R 5
D 4
U 7
L 4
U 4
L 7
R 1
D 8
R 4
L 4
U 4
L 8
U 5
L 7
D 4
L 9
D 2
U 3
L 3
D 5
U 1
L 1
R 6
D 2
L 1
D 2
L 3
U 4
L 6
U 3
R 5
L 3
D 7
U 6
L 6
D 9
L 5
U 1
R 9
U 4
R 1
L 5
U 9
R 6
D 7
U 9
L 4
U 6
D 7
U 8
L 1
U 8
R 4
L 2
R 3
D 1
U 6
D 9
L 8
U 8
L 9
D 4
R 8
L 1
D 9
L 1
R 7
D 8
U 6
D 8
R 9
U 2
R 3
U 7
R 8
D 5
L 6
U 3
D 8
R 2
U 5
L 5
R 9
D 4
U 1
D 6
L 1
U 3
R 4
L 5
R 1
L 9
R 4
D 1
L 9
D 4
U 7
R 2
D 8
L 9
R 5
L 5
D 7
L 6
D 6
L 10
U 8
L 6
D 6
R 9
D 10
L 5
R 8
D 2
U 7
R 4
U 5
R 3
L 6
D 10
L 7
D 7
U 9
L 6
R 10
U 2
L 3
U 10
D 3
R 10
D 4
L 9
R 2
L 8
R 4
D 7
R 10
D 2
U 9
D 2
R 7
D 10
R 10
D 7
R 5
D 5
R 7
U 7
D 10
U 5
D 3
R 5
U 10
L 3
D 7
R 7
U 7
L 4
U 3
R 3
D 7
L 7
U 2
D 4
L 5
R 3
U 5
D 6
R 2
D 4
U 2
D 6
U 7
D 2
L 2
U 6
D 9
U 3
D 1
U 5
D 2
R 3
L 6
D 8
L 7
R 5
L 4
U 6
L 9
R 5
D 5
U 4
D 6
U 5
D 1
U 8
D 2
L 4
R 5
L 3
U 10
L 1
R 10
U 4
R 5
D 4
R 8
U 5
L 2
R 10
L 10
U 4
D 3
U 3
R 4
D 8
R 9
L 5
R 5
D 10
L 11
D 10
L 6
R 11
L 2
R 7
U 6
R 9
L 10
D 3
R 6
D 7
L 10
U 7
D 2
L 4
D 1
L 8
D 7
L 10
R 2
U 7
L 9
R 1
D 1
L 11
U 11
D 3
R 4
D 4
U 11
L 6
D 2
L 7
D 6
U 8
L 1
R 11
L 1
U 3
D 8
L 4
D 11
U 8
R 4
D 1
R 5
D 9
L 2
D 1
U 4
R 7
L 6
D 5
U 1
L 8
U 6
R 10
D 11
U 3
D 1
U 7
D 9
R 8
D 4
R 11
L 5
D 8
L 8
U 1
R 10
U 9
D 7
R 1
D 9
U 1
R 3
D 11
L 9
D 1
R 2
U 5
R 8
U 1
L 8
D 11
U 9
L 8
U 1
R 1
D 1
R 10
D 3
R 8
U 10
D 2
U 6
D 5
R 5
D 10
R 7
L 1
R 10
U 1
D 8
R 7
D 10
R 8
U 2
L 12
D 1
R 6
D 7
U 11
L 8
R 12
D 3
U 12
L 11
D 2
R 3
L 6
U 1
R 2
L 5
R 2
U 8
L 11
R 4
L 2
R 3
D 12
R 4
U 8
R 5
D 6
R 9
D 8
R 7
D 10
R 5
U 4
L 6
D 1
U 6
D 7
R 2
L 2
R 8
L 12
R 7
L 9
U 10
D 2
R 9
L 2
D 10
U 8
R 9
L 7
D 1
L 8
R 4
L 6
D 11
R 2
L 6
D 1
R 6
D 2
R 11
U 9
R 10
U 8
L 4
D 12
L 4
D 7
R 5
L 11
D 2
U 7
D 1
L 10
R 3
L 5
U 12
R 5
L 8
R 6
L 10
D 9
U 8
R 3
D 12
L 4
D 7
U 3
D 11
U 2
D 11
R 7
D 5
U 2
D 8
U 7
R 2
U 5
D 1
R 10
D 7
L 7
R 4
L 1
U 12
L 12
D 9
R 7
U 9
R 5
L 3
R 2
U 11
D 1
L 10
D 11
R 1
D 7
R 7
D 9
R 13
D 7
L 9
D 9
R 13
D 7
R 9
D 10
U 7
R 5
L 1
U 8
D 9
U 10
L 5
U 4
D 9
U 1
L 8
D 3
U 7
D 6
U 2
D 8
L 12
U 2
R 11
L 10
R 9
D 4
L 4
U 2
L 11
U 11
R 6
D 7
L 2
D 6
R 9
L 7
D 6
L 12
U 12
D 5
R 1
L 11
U 12
L 4
D 4
R 12
U 6
R 4
U 6
R 3
L 3
R 10
D 6
R 9
D 12
U 6
D 3
U 13
D 13
U 2
D 10
R 6
L 10
R 10
L 13
U 6
D 12
R 8
D 8
U 6
D 1
L 7
R 7
U 5
R 4
U 2
D 8
L 11
D 13
L 10
U 3
L 8
U 6
D 5
R 8
D 12
U 13
L 10
U 12
R 10
U 9
L 3
D 10
U 3
R 2
L 8
D 4
U 12
L 13
R 14
U 13
R 4
L 7
D 5
L 12
D 4
L 9
R 12
D 10
U 11
R 7
D 3
R 5
D 13
R 11
D 8
R 4
L 3
R 11
L 5
D 7
U 2
D 8
L 12
R 2
D 10
U 11
D 6
L 9
D 8
R 3
D 9
L 9
D 6
R 10
U 5
D 2
R 9
U 5
D 7
L 1
D 14
U 2
R 14
U 11
D 4
L 8
R 8
D 2
L 3
U 4
R 2
U 5
R 6
U 8
R 2
L 4
D 1
L 9
U 13
D 12
R 7
L 6
R 10
U 12
L 14
U 3
L 6
D 12
R 13
L 13
U 5
L 5
R 6
L 11
R 7
L 6
D 14
U 2
D 12
L 2
R 9
U 4
L 14
U 12
R 12
D 3
L 5
R 8
D 10
R 13
D 6
U 9
R 10
L 14
D 4
L 6
D 8
L 12
U 10
L 11
D 3
U 2
R 2
L 13
R 6
L 3
R 14
D 7
R 3
D 14
R 3
L 1
D 15
L 6
U 6
R 3
D 12
U 11
R 10
D 10
L 2
R 1
U 7
D 2
L 7
D 13
R 9
U 6
R 6
D 7
R 3
L 14
R 2
L 10
D 13
U 8
R 3
U 12
R 10
L 12
R 11
U 6
L 13
R 1
D 14
U 13
D 13
U 1
L 10
R 2
D 4
U 1
L 7
U 2
D 9
R 9
U 7
L 9
D 7
U 2
R 1
D 13
U 3
L 1
U 3
D 15
U 6
R 8
L 6
U 6
L 8
D 1
U 4
R 15
L 13
R 3
L 15
D 4
R 10
L 14
U 2
L 3
R 7
L 12
R 15
U 7
D 2
U 15
R 12
D 12
L 6
D 8
R 5
L 14
R 8
U 1
D 5
U 1
L 3
R 2
U 9
L 5
U 8
L 2
U 1
R 5
D 3
L 4
R 15
U 15
L 6
D 12
U 9
D 12
U 9
D 9
L 16
U 10
D 4
R 13
D 4
U 6
L 1
U 6
R 12
L 6
D 8
U 11
D 9
R 16
U 14
L 15
U 15
D 5
R 6
D 16
R 1
D 10
L 5
R 12
L 1
D 9
U 9
D 11
L 9
U 5
R 11
U 16
L 6
D 13
L 3
D 7
U 1
R 11
D 6
R 6
U 7
L 14
R 2
L 6
U 8
L 9
R 3
D 1
L 13
R 8
L 14
U 11
D 13
L 12
U 8
L 6
U 13
D 8
U 8
D 12
R 7
D 16
U 4
D 2
L 6
R 7
U 1
L 12
R 13
D 16
L 3
D 4
R 15
U 5
R 1
D 14
L 1
U 4
L 14
U 15
R 14
L 14
R 16
U 10
L 4
D 6
U 5
L 15
U 8
L 9
R 12
U 7
R 4
L 16
U 10
R 6
U 14
L 3
D 13
L 6
U 12
R 3
L 16
R 16
U 8
R 13
U 7
D 6
R 3
U 16
L 2
D 2
R 17
D 17
R 3
L 11
R 12
L 1
R 17
L 9
U 2
L 16
D 16
L 7
R 2
D 13
U 1
D 5
L 5
U 14
D 13
R 3
U 5
D 9
L 9
U 3
L 2
U 8
L 5
R 13
L 8
U 2
R 6
D 2
L 9
U 15
R 14
U 1
R 11
D 13
U 9
D 17
R 14
D 14
R 7
D 3
L 1
R 15
D 13
U 14
R 3
U 5
D 15
L 10
R 10
L 1
R 16
D 13
L 17
U 9
R 1
L 4
U 4
L 3
R 10
L 14
U 17
R 6
L 17
D 11
R 5
D 5
U 1
L 9
U 7
D 1
U 6
L 13
U 6
D 13
R 17
U 3
D 5
L 15
U 3
L 5
R 12
L 16
U 8
R 7
U 16
R 14
U 17
R 2
U 6
L 5
U 17
D 11
U 3
R 8
D 5
U 16
R 1
U 11
L 15
R 12
L 5
D 13
L 16
R 14
L 15
D 17
L 6
U 9
R 18
D 8
U 6
L 18
U 11
L 3
U 18
D 14
L 17
D 5
L 15
D 5
L 12
D 13
U 9
L 7
R 11
U 12
L 4
R 6
U 7
D 17
U 13
L 10
U 15
R 7
L 2
R 8
L 7
U 1
R 17
L 17
D 4
L 13
R 2
D 5
U 3
L 1
U 2
D 18
L 3
D 17
R 6
U 12
D 11
L 1
R 10
U 9
D 7
R 12
U 11
D 13
L 11
R 14
U 6
R 11
L 13
U 5
D 15
R 12
U 10
R 16
D 16
L 1
U 18
R 8
L 8
R 4
L 18
U 5
R 15
L 15
U 13
D 16
R 4
D 13
R 6
L 13
D 1
U 8
R 13
D 8
L 17
U 12
R 2
U 13
L 6
U 16
L 15
R 17
D 1
R 8
D 9
R 4
U 11
D 11
L 12
R 10
U 2
R 3
D 7
L 2
U 8
R 3
D 4
U 6
L 8
U 9
R 7
D 1
U 12
L 4
U 13
L 11
D 14
U 18
L 17
R 4
L 19
R 1
D 12
U 9
R 11
D 4
U 15
R 7
D 6
L 17
D 4
R 5
U 11
R 9
L 4
U 11
R 10
U 4
D 19
U 18
D 12
R 2
L 15
D 1
R 13
D 3
R 5
D 6
U 11
L 3
D 5
R 2
D 15
R 4
U 19
D 8
L 11
R 17
U 7
D 1
U 2
R 18
L 10
R 4
D 7
U 8
L 1
U 11
L 15
R 2
U 13
R 12
L 8
R 1
U 4
L 12
R 13
D 7
U 5
R 14
U 13
D 13
L 18
U 18
R 14
D 18
U 12
D 11
U 6
R 6
U 5
R 4
D 7
R 1
U 5
R 1
D 1
U 13
D 10
R 19
D 7
U 14
D 7
U 19
R 12
L 2
D 12
U 10
D 17
L 12
U 1
R 8
L 8
D 15
R 9
U 5
R 11
L 10
R 2
L 13
U 10
L 7
U 1";

			return ParseData(data);
		}

		private static List<Movement> ParseData(string data)
		{
			var rows = data.Split("\r\n");

			var dataToReturn = new List<Movement>();

			foreach (string row in rows)
			{
				var parts = row.Split(' ');

				var movement = new Movement
				{
					Direction = parts[0],
					Distance = int.Parse(parts[1])
				};

				dataToReturn.Add(movement);
			}

			return dataToReturn;
		}

		class Movement
		{
			public string Direction { get; set; }
			public int Distance { get; set; }

			public bool IsRight => Direction == "R";
			public bool IsLeft => Direction == "L";
			public bool IsUp => Direction == "U";
			public bool IsDown => Direction == "D";

			public override string ToString()
			{
				return $"{Direction} {Distance}";
			}
		}

		class Knot
		{
			public int X
			{
				get => Coordinate.X;
				set => Coordinate.X = value;
			}
			public int Y
			{
				get => Coordinate.Y;
				set => Coordinate.Y = value;
			}

			public Coordinate Coordinate { get; set; } = new Coordinate();
			public Knot KnotToFollow { get; set; }

			public override string ToString()
			{
				return $"({X}, {Y})";
			}

			public Coordinate GetCoordinate()
			{
				return new Coordinate
				{
					X = X,
					Y = Y
				};
			}
		}

		class Coordinate
		{
			public int X { get; set; }
			public int Y { get; set; }

			public override bool Equals(object obj)
			{
				return Equals(obj as Coordinate);
			}

			public bool Equals(Coordinate other)
			{
				return other != null &&
					X == other.X &&
					Y == other.Y;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(X, Y);
			}
		}
	}
}
