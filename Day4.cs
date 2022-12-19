namespace Advent2022
{
    internal class Day4
    {
        private readonly string _input;

        public Day4(string inputFilePath)
        { 
            _input = File.ReadAllText(inputFilePath);
        }

        public int CountOfPairsWhereOneRangeContainsTheOther()
        {
            var count = 0;
            var lines = _input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var pair = line.Split(",");
                var first = new SectionAssignment(pair[0]);
                var second = new SectionAssignment(pair[1]);
                if (first.FullyContains(second) || second.FullyContains(first)) count++;
            }

            return count;
        }

        public int CountOfPairsThatOverlap()
        {
            var count = 0;
            var lines = _input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var pair = line.Split(",");
                var first = new SectionAssignment(pair[0]);
                var second = new SectionAssignment(pair[1]);
                if (first.Intersects(second)) count++;
            }

            return count;
        }
    }

    internal class SectionAssignment
    {
        private readonly int _start;
        private readonly int _end;

        public IEnumerable<int> Range
        {
            get
            {
                var list = new List<int>();
                for (var i = _start; i <= _end; i++)
                {
                    list.Add(i);
                }

                return list;
            }
        }

        public SectionAssignment(string input)
        {
            var values = input.Split("-");
            _start = int.Parse(values[0]);
            _end = int.Parse(values[1]);
        }

        public bool FullyContains(SectionAssignment other)
        {
            return other.Range.All(i => Range.Contains(i));
        }

        public bool Intersects(SectionAssignment other)
        {
            return other.Range.Intersect(Range).Count() > 0;
        }
    }
}