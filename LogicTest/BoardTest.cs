using Logic;
using Data;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace BoardLogicTest
{
    public class BoardLogicDiTests : IDisposable
    {
        private readonly BoardLogic _board;

        public BoardLogicDiTests()
        {
            // U¿ywamy prawdziwej implementacji
            var ballLogic = new BallLogic();
            _board = new BoardLogic(100, 100, ballLogic);
        }

        public void Dispose()
        {
            _board.Dispose();
        }

        [Fact]
        public void AddBall_WithInjectedBallLogic_ShouldAddBall()
        {
            int countBefore = _board.Balls.Count;

            _board.AddBall(10, 10, 5, 1, 1, 0.5);

            int countAfter = _board.Balls.Count;

            Assert.Equal(countBefore + 1, countAfter);
        }

        [Fact]
        public async Task BallMoves_AfterTimeWithInjectedBallLogic()
        {
            _board.AddBall(10, 10, 5, 2, 2, 1);

            var firstPosition = _board.Balls.First();
            double x0 = firstPosition.X;
            double y0 = firstPosition.Y;

            var tcs = new TaskCompletionSource();

            void Handler()
            {
                tcs.TrySetResult();
            }

            _board.BallsMoved += Handler;

            await Task.WhenAny(tcs.Task, Task.Delay(500));
            _board.BallsMoved -= Handler;

            var after = _board.Balls.First();

            Assert.NotEqual(x0, after.X);
            Assert.NotEqual(y0, after.Y);
        }

     
    }
}
