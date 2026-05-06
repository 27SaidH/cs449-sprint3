using cs449sprint3.Models;

namespace cs449sprint3.Core
{
    public class Board
    {
        public int Size { get; }
        public BoardType Type { get; }

        private readonly CellState[,] _cells;

        public Board(int size, BoardType type)
        {
            if (size < 3 || size % 2 == 0)
                throw new ArgumentException(
                    "Board size must be an odd number greater than or equal to 3.");

            Size = size;
            Type = type;

            _cells = new CellState[size, size];

            Initialize();
        }

        private void Initialize()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    _cells[r, c] =
                        IsPlayablePosition(r, c)
                        ? CellState.Peg
                        : CellState.Invalid;
                }
            }

            int center = Size / 2;

            _cells[center, center] = CellState.Empty;
        }

        public bool IsInsideBounds(int r, int c)
        {
            return r >= 0 && r < Size &&
                   c >= 0 && c < Size;
        }

        public bool IsPlayablePosition(int r, int c)
        {
            if (!IsInsideBounds(r, c))
                return false;

            int center = Size / 2;

            switch (Type)
            {
                case BoardType.English:

                    int arm = Size / 3;

                    bool rowInMiddleBand =
                        r >= arm && r < Size - arm;

                    bool colInMiddleBand =
                        c >= arm && c < Size - arm;

                    return rowInMiddleBand || colInMiddleBand;

                case BoardType.Diamond:

                    return Math.Abs(r - center) +
                           Math.Abs(c - center) <= center;

                case BoardType.Hexagon:

                    return Math.Abs(r - c) <= center;

                default:
                    return false;
            }
        }

        public CellState GetCell(int r, int c)
        {
            if (!IsInsideBounds(r, c))
                throw new ArgumentOutOfRangeException();

            return _cells[r, c];
        }

        public void SetCell(int r, int c, CellState value)
        {
            if (!IsInsideBounds(r, c))
                throw new ArgumentOutOfRangeException();

            _cells[r, c] = value;
        }

        public int CountPegs()
        {
            int count = 0;

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (_cells[r, c] == CellState.Peg)
                        count++;
                }
            }

            return count;
        }
    }
}
