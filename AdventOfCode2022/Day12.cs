using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day12
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			//var result = RunPart1(data);
			var result = RunPart2(data);

			return result;
		}

		private static int RunPart2(char[,] plot)
		{
			int shortestDistance = int.MaxValue;

			int width = plot.GetLength(0);
			int height = plot.GetLength(1);

			// Find the shortest distance for each possible starting point 'a'
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					var elevation = plot[x, y];
					if (elevation == 'S' || elevation == 'a')
					{
						// Return the shortest distance from the starting node to the final node
						var distance = GetShortestDistance(plot, x, y);
						shortestDistance = Math.Min(shortestDistance, distance);
					}
				}
			}

			return shortestDistance;
			// Test: 29
			// Actual: 375
		}

		private static int RunPart1(char[,] plot)
		{
			int width = plot.GetLength(0);
			int height = plot.GetLength(1);

			// Find the starting node
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (plot[x, y] == 'S')
					{
						// Return the shortest distance from the starting node to the final node
						return GetShortestDistance(plot, x, y);
					}
				}
			}

			return 0;
			// Test: 31
			// Actual: 380
		}

		private static int GetShortestDistance(char[,] plot, int startX, int startY)
		{
			int width = plot.GetLength(0);
			int height = plot.GetLength(1);

			// Using Dijkstra's algorithm

			// 1. Mark all nodes unvisited. Create a set of all the unvisited nodes called the unvisited set.
			var nodes = new Node[width, height];

			// 2. Assign to every node a tentative distance value: set it to zero for our initial node and to infinity for all other nodes.
			// The tentative distance of a node v is the length of the shortest path discovered so far between the node v and the starting node.
			// Since initially no path is known to any other vertex than the source itself (which is a path of length zero), all other tentative distances are initially set to infinity.
			Node currentNode = null;
			Node finalNode = null;

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					var elevation = plot[x, y];
					var node = new Node(x, y, elevation);

					//if (elevation == 'S')
					if (x == startX && y == startY)
					{
						// Mark the starting node as the current node
						node.Elevation = 'a';
						node.IsStartingNode = true;
						node.Distance = 0;
						node.HasBeenVisited = true;
						currentNode = node;
					}
					else if (elevation == 'E')
					{
						node.Elevation = 'z';
						node.IsFinalNode = true;
						finalNode = node;
					}

					nodes[x, y] = node;
				}
			}

			// For the current node, consider all of its unvisited neighbors and calculate their tentative distances through the current node.
			// Compare the newly calculated tentative distance to the current assigned value and assign the smaller one.
			// For example, if the current node A is marked with a distance of 6, and the edge connecting it with a neighbor B has length 2,
			// then the distance to B through A will be 6 + 2 = 8.
			// If B was previously marked with a distance greater than 8 then change it to 8. Otherwise, the current value will be kept.
			while (!finalNode.HasBeenVisited && currentNode != null)
			{
				// Gather the coordinates of the nodes directly above, below, to the right, and to the left of the current node
				var nearbyCoordinatesList = new List<KeyValuePair<int, int>>
				{
					new KeyValuePair<int, int>(currentNode.X, currentNode.Y - 1),
					new KeyValuePair<int, int>(currentNode.X, currentNode.Y + 1),
					new KeyValuePair<int, int>(currentNode.X - 1, currentNode.Y),
					new KeyValuePair<int, int>(currentNode.X + 1, currentNode.Y),
				};

				// Check each of the (non-diagonal) surrounding nodes
				foreach (var nearbyCoordinates in nearbyCoordinatesList)
				{
					int x = nearbyCoordinates.Key;
					int y = nearbyCoordinates.Value;

					// Skip out of bounds coordinates
					if (x < 0 || x == width || y < 0 || y == height)
					{
						continue;
					}

					var neighboringNode = nodes[x, y];

					// Skip nodes that have already been visited
					if (neighboringNode.HasBeenVisited)
					{
						continue;
					}

					// Check whether we can visit this node from the current node: destination elevation can be at most one higher
					// than current elevation (going much lower is fine though)
					if (neighboringNode.Elevation - currentNode.Elevation > 1)
					{
						continue;
					}

					// If the node at the current coordinates hasn't been visited yet, potentially update its tentative distance
					//neighboringNode.Distance = Math.Min(neighboringNode.Distance, currentNode.Distance + plot[x, y]);
					neighboringNode.Distance = Math.Min(neighboringNode.Distance, currentNode.Distance + 1);
				}

				// 4. When we are done considering all of the unvisited neighbors of the current node,
				// mark the current node as visited and remove it from the unvisited set. A visited node will never be checked again.
				currentNode.HasBeenVisited = true;

				// 5. If the destination node has been marked visited (when planning a route between two specific nodes)
				// or if the smallest tentative distance among the nodes in the unvisited set is infinity (when planning a complete traversal;
				// occurs when there is no connection between the initial node and remaining unvisited nodes), then stop. The algorithm has finished.

				// 6. Otherwise, select the unvisited node that is marked with the smallest tentative distance, set it as the new current node, and go back to step 3.
				currentNode = nodes
					.Cast<Node>()
					.Where(n => !n.HasBeenVisited && n.Distance < int.MaxValue) // Only consider nodes that are reachable
					.OrderBy(n => n.Distance)
					.FirstOrDefault();
			}

			return finalNode.Distance;
		}

		private static char[,] GetTestData()
		{
			string data =
@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

			return ParseData(data);
		}

		private static char[,] GetData()
		{
			string data =
@"abcccccccaaaaaaaaccccccccccaaaaaaccccccaccaaaaaaaccccccaacccccccccaaaaaaaaaaccccccccccccccccccccccccccccccccaaaaa
abcccccccaaaaaaaaacccccccccaaaaaacccccaaacaaaaaaaaaaaccaacccccccccccaaaaaaccccccccccccccccccccccccccccccccccaaaaa
abcccccccaaaaaaaaaaccccccccaaaaaacaaacaaaaaaaaaaaaaaaaaaccccccccccccaaaaaaccccccccccccccaaacccccccccccccccccaaaaa
abaaacccccccaaaaaaacccccccccaaacccaaaaaaaaaaaaaaaaaaaaaaaaacccccccccaaaaaaccccccccccccccaaacccccccccccccccccaaaaa
abaaaaccccccaaaccccccccccccccccccccaaaaaaaaacaaaacacaaaaaacccccccccaaaaaaaacccccccccccccaaaaccaaacccccccccccaccaa
abaaaaccccccaaccccaaccccccccccccccccaaaaaaacaaaaccccaaaaaccccccccccccccccacccccccccccccccaaaaaaaaacccccccccccccca
abaaaaccccccccccccaaaacccccccccaacaaaaaaaacccaaacccaaacaacccccccccccccccccccccccccccciiiiaaaaaaaacccccccccccccccc
abaaacccccccccccaaaaaacccccccccaaaaaaaaaaacccaaacccccccaacccccccccccaacccccccccccccciiiiiiijaaaaccccccccaaccccccc
abaaaccccccccccccaaaacccccccccaaaaaaaacaaacccaaaccccccccccccccccccccaaacaaacccccccciiiiiiiijjjacccccccccaaacccccc
abcccccaacaacccccaaaaaccccccccaaaaaacccccacaacccccccccccccccccccccccaaaaaaaccccccciiiinnnoijjjjjjjjkkkaaaaaaacccc
abcccccaaaaacccccaacaaccccccccccaaaacccaaaaaaccccccccccccccccccccccccaaaaaaccccccciiinnnnooojjjjjjjkkkkaaaaaacccc
abccccaaaaacccccccccccccccccccccaccccccaaaaaaaccccccccccccccccccccaaaaaaaaccccccchhinnnnnoooojjooopkkkkkaaaaccccc
abccccaaaaaaccccccccccccccccccccccccccccaaaaaaacccccccccccccccccccaaaaaaaaacccccchhhnnntttuooooooopppkkkaaaaccccc
abccccccaaaaccccccccccacccccccccccccccccaaaaaaacccaaccccccccccccccaaaaaaaaaaccccchhhnnttttuuoooooppppkkkaaaaccccc
abccccccaccccccccccccaaaacaaaccccccccccaaaaaacaaccaacccaaccccccccccccaaacaaacccchhhnnnttttuuuuuuuuupppkkccaaccccc
abccccccccccccccaaccccaaaaaaaccccccccccaaaaaacaaaaaacccaaaaaaccccccccaaacccccccchhhnnntttxxxuuuuuuupppkkccccccccc
abcccccccccccccaaaacccaaaaaaacccaccccccccccaaccaaaaaaacaaaaaaccccccccaacccaaccchhhhnnnttxxxxuuyyyuupppkkccccccccc
abcccccccccccccaaaaccaaaaaaaaacaaacccccccccccccaaaaaaaaaaaaaccccccccccccccaaachhhhmnnnttxxxxxxyyyuvppkkkccccccccc
abcccccccccccccaaaacaaaaaaaaaaaaaaccccccccccccaaaaaacaaaaaaaccccccccccccccaaaghhhmmmttttxxxxxyyyyvvpplllccccccccc
abccacccccccccccccccaaaaaaaaaaaaaaccccccccccccaaaaaacccaaaaaacccaacaacccaaaaagggmmmttttxxxxxyyyyvvppplllccccccccc
SbaaaccccccccccccccccccaaacaaaaaaaacccccccccccccccaacccaaccaacccaaaaacccaaaagggmmmsttxxxEzzzzyyvvvppplllccccccccc
abaaaccccccccccccccccccaaaaaaaaaaaaacaaccccccccccccccccaaccccccccaaaaaccccaagggmmmsssxxxxxyyyyyyvvvqqqlllcccccccc
abaaacccccccccccccccccccaaaaaaaaaaaaaaaaacccccccccccccccccccccccaaaaaaccccaagggmmmsssxxxwywyyyyyyvvvqqlllcccccccc
abaaaaacccccccccccccccccccaacaaaccaaaaaaacccccccccccccccccccccccaaaaccccccaagggmmmssswwwwwyyyyyyyvvvqqqllcccccccc
abaaaaaccccccccccccccccccccccaaaccccaaaacccccccccccccccccaaccaacccaaccccccccgggmmmmssssswwyywwvvvvvvqqqlllccccccc
abaaaaacccccccccccccaccacccccaaaccccaaaacccccccccccccccccaaaaaacccccccccccaaggggmllllsssswwywwwvvvvqqqqlllccccccc
abaaccccccccccccccccaaaaccccccccccccaccaccccccccccccccccccaaaaacccccccccccaaagggglllllssswwwwwrrqqqqqqmmllccccccc
abaaccccccccccccccccaaaaaccccccaaccaaccccccccccccccccccccaaaaaaccaacccccccaaaaggfffllllsswwwwrrrrqqqqqmmmcccccccc
abacaaaccccccccccccaaaaaaccccccaaaaaaccccccaacccccccccccaaaaaaaacaaacaaccccaaaaffffflllsrrwwwrrrmmmmmmmmmcccccccc
abaaaaaccccccccccccaaaaaaccccccaaaaaccccccaaaaccccccccccaaaaaaaacaaaaaaccccaaaaccfffflllrrrrrrkkmmmmmmmccccaccccc
abaaaacccccccccccccccaaccccccccaaaaaacccccaaaacccccccccccccaaccaaaaaaaccccccccccccffflllrrrrrkkkmmmmmccccccaccccc
abaaacccccccccccccccccccccccccaaaaaaaaccccaaaacccccccccccccaaccaaaaaaacccccccccccccfffllkrrrkkkkmddddcccccaaacccc
abaaacccccccccccccccccccccccccaaaaaaaacccccccccccccccccccccccccccaaaaaaccccccccccccfffllkkkkkkkdddddddcaaaaaacccc
abaaaacccccccccccccccccccccccccccaaccccccccccccccccccccccccccccccaacaaacccccccccccccfeekkkkkkkddddddcccaaaccccccc
abcaaacccccccccccaaaccccccccaacccaaccccaaaaaccccaaaccccccccccccccaaccccccccccccccccceeeeekkkkdddddccccccaaccccccc
abccccccccccccccaaaaaaccccccaaacaaccacaaaaaaaccaaaaccccccccccaccaaccccccccccccccccccceeeeeeeedddacccccccccccccccc
abccccccccccccccaaaaaacccccccaaaaacaaaaaccaaaaaaaacccccccccccaaaaacccccccccccccccccccceeeeeeedaaacccccccccccccaaa
abccccccaaacccccaaaaacccccccaaaaaacaaaaaaaaaaaaaaaccccccccccccaaaaaccccccccccccccccccccceeeeecaaacccccccccccccaaa
abccccccaaaccccccaaaaacccccaaaaaaaaccaaaaacaaaaaaccccccccccccaaaaaacccccccccccccccccccccaaaccccaccccccccccccccaaa
abccccaacaaaaacccaaaaacccccaaaaaaaacaaaaaaaaaaaaaaaccccaaaaccaaaacccccccccccccccccccccccaccccccccccccccccccaaaaaa
abccccaaaaaaaaccccccccccccccccaaccccaacaaaaaaaaaaaaaaccaaaaccccaaacccccccccccccccccccccccccccccccccccccccccaaaaaa";

			return ParseData(data);
		}

		private static char[,] ParseData(string data)
		{
			var rows = data.Split("\r\n");

			int width = rows[0].Length;
			int height = rows.Length;

			var dataToReturn = new char[width, height];

			for (int y = 0; y < height; y++)
			{
				var characters = rows[y].ToCharArray();

				for (int x = 0; x < width; x++)
				{
					dataToReturn[x, y] = characters[x];
				}
			}

			return dataToReturn;
		}

		public class Node
		{
			public int X { get; private set; }
			public int Y { get; private set; }
			public int Distance = int.MaxValue;
			public bool HasBeenVisited { get; set; }
			public char Elevation { get; set; }
			public bool IsStartingNode { get; set; } = false;
			public bool IsFinalNode { get; set; } = false;

			public Node(int x, int y, char elevation)
			{
				X = x;
				Y = y;
				Elevation = elevation;
			}

			public override string ToString()
			{
				return $"Distance: {Distance} \t Has been visited: {HasBeenVisited}";
			}
		}
	}
}
