using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022
{
	public class Day11
	{
		public static int Run()
		{
			bool isTest = false;
			var data = isTest ? GetTestData() : GetData();

			var result = RunPart1(data);

			return result;
		}

		private static int RunPart1(List<Monkey> monkeys)
		{
			// Find the leve of monkey business after 20 rounds of moneys throwing stuff
			for (int round = 1; round <= 20; round++)
			{
				// Each monkey inspects each of its items, then throws each to another monkey
				foreach (var monkey in monkeys)
				{
					foreach (var item in monkey.Items)
					{
						// Inspect the item
						monkey.InspectItem(item);

						// Divide worry level by 3 (round down)
						item.WorryLevel /= 3;

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

			// Multiply the top two for the resulting level of monkey business
			return itemInspectionCounts[0] * itemInspectionCounts[1];
			// Test: 10605
			// Actual: 61005
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
				Operation = new Action<Item>(item => item.WorryLevel *= 19),
				Test = new Func<Item, int> (item => item.WorryLevel % 23 == 0 ? 2 : 3)
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
				Operation = new Action<Item>(item => item.WorryLevel += 6),
				Test = new Func<Item, int>(item => item.WorryLevel % 19 == 0 ? 2 : 0)
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
				Operation = new Action<Item>(item => item.WorryLevel *= item.WorryLevel),
				Test = new Func<Item, int>(item => item.WorryLevel % 13 == 0 ? 1 : 3)
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
				Operation = new Action<Item>(item => item.WorryLevel += 3),
				Test = new Func<Item, int>(item => item.WorryLevel % 17 == 0 ? 0 : 1)
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
				Operation = new Action<Item>(item => item.WorryLevel *= 19),
				Test = new Func<Item, int>(item => item.WorryLevel % 7 == 0 ? 6 : 2)
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
				Operation = new Action<Item>(item => item.WorryLevel += 1),
				Test = new Func<Item, int>(item => item.WorryLevel % 2 == 0 ? 2 : 0)
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
				Operation = new Action<Item>(item => item.WorryLevel += 8),
				Test = new Func<Item, int>(item => item.WorryLevel % 19 == 0 ? 6 : 5)
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
				Operation = new Action<Item>(item => item.WorryLevel *= item.WorryLevel),
				Test = new Func<Item, int>(item => item.WorryLevel % 3 == 0 ? 1 : 0)
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
				Operation = new Action<Item>(item => item.WorryLevel += 6),
				Test = new Func<Item, int>(item => item.WorryLevel % 13 == 0 ? 3 : 1)
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
				Operation = new Action<Item>(item => item.WorryLevel *= 17),
				Test = new Func<Item, int>(item => item.WorryLevel % 11 == 0 ? 4 : 7)
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
				Operation = new Action<Item>(item => item.WorryLevel += 5),
				Test = new Func<Item, int>(item => item.WorryLevel % 5 == 0 ? 5 : 7)
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
				Operation = new Action<Item>(item => item.WorryLevel += 3),
				Test = new Func<Item, int>(item => item.WorryLevel % 17 == 0 ? 3 : 4)
			};
			monkeys.Add(monkey7);

			return monkeys;
		}

		class Monkey
		{
			public int MonkeyIndex { get; set; }
			public List<Item> Items { get; set; } = new List<Item>();

			public Action<Item> Operation { get; set; }
			public Func<Item, int> Test { get; set; }

			public int ItemInspectionCount { get; private set; } = 0; // Used to determine the most active monkeys

			// Part 1
			public void InspectItem(Item item)
			{
				// Perform the operation on the item
				Operation(item);

				// Increment the item inspections count
				ItemInspectionCount++;
			}

			public override string ToString() => $"Monkey {MonkeyIndex}: {ItemInspectionCount}";
		}

		class Item
		{
			public int WorryLevel { get; set; }

			public override string ToString() => $"{WorryLevel}";
		}
	}
}
