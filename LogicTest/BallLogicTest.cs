using System;
using Logic;
using Data;
using Xunit;

namespace Tests
{
    public class TestBall : IBall
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public double VelocityX { get; private set; }
        public double VelocityY { get; private set; }
        public double Mass { get; set; }

        public TestBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
        {
            X = x;
            Y = y;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Mass = mass;
        }

        public void Move(double time)
        {
            X += VelocityX * time;
            Y += VelocityY * time;
        }

        public void SetVelocity(double vx, double vy)
        {
            VelocityX = vx;
            VelocityY = vy;
        }
    }

    public class BallLogicTests
    {
        private readonly IBallLogic _logic;

        public BallLogicTests()
        {
            _logic = new BallLogic(); 
        }

        [Fact]
        public void CreateBall_ReturnsCorrectBall()
        {
            var ball = _logic.CreateBall(0, 0, 10, 1, -1, 2);

            Assert.Equal(-10, ball.X);
            Assert.Equal(-10, ball.Y);
            Assert.Equal(10, ball.Radius);
            Assert.Equal(1, ball.VelocityX);
            Assert.Equal(-1, ball.VelocityY);
            Assert.Equal(2, ball.Mass);
        }

        [Fact]
        public void Move_UpdatesPositionCorrectly()
        {
            var ball = new TestBall(0, 0, 10, 2, 3, 1);
            _logic.Move(ball, 2);

            Assert.Equal(4, ball.X);
            Assert.Equal(6, ball.Y);
        }

        [Fact]
        public void Bounce_ReversesVelocityOnWallCollision()
        {
            var ball = new TestBall(5, 5, 10, 2, 3, 1);
            _logic.Bounce(ball, 10, 10);

            Assert.True(ball.VelocityX < 0); 
            Assert.True(ball.VelocityY < 0); 
        }

        [Fact]
        public void BounceBetweenBalls_ChangesVelocitiesOnCollision()
        {
            var ball1 = new TestBall(0, 0, 10, 1, 0, 1);
            var ball2 = new TestBall(5, 0, 10, -1, 0, 1);

            _logic.BounceBetweenBalls(ball1, ball2);

            Assert.True(ball1.VelocityX < 0);
            Assert.True(ball2.VelocityX > 0);
        }
    }
}
