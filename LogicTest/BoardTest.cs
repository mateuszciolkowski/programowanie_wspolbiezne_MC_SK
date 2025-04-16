using Logic;
using Data;
using Xunit;
namespace boardLogicTest
{
    public class BoardLogicTests
    {
        private readonly BoardLogic _board;

        public BoardLogicTests()
        {
            _board = new BoardLogic(100, 100);
        }

        [Fact]
        public void ResizeBoard_ShouldUpdateDimensions()
        {
            _board.ResizeBoard(200, 300);

            Assert.Equal(200, _board.Width);
            Assert.Equal(300, _board.Height);
        }

        [Fact]
        public void AddBall_ShouldIncreaseBallCount()
        {
            int initialCount = _board.Balls.Count;

            _board.AddBall(10, 10, 5, 1, 1);

            Assert.Equal(initialCount + 1, _board.Balls.Count);
        }

        [Fact]
        public void RemoveBall_ShouldDecreaseBallCount()
        {
            _board.AddBall(10, 10, 5, 1, 1);
            int initialCount = _board.Balls.Count;

            _board.RemoveBall();

            Assert.Equal(initialCount - 1, _board.Balls.Count);
        }

        [Fact]
        public void RemoveBall_ShouldNotThrow_WhenNoBalls()
        {
            var exception = Record.Exception(() => _board.RemoveBall());

            Assert.Null(exception);
        }

        [Fact]
        public void ClearBalls_ShouldRemoveAllBalls()
        {
            _board.AddBall(10, 10, 5, 1, 1);
            _board.AddBall(20, 20, 5, -1, -1);

            _board.ClearBalls();

            Assert.Empty(_board.Balls);
        }

        [Fact]
        public void MoveTheBalls_ShouldUpdateBallPositions()
        {
            _board.AddBall(10, 10, 5, 2, 3);
            var initialBall = _board.Balls[0];

            double initialX = initialBall.X;
            double initialY = initialBall.Y;

            _board.MoveTheBalls(1.0);

            Assert.NotEqual(initialX, initialBall.X);
            Assert.NotEqual(initialY, initialBall.Y);
        }

        [Fact]
        public void GetBalls_ShouldReturnSameListAsBallsProperty()
        {
            var listFromProperty = _board.Balls;
            var listFromMethod = _board.GetBalls();

            Assert.Equal(listFromProperty.Count, listFromMethod.Count);
        }
    }
}
