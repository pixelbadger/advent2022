namespace Advent2022
{
    internal class Day2
    {
        private string _input;

        public Day2(string inputFilePath)
        {
            _input = File.ReadAllText(inputFilePath);
        }

        public int GetTotalScore()
        {
            var rounds = _input.Split(Environment.NewLine).Select(l => new RpsRound(l));
            return rounds.Select(r => r.Score).Sum();
        }

        public int GetTotalScorePt2()
        {
            var rounds = _input.Split(Environment.NewLine).Select(l => new RpsRound(l)).ToList();
            rounds.ForEach(r => r.SetPlayerPlayBasedOnRoundState());
            return rounds.Select(r => r.Score).Sum();
        }
    }

    internal enum RpsPlay
    {
        Rock,
        Paper,
        Scissors
    }

    internal enum RpsRoundState
    {
        Win,
        Lose,
        Draw
    }

    internal class RpsRound
    {
        private readonly RpsPlay _opponent;
        private readonly RpsRoundState _roundState;
        private RpsPlay _player;

        public int Score => GetPlayScore() + GetWinScore();

        public RpsRound(string input)
        {
            var data = input.Split(' ');
            switch (data[0])
            {
                case "A":
                    _opponent = RpsPlay.Rock;
                    break;
                case "B":
                    _opponent = RpsPlay.Paper;
                    break;
                case "C":
                    _opponent = RpsPlay.Scissors;
                    break;
                default:
                    throw new ArgumentException("Could not parse opponent play");
            }

            switch (data[1])
            {
                case "X":
                    _player = RpsPlay.Rock;
                    _roundState = RpsRoundState.Lose;
                    break;
                case "Y":
                    _player = RpsPlay.Paper;
                    _roundState = RpsRoundState.Draw;
                    break;
                case "Z":
                    _player = RpsPlay.Scissors;
                    _roundState = RpsRoundState.Win;
                    break;
                default:
                    throw new ArgumentException("Could not parse player play");
            }
        }

        public void SetPlayerPlayBasedOnRoundState()
        {
            if (_roundState == RpsRoundState.Draw)
            {
                _player = _opponent;
                return;
            }

            if (_roundState == RpsRoundState.Lose)
            {
                switch (_opponent)
                {
                    case RpsPlay.Rock:
                        _player = RpsPlay.Scissors;
                        break;
                    case RpsPlay.Paper:
                        _player = RpsPlay.Rock;
                        break;
                    case RpsPlay.Scissors:
                        _player = RpsPlay.Paper;
                        break;
                    default:
                        throw new ArgumentException("Could not set player play to lose");
                }
            }

            if (_roundState == RpsRoundState.Win)
            {
                switch (_opponent)
                {
                    case RpsPlay.Rock:
                        _player = RpsPlay.Paper;
                        break;
                    case RpsPlay.Paper:
                        _player = RpsPlay.Scissors;
                        break;
                    case RpsPlay.Scissors:
                        _player = RpsPlay.Rock;
                        break;
                    default:
                        throw new ArgumentException("Could not set player play to win");
                }
            }
        }

        private int GetWinScore()
        {
            if (_opponent == _player) return 3;

            if (_opponent == RpsPlay.Rock && _player == RpsPlay.Scissors) return 0;
            if (_opponent == RpsPlay.Paper && _player == RpsPlay.Rock) return 0;
            if (_opponent == RpsPlay.Scissors && _player == RpsPlay.Paper) return 0;

            if (_player == RpsPlay.Rock && _opponent == RpsPlay.Scissors) return 6;
            if (_player == RpsPlay.Paper && _opponent == RpsPlay.Rock) return 6;
            if (_player == RpsPlay.Scissors && _opponent == RpsPlay.Paper) return 6;

            throw new ArgumentException("Could not calculate win score");
        }

        private int GetPlayScore()
        {
            switch (_player)
            {
                case RpsPlay.Rock:
                    return 1;
                case RpsPlay.Paper:
                    return 2;
                case RpsPlay.Scissors:
                    return 3;
                default:
                    throw new ArgumentException("Could not get play score");
            }
        }
    }
}