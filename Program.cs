using Advent2022;

Console.WriteLine("**** Day 1 ****");
var day1 = new Day1("./data/day1.txt");
var highestElf = day1.FindElfCarryingHighestCalories();
Console.WriteLine($"The elf carrying the highest calories of food is {highestElf}");
Console.WriteLine($"The sum of the top three calorie-carrying elves is {day1.SumOfCaloriesOfTopThreeElves()}");

Console.WriteLine();
Console.WriteLine("**** Day 2 ****");
var day2 = new Day2("./data/day2.txt");
Console.WriteLine($"The total score according to the strategy guide is {day2.GetTotalScore()}");
Console.WriteLine($"The total score according to the correct strategy guide is {day2.GetTotalScorePt2()}");

Console.WriteLine();
Console.WriteLine("**** Day 3 ****");
var day3 = new Day3("./data/day3.txt");
Console.WriteLine($"The sum of priorities of items in both compartments is {day3.SumOfPriorities()}");
Console.WriteLine($"The sum of priorities of all badges across groups of 3 elves is {day3.SumOfElfGroups()}");

Console.WriteLine();
Console.WriteLine("**** Day 4 ****");
var day4 = new Day4("./data/day4.txt");
Console.WriteLine($"The count of assignment pairs that fully contain the other is {day4.CountOfPairsWhereOneRangeContainsTheOther()}");
Console.WriteLine($"The count of assignment pairs that overlap is {day4.CountOfPairsThatOverlap()}");

Console.WriteLine();
Console.WriteLine("**** Day 5 ****");
var day5 = new Day5("./data/day5.txt");
day5.ProcessInstructionInput();
Console.WriteLine($"The crates on top of each stack using the CrateMover9000: {day5.TopCrates}");
day5 = new Day5("./data/day5.txt");
day5.ProcessInstructionInputPt2();
Console.WriteLine($"The crates on top of each stack using the CrateMover9001: {day5.TopCrates}");

Console.WriteLine();
Console.WriteLine("**** Day 6 ****");
var day6 = new Day6("./data/day6.txt");
Console.WriteLine($"Characters before first packet: {day6.CharactersBeforeStartOfPacket()}");