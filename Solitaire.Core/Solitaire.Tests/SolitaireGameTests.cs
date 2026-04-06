using Xunit;
using cs449sprint2.Core;
using cs449sprint2.Models;

namespace cs449sprint2.Tests
{
    public class SolitaireGameTests
    {
        [Fact]
        public void NewGame_CreatesBoard()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            Assert.NotNull(game.Board);
            Assert.Equal(7, game.Board.Size);
            Assert.Equal(BoardType.English, game.Board.Type);
        }

        [Fact]
        public void NewGame_SetsCenterToEmpty()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            Assert.Equal(CellState.Empty, game.Board.GetCell(3, 3));
        }

        [Fact]
        public void ValidMove_Works()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.Board.SetCell(3, 1, CellState.Peg);
            game.Board.SetCell(3, 2, CellState.Peg);
            game.Board.SetCell(3, 3, CellState.Empty);

            Assert.True(game.MakeMove(3, 1, 3, 3));
            Assert.Equal(CellState.Empty, game.Board.GetCell(3, 1));
            Assert.Equal(CellState.Empty, game.Board.GetCell(3, 2));
            Assert.Equal(CellState.Peg, game.Board.GetCell(3, 3));
        }

        [Fact]
        public void InvalidMove_ReturnsFalse_WhenOutOfBounds()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            Assert.False(game.IsValidMove(0, 0, -2, 0));
            Assert.False(game.IsValidMove(0, 0, 0, 8));
        }

        [Fact]
        public void IsGameOver_ReturnsTrue_WhenNoMovesExist()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            for (int r = 0; r < game.Board.Size; r++)
            {
                for (int c = 0; c < game.Board.Size; c++)
                {
                    if (game.Board.IsPlayablePosition(r, c))
                        game.Board.SetCell(r, c, CellState.Empty);
                }
            }

            game.Board.SetCell(3, 3, CellState.Peg);

            Assert.True(game.IsGameOver());
        }

        [Fact]
        public void RandomizeBoard_KeepsInvalidCellsInvalid()
        {
            var game = new SolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.RandomizeBoard();

            for (int r = 0; r < game.Board.Size; r++)
            {
                for (int c = 0; c < game.Board.Size; c++)
                {
                    if (!game.Board.IsPlayablePosition(r, c))
                    {
                        Assert.Equal(CellState.Invalid, game.Board.GetCell(r, c));
                    }
                }
            }
        }

        [Fact]
        public void AutomatedGame_MakesAutomaticMove_WhenMoveExists()
        {
            var game = new AutomatedSolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.Board.SetCell(3, 1, CellState.Peg);
            game.Board.SetCell(3, 2, CellState.Peg);
            game.Board.SetCell(3, 3, CellState.Empty);

            Assert.True(game.MakeAutomaticMove());
        }

        [Fact]
        public void AutomatedGame_AutoPlayToEnd_EndsGame()
        {
            var game = new AutomatedSolitaireGame();
            game.StartNewGame(7, BoardType.English);

            game.AutoPlayToEnd();

            Assert.True(game.IsGameOver());
        }
    }
}
