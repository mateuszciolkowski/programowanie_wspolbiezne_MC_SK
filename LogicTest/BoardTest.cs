using Logic;
using Data;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Linq;

namespace BoardLogicTest
{
    public class BoardLogicTests : IDisposable
    {
        private readonly BoardLogic _board;

        public BoardLogicTests()
        {
            _board = new BoardLogic(100, 100);
        }

        public void Dispose()
        {
            _board.Dispose(); // wy³¹cz timer po teœcie
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

            _board.AddBall(10, 10, 5, 1, 1, 0.1);

            Assert.Equal(initialCount + 1, _board.Balls.Count);
        }

        [Fact]
        public void RemoveBall_ShouldDecreaseBallCount()
        {
            _board.AddBall(10, 10, 5, 1, 1, 0.1);
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
            _board.AddBall(10, 10, 5, 1, 1, 0.1);
            _board.AddBall(20, 20, 5, -1, -1, 0.1);

            _board.ClearBalls();

            Assert.Empty(_board.Balls);
        }

        [Fact]
        public async Task MoveTheBalls_ShouldUpdateBallPositions()
        {
            _board.AddBall(10, 10, 5, 2, 3, 0.1);
            var initial = _board.Balls.First();

            double x0 = initial.X;
            double y0 = initial.Y;

            var tcs = new TaskCompletionSource();

            void Handler()
            {
                tcs.TrySetResult();
            }

            _board.BallsMoved += Handler;

            // Czekaj maksymalnie 500ms na zdarzenie
            await Task.WhenAny(tcs.Task, Task.Delay(500));
            _board.BallsMoved -= Handler;

            var updated = _board.Balls.First();

            Assert.NotEqual(x0, updated.X);
            Assert.NotEqual(y0, updated.Y);
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
