namespace Advent2022
{
    internal class Day1
    {
        private string _input;

        public Day1(string inputFilePath)
        {
            _input = File.ReadAllText(inputFilePath);
        }

        public int SumOfCaloriesOfTopThreeElves() => GetElfBreakdown()
            .Select(e => e.Sum())
            .OrderDescending()
            .Take(3)
            .Sum();

        public int FindElfCarryingHighestCalories()
        {
            var elves = GetElfBreakdown();
            var totals = elves.Select(e => e.Sum()).ToList();
            var highestCount = 0;
            var highestElf = 0;
            for (var i = 0; i < totals.Count(); i++)
            {
                if (totals[i] > highestCount)
                {
                    highestCount = totals[i];
                    highestElf = ++i;
                }
            }

            return highestCount;
        }

        private IEnumerable<IEnumerable<int>> GetElfBreakdown()
        {
            var list = new List<IEnumerable<int>>();
            var lines = _input.Split(Environment.NewLine);
            
            List<int> elf = new List<int>();
            foreach (var line in lines)
            {
                // if at end of elf, add to main list and create new elf
                if (string.IsNullOrEmpty(line))
                {
                    list.Add(elf);
                    elf = new List<int>();
                }
                else // add to current elf
                {
                    elf.Add(int.Parse(line));
                }
            }

            return list;
        }
    }
}