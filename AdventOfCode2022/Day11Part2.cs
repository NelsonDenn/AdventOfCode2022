using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day11Part2
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			long result = RunPart2(data);

			return 0;
		}

		private static long RunPart2(List<Monkey> monkeys)
		{
			// Find the leve of monkey business after 20 rounds of moneys throwing stuff
			for (int round = 1; round <= 10000; round++)
			{
				// Each monkey inspects each of its items, then throws each to another monkey
				foreach (var monkey in monkeys)
				{
					foreach (var item in monkey.Items)
					{
						// Inspect the item
						monkey.InspectItem(item);

						// Divide worry level by 3 (round down)
						//item.WorryLevel /= 3;
						// Worry level is no longer divided by 3

						// Throw the item to another monkey, depending on the monkey's test of the item
						var nextMonkeyIndex = monkey.Test(item);
						monkeys.Find(monkey => monkey.MonkeyIndex == nextMonkeyIndex).Items.Add(item);
					}

					// Remove all items from this monkey's list, as they were all thrown to other monkeys
					monkey.Items.Clear();
				}
			}

			// Get the item inspection counts fo the two most active monkeys
			var itemInspectionCounts = monkeys
				.Select(monkey => monkey.ItemInspectionCount)
				.OrderByDescending(itemInspectionCount => itemInspectionCount)
				.Take(2)
				.ToList();

			long mostItemInspections = itemInspectionCounts[0];
			long secondMostItemInspections = itemInspectionCounts[1];

			// Multiply the top two for the resulting level of monkey business
			long monkeyBusiness = mostItemInspections * secondMostItemInspections;
			return monkeyBusiness;
			// Test:   2713310158
			// Actual: 20567144694
		}

		private static List<Monkey> GetTestData()
		{
			var monkeys = new List<Monkey>();

			var monkey0 = new Monkey
			{
				MonkeyIndex = 0,
				Items =
				{
					new Item
					{
						WorryLevel = 79
					},
					new Item
					{
						WorryLevel = 98
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Multiply,
					Value = 19
				},
				DivisibilityCheck = 23,
				IsDivisibleNextMonkeyIndex = 2,
				IsNotDivisibleNextMonkeyIndex = 3
			};
			monkeys.Add(monkey0);

			var monkey1 = new Monkey
			{
				MonkeyIndex = 1,
				Items =
				{
					new Item
					{
						WorryLevel = 54
					},
					new Item
					{
						WorryLevel = 65
					},
					new Item
					{
						WorryLevel = 75
					},
					new Item
					{
						WorryLevel = 74
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 6
				},
				DivisibilityCheck = 19,
				IsDivisibleNextMonkeyIndex = 2,
				IsNotDivisibleNextMonkeyIndex = 0
			};
			monkeys.Add(monkey1);

			var monkey2 = new Monkey
			{
				MonkeyIndex = 2,
				Items =
				{
					new Item
					{
						WorryLevel = 79
					},
					new Item
					{
						WorryLevel = 60
					},
					new Item
					{
						WorryLevel = 97
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Multiply,
					Value = 0 // Use 0 to mean multiply value by itself
				},
				DivisibilityCheck = 13,
				IsDivisibleNextMonkeyIndex = 1,
				IsNotDivisibleNextMonkeyIndex = 3
			};
			monkeys.Add(monkey2);

			var monkey3 = new Monkey
			{
				MonkeyIndex = 3,
				Items =
				{
					new Item
					{
						WorryLevel = 74
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 3
				},
				DivisibilityCheck = 17,
				IsDivisibleNextMonkeyIndex = 0,
				IsNotDivisibleNextMonkeyIndex = 1
			};
			monkeys.Add(monkey3);

			return monkeys;
		}

		private static List<Monkey> GetData()
		{
			var monkeys = new List<Monkey>();

			var monkey0 = new Monkey
			{
				MonkeyIndex = 0,
				Items =
				{
					new Item
					{
						WorryLevel = 59
					},
					new Item
					{
						WorryLevel = 74
					},
					new Item
					{
						WorryLevel = 65
					},
					new Item
					{
						WorryLevel = 86
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Multiply,
					Value = 19
				},
				DivisibilityCheck = 7,
				IsDivisibleNextMonkeyIndex = 6,
				IsNotDivisibleNextMonkeyIndex = 2
			};
			monkeys.Add(monkey0);

			var monkey1 = new Monkey
			{
				MonkeyIndex = 1,
				Items =
				{
					new Item
					{
						WorryLevel = 62,
					},
					new Item
					{
						WorryLevel = 84
					},
					new Item
					{
						WorryLevel = 72
					},
					new Item
					{
						WorryLevel = 91
					},
					new Item
					{
						WorryLevel = 68
					},
					new Item
					{
						WorryLevel = 78
					},
					new Item
					{
						WorryLevel = 51
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 1
				},
				DivisibilityCheck = 2,
				IsDivisibleNextMonkeyIndex = 2,
				IsNotDivisibleNextMonkeyIndex = 0
			};
			monkeys.Add(monkey1);

			var monkey2 = new Monkey
			{
				MonkeyIndex = 2,
				Items =
				{
					new Item
					{
						WorryLevel = 78
					},
					new Item
					{
						WorryLevel = 84
					},
					new Item
					{
						WorryLevel = 96
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 8
				},
				DivisibilityCheck = 19,
				IsDivisibleNextMonkeyIndex = 6,
				IsNotDivisibleNextMonkeyIndex = 5
			};
			monkeys.Add(monkey2);

			var monkey3 = new Monkey
			{
				MonkeyIndex = 3,
				Items =
				{
					new Item
					{
						WorryLevel = 97
					},
					new Item
					{
						WorryLevel = 86
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Multiply,
					Value = 0 // Use 0 to mean multiply value by itself
				},
				DivisibilityCheck = 3,
				IsDivisibleNextMonkeyIndex = 1,
				IsNotDivisibleNextMonkeyIndex = 0
			};
			monkeys.Add(monkey3);

			var monkey4 = new Monkey
			{
				MonkeyIndex = 4,
				Items =
				{
					new Item
					{
						WorryLevel = 50
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 6
				},
				DivisibilityCheck = 13,
				IsDivisibleNextMonkeyIndex = 3,
				IsNotDivisibleNextMonkeyIndex = 1
			};
			monkeys.Add(monkey4);

			var monkey5 = new Monkey
			{
				MonkeyIndex = 5,
				Items =
				{
					new Item
					{
						WorryLevel = 73
					},
					new Item
					{
						WorryLevel = 65
					},
					new Item
					{
						WorryLevel = 69
					},
					new Item
					{
						WorryLevel = 65
					},
					new Item
					{
						WorryLevel = 51
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Multiply,
					Value = 17
				},
				DivisibilityCheck = 11,
				IsDivisibleNextMonkeyIndex = 4,
				IsNotDivisibleNextMonkeyIndex = 7
			};
			monkeys.Add(monkey5);

			var monkey6 = new Monkey
			{
				MonkeyIndex = 6,
				Items =
				{
					new Item
					{
						WorryLevel = 69
					},
					new Item
					{
						WorryLevel = 82
					},
					new Item
					{
						WorryLevel = 97
					},
					new Item
					{
						WorryLevel = 93
					},
					new Item
					{
						WorryLevel = 82
					},
					new Item
					{
						WorryLevel = 84
					},
					new Item
					{
						WorryLevel = 58
					},
					new Item
					{
						WorryLevel = 63
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 5
				},
				DivisibilityCheck = 5,
				IsDivisibleNextMonkeyIndex = 5,
				IsNotDivisibleNextMonkeyIndex = 7
			};
			monkeys.Add(monkey6);

			var monkey7 = new Monkey
			{
				MonkeyIndex = 7,
				Items =
				{
					new Item
					{
						WorryLevel = 81
					},
					new Item
					{
						WorryLevel = 78
					},
					new Item
					{
						WorryLevel = 82
					},
					new Item
					{
						WorryLevel = 76
					},
					new Item
					{
						WorryLevel = 79
					},
					new Item
					{
						WorryLevel = 80
					}
				},
				Operation = new Operation
				{
					Type = OperationType.Add,
					Value = 3
				},
				DivisibilityCheck = 17,
				IsDivisibleNextMonkeyIndex = 3,
				IsNotDivisibleNextMonkeyIndex = 4
			};
			monkeys.Add(monkey7);

			return monkeys;
		}

		class Monkey
		{
			public int MonkeyIndex { get; set; }
			public List<Item> Items { get; set; } = new List<Item>();

			public Operation Operation { get; set; }
			public int DivisibilityCheck { get; set; }
			public int IsDivisibleNextMonkeyIndex { get; set; }
			public int IsNotDivisibleNextMonkeyIndex { get; set; }

			public int ItemInspectionCount { get; private set; } = 0; // Used to determine the most active monkeys

			// Part 1
			public void InspectItem(Item item)
			{
				// Perform the operation on the item
				item.Operations.Add(Operation);

				// Increment the item inspections count
				ItemInspectionCount++;
			}

			// Part 2
			public int Test(Item item)
			{
				int modulo = item.WorryLevel % DivisibilityCheck;

				// Perform all of the item's operations sequentially, keeping track of test results as we go
				foreach (var operation in item.Operations)
				{
					if (operation.Type == OperationType.Multiply)
					{
						if (operation.Value == 0)
						{
							// Multiply value * value
							modulo = modulo * modulo % DivisibilityCheck;
						}
						else
						{
							modulo = modulo * operation.Value % DivisibilityCheck;
						}
					}
					else if (operation.Type == OperationType.Add)
					{
						modulo = (modulo + operation.Value) % DivisibilityCheck;
					}
				}

				// Test the item
				var isDivisible = modulo == 0;

				// Return the index of the monkey to which the item will be thrown next
				return isDivisible ? IsDivisibleNextMonkeyIndex : IsNotDivisibleNextMonkeyIndex;
			}

			public override string ToString() => $"Monkey {MonkeyIndex}: {ItemInspectionCount}";
		}

		class Item
		{
			public int WorryLevel { get; set; } // The initial worry level of the item
			public List<Operation> Operations { get; set; } = new List<Operation>();
		}

		class Operation
		{
			public OperationType Type { get; set; }
			public int Value { get; set; }
		}

		private enum OperationType
		{
			Add, Multiply
		}
	}
}
