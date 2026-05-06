using cs449sprint3.Models;

namespace cs449sprint3.Core
{
    public class ManualSolitaireGame : SolitaireGameBase
    {
        private readonly Random _random = new();

        public void RandomizeBoard()
        {
            if (Board == null)
                throw new InvalidOperationException(
                    "Start a game before randomizing.");

            int maxAttempts = 50;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                RandomizePlayableCells();

                if (Board.CountPegs() > 1 &&
                    GetValidMoves().Count > 0)
                {
                    return;
                }
            }
        }

        private void RandomizePlayableCells()
        {
            var playablePositions =
                new List<(int Row, int Col)>();

            for (int r = 0; r < Board.Size; r++)
            {
                for (int c = 0; c < Board.Size; c++)
                {
                    if (Board.IsPlayablePosition(r, c))
                    {
                        playablePositions.Add((r, c));
                    }
                }
            }

            foreach (var pos in playablePositions)
            {
                Board.SetCell(
                    pos.Row,
                    pos.Col,
                    _random.Next(2) == 0
                        ? CellState.Peg
                        : CellState.Empty);
            }

            if (Board.CountPegs() < 2)
            {
                int needed = 2 - Board.CountPegs();

                while (needed > 0)
                {
                    var pos =
                        playablePositions[
                            _random.Next(playablePositions.Count)];

                    if (Board.GetCell(pos.Row, pos.Col) != CellState.Peg)
                    {
                        Board.SetCell(pos.Row, pos.Col, CellState.Peg);
                        needed--;
                    }
                }
            }
        }
    }
}
