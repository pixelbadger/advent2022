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
            InitializeForest();
        }

        public int HighestScenicScore()
        {
            var highestScore = 0;
            for (var x = 0; x < _forestWidth; x++)
            {
                for (var y = 0; y < _forestHeight; y++)
                {
                    var scenicScore = CalculateScenicScore(x, y);
                    if (scenicScore > highestScore) highestScore = scenicScore;
                }
            }

            return highestScore;
        }

        public int CountVisibleTrees()
        {
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

        private int CalculateScenicScore(int x, int y)
        {
            var upScore = ViewingDistanceInDirection(x, y, Direction.Up);
            var downScore = ViewingDistanceInDirection(x, y, Direction.Down);
            var leftScore = ViewingDistanceInDirection(x, y, Direction.Left);
            var rightScore = ViewingDistanceInDirection(x, y, Direction.Right);

            return upScore * leftScore * downScore * rightScore;
        }

        private int ViewingDistanceInDirection(int x, int y, Direction direction)
        {
            var treeValue = _forest[x, y];
            var viewingDistance = 0;
            switch (direction)
            {
                case Direction.Up:
                    y--;
                    while (y >= 0)
                    {
                        viewingDistance++;
                        if (_forest[x, y] >= treeValue) break;
                        y--;
                    }
                    return viewingDistance;
                case Direction.Down:
                    y++;
                    while (y < _forestHeight)
                    {
                        viewingDistance++;
                        if (_forest[x, y] >= treeValue) break;
                        y++;
                    }
                    return viewingDistance;
                case Direction.Left:
                    x--;
                    while (x >= 0)
                    {
                        viewingDistance++;
                        if (_forest[x, y] >= treeValue) break;
                        x--;
                    }
                    return viewingDistance;
                case Direction.Right:
                    x++;
                    while (x < _forestWidth)
                    {
                        viewingDistance++;
                        if (_forest[x, y] >= treeValue) break;
                        x++;
                    }
                    return viewingDistance;
                default:
                    throw new ArgumentException("Could not process direction");
            }
        }

        private bool VisibleInDirection(int x, int y, Direction direction)
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