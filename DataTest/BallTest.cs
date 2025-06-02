using Xunit;
using Data;

namespace DataTests
{
    public class BallTests
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            double x = 50;
            double y = 60;
            double radius = 10;
            double velocityX = 3;
            double velocityY = -2;
            double mass = 5;

            var ball = new Ball(x, y, radius, velocityX, velocityY, mass);

            Assert.Equal(x - radius, ball.X);
            Assert.Equal(y - radius, ball.Y);
            Assert.Equal(radius, ball.Radius);
            Assert.Equal(velocityX, ball.VelocityX);
            Assert.Equal(velocityY, ball.VelocityY);
            Assert.Equal(mass, ball.Mass);
        }

        [Fact]
        public void Move_ShouldUpdatePositionCorrectly()
        {
            var ball = new Ball(10, 20, 5, 2, 3, 1);

            double initialX = ball.X;
            double initialY = ball.Y;
            double timeToMove = 2;

            ball.Move(timeToMove);

            Assert.Equal(initialX + 2 * 2, ball.X);
            Assert.Equal(initialY + 3 * 2, ball.Y);
        }

        [Fact]
        public void SetVelocity_ShouldUpdateVelocityCorrectly()
        {
            var ball = new Ball(0, 0, 1, 1, 1, 1);

            ball.SetVelocity(-5, 3);

            Assert.Equal(-5, ball.VelocityX);
            Assert.Equal(3, ball.VelocityY);
        }

        [Fact]
        public void Properties_ShouldBeGettableAndSettable()
        {
            var ball = new Ball(0, 0, 1, 0, 0, 1);

            ball.X = 100;
            ball.Y = 200;
            ball.Radius = 10;
            ball.Mass = 50;

            Assert.Equal(100, ball.X);
            Assert.Equal(200, ball.Y);
            Assert.Equal(10, ball.Radius);
            Assert.Equal(50, ball.Mass);
        }
    }
}
