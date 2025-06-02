using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Logic;
using Xunit;

namespace Tests
{
    public class TestBoardBall : IBall
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public double VelocityX { get; private set; }
        public double VelocityY { get; private set; }
        public double Mass { get; set; }

        public TestBoardBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
        {
            X = x;
            Y = y;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Mass = mass;
        }

        public void Move(double time) => (X, Y) = (X + VelocityX * time, Y + VelocityY * time);
        public void SetVelocity(double vx, double vy) => (VelocityX, VelocityY) = (vx, vy);
    }

    public class FakeBallLogic : IBallLogic
    {
        public List<(IBall, double)> MoveCalls = new();
        public List<(IBall, double, double)> BounceCalls = new();
        public List<(IBall, IBall)> BounceBetweenBallsCalls = new();

        public IBall CreateBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
            => new TestBall(x, y, radius, velocityX, velocityY, mass);

        public void Move(IBall ball, double timeToMove) => MoveCalls.Add((ball, timeToMove));
        public void Bounce(IBall ball, double width, double height) => BounceCalls.Add((ball, width, height));
        public void BounceBetweenBalls(IBall ball1, IBall ball2) => BounceBetweenBallsCalls.Add((ball1, ball2));
    }

    public class BoardLogicTests : IDisposable
    {
        private readonly BoardLogic _boardLogic;
        private readonly FakeBallLogic _fakeBallLogic;

        public BoardLogicTests()
        {
            _fakeBallLogic = new FakeBallLogic();
            _boardLogic = new BoardLogic(100, 100, _fakeBallLogic);
        }

        public void Dispose()
        {
            _boardLogic.Dispose();
        }

        [Fact]
        public void AddBall_ShouldIncreaseBallCount()
        {
            _boardLogic.AddBall(0, 0, 10, 1, 1, 1);
            var balls = _boardLogic.GetBalls();
            Assert.Single(balls);
        }

        [Fact]
        public void RemoveBall_ShouldDecreaseBallCount()
        {
            _boardLogic.AddBall(0, 0, 10, 1, 1, 1);
            _boardLogic.RemoveBall();
            var balls = _boardLogic.GetBalls();
            Assert.Empty(balls);
        }

        [Fact]
        public void ClearBalls_ShouldRemoveAllBalls()
        {
            _boardLogic.AddBall(0, 0, 10, 1, 1, 1);
            _boardLogic.AddBall(5, 5, 10, 1, 1, 1);
            _boardLogic.ClearBalls();
            Assert.Empty(_boardLogic.GetBalls());
        }

        [Fact]
        public async Task GetBallsAsync_ShouldReturnAllBalls()
        {
            _boardLogic.AddBall(1, 2, 3, 4, 5, 6);
            var balls = await _boardLogic.GetBallsAsync();
            Assert.Single(balls);
        }

        [Fact]
        public void ResizeBoard_ShouldUpdateDimensions()
        {
            _boardLogic.ResizeBoard(300, 200);
            Assert.Equal(300, _boardLogic.Width);
            Assert.Equal(200, _boardLogic.Height);
        }

        [Fact]
        public void TimerShouldTriggerLogicMoveAndBounce()
        {
            _boardLogic.AddBall(0, 0, 10, 1, 1, 1);

            Thread.Sleep(20);

            Assert.True(_fakeBallLogic.MoveCalls.Count > 0);
            Assert.True(_fakeBallLogic.BounceCalls.Count > 0);
        }
    }
}
