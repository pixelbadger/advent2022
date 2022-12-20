namespace Advent2022
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Day8
    {
        private readonly string _input;
        private int _forestWidth;
        private int _forestHeight;
        private int[,] _forest;

        public Day8(string inputFilePath)
        {
            _input = File.ReadAllText(inputFilePath);
        }

        public int CountVisibleTrees()
        {
            InitializeForest();

            var visibleCount = 0;
            for (var x = 0; x < _forestWidth; x++)
            {
                for (var y = 0; y < _forestHeight; y++)
                {
                    // count edges implicitly
                    if (x == 0 || y == 0 || x == _forestWidth - 1 || y == _forestHeight - 1)
                    {
                        visibleCount++;
                        continue;
                    }

                    if (VisibleInDirection(x, y, Direction.Up))
                    {
                        visibleCount++;
                        continue;
                    }

                    if (VisibleInDirection(x, y, Direction.Down))
                    {
                        visibleCount++;
                        continue;
                    }

                    if (VisibleInDirection(x, y, Direction.Left))
                    {
                        visibleCount++;
                        continue;
                    }

                    if (VisibleInDirection(x, y, Direction.Right))
                    {
                        visibleCount++;
                        continue;
                    }
                }
            }

            return visibleCount;
        }

        public bool VisibleInDirection(int x, int y, Direction direction)
        {
            var treeValue = _forest[x, y];
            switch (direction)
            {
                case Direction.Up:
                    y--;
                    while (y >= 0)
                    {
                        if (_forest[x, y] >= treeValue) return false;
                        y--;
                    }
                    return true;
                case Direction.Down:
                    y++;
                    while (y < _forestHeight)
                    {
                        if (_forest[x, y] >= treeValue) return false;
                        y++;
                    }
                    return true;
                case Direction.Left:
                    x--;
                    while (x >= 0)
                    {
                        if (_forest[x, y] >= treeValue) return false;
                        x--;
                    }
                    return true;
                case Direction.Right:
                    x++;
                    while (x < _forestWidth)
                    {
                        if (_forest[x, y] >= treeValue) return false;
                        x++;
                    }
                    return true;
                default:
                    throw new ArgumentException("Could not process direction");
            }
        }

        private void InitializeForest()
        {
            var lines = _input.Split(Environment.NewLine);
            _forestWidth = lines[0].Length;
            _forestHeight = lines.Count();

            _forest = new int[_forestWidth, _forestHeight];

            var lineCount = 0;
            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    var number = line[i];
                    _forest[i, lineCount] = (int)char.GetNumericValue(line[i]);
                }

                lineCount++;
            }
        }
    }
}