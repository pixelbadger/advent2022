namespace Advent2022
{
    public class Day3
    {
        private readonly string _input;
        public Day3(string inputFilePath)
        {
            _input = File.ReadAllText(inputFilePath);
        }

        public int SumOfPriorities()
        {
            return _input.Split(Environment.NewLine)
                .Select(l => new Rucksack(l))
                .Select(r => r.MatchingItemPriority)
                .Sum();
        }

        public int SumOfElfGroups()
        {
            var lines = _input.Split(Environment.NewLine);
            var chunks = lines.Chunk(3);
            var sum = 0;
            foreach (var chunk in chunks)
            {
                var elfGroup = new ElfGroup(chunk);
                sum += elfGroup.MatchingItemPriority;
            }

            return sum;
        }
    }

    internal class ElfGroup
    {
        private readonly string _elf1;
        private readonly string _elf2;
        private readonly string _elf3;

        public char MatchingItem => _elf1.Intersect(_elf2).Intersect(_elf3).Single();

        public int MatchingItemPriority => Utility.GetCharPriority(MatchingItem);

        public ElfGroup(IEnumerable<string> input)
        {
            var list = input.ToList();
            if (list.Count != 3) throw new ArgumentException("Input does not have three entries");

            _elf1 = list[0];
            _elf2 = list[1];
            _elf3 = list[2];
        }
    }

    internal class Rucksack
    {
        private readonly string _compartmentOne;
        private readonly string _compartmentTwo;

        public char MatchingItem => _compartmentOne
            .Intersect(_compartmentTwo)
            .Single();

        public int MatchingItemPriority => Utility.GetCharPriority(MatchingItem);

        public Rucksack(string input)
        {
            _compartmentOne = new String(input.Take((input.Length / 2)).ToArray());
            _compartmentTwo = new String(input.TakeLast((input.Length / 2)).ToArray());
        }

        public override string ToString() =>$"Compartment One: {_compartmentOne}; Compartment Two: {_compartmentTwo}";
    }

    public static class Utility
    {
        public static int GetCharPriority(char value) => char.IsLower(value)
            ? (int)value - 96
            : ((int)value - 64) + 26;
    }
}