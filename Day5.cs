using System.Text;

namespace Advent2022
{
    public class Day5
    {
        private readonly IEnumerable<string> _input;
        private readonly List<Stack<char>> _stacks = new List<Stack<char>>();

        public string TopCrates
        {
            get 
            {
                var sb = new StringBuilder();
                for (var i = 0; i < _stacks.Count; i++)
                {
                    sb.Append(_stacks[i].Pop());
                }

                return sb.ToString();
            }
        }

        public Day5(string inputFilePath)
        {
            var input = File.ReadAllText(inputFilePath).Split(Environment.NewLine);
            for (var i = 0; i < 9; i++) _stacks.Add(new Stack<char>());

            var stackDefinition = input.Take(8);
            ProcessStackDefinition(stackDefinition);

            _input = input.Skip(10);
        }

        public void ProcessInstructionInput()
        {
            var tuples = _input
                .Select(i => i.Replace("move ", "").Replace("from ", "").Replace("to ", ""))
                .Select(i => i.Split(" "))
                .Select(i => (Quantity: int.Parse(i[0]), From: int.Parse(i[1]) - 1, To: int.Parse(i[2]) - 1));

            foreach (var tuple in tuples)
            {
                for (var i = 0; i < tuple.Quantity; i++)
                {
                    var value = _stacks[tuple.From].Pop();
                    _stacks[tuple.To].Push(value);
                }
            }
        }

        public void ProcessInstructionInputPt2()
        {
            var tuples = _input
                .Select(i => i.Replace("move ", "").Replace("from ", "").Replace("to ", ""))
                .Select(i => i.Split(" "))
                .Select(i => (Quantity: int.Parse(i[0]), From: int.Parse(i[1]) - 1, To: int.Parse(i[2]) - 1));

            foreach (var tuple in tuples)
            {
                var stack = new Stack<char>();
                for (var i = 0; i < tuple.Quantity; i++)
                {
                    var value = _stacks[tuple.From].Pop();
                    stack.Push(value);
                }

                while (stack.Count > 0)
                {
                    var value = stack.Pop();
                    _stacks[tuple.To].Push(value);
                }
            }
        }

        private void ProcessStackDefinition(IEnumerable<string> input)
        {
            input = input.Reverse();
            foreach (var line in input)
            {
                if (!char.IsWhiteSpace(line[1])) _stacks[0].Push(line[1]);
                if (!char.IsWhiteSpace(line[5])) _stacks[1].Push(line[5]);
                if (!char.IsWhiteSpace(line[9])) _stacks[2].Push(line[9]);
                if (!char.IsWhiteSpace(line[13])) _stacks[3].Push(line[13]);
                if (!char.IsWhiteSpace(line[17])) _stacks[4].Push(line[17]);
                if (!char.IsWhiteSpace(line[21])) _stacks[5].Push(line[21]);
                if (!char.IsWhiteSpace(line[25])) _stacks[6].Push(line[25]);
                if (!char.IsWhiteSpace(line[29])) _stacks[7].Push(line[29]);
                if (!char.IsWhiteSpace(line[33])) _stacks[8].Push(line[33]);
            }   
        }
    }
}