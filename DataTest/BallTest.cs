using Data;

namespace DataTests
{
    public class BallTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            double x = 10.0;
            double y = 20.0;
            double radius = 5.0;
            double velocityX = 2.0;
            double velocityY = 3.0;

            // Act
            Ball ball = new Ball(x, y, radius, velocityX, velocityY);

            // Assert
            Assert.Equal(x, ball.X);
            Assert.Equal(y, ball.Y);
            Assert.Equal(radius, ball.Radius);
            Assert.Equal(velocityX, ball.VelocityX);
            Assert.Equal(velocityY, ball.VelocityY);
        }

        [Fact]
        public void Properties_ShouldBeSettable()
        {
            // Arrange
            Ball ball = new Ball(0, 0, 1, 0, 0);

            // Act
            ball.X = 15.0;
            ball.Y = 25.0;
            ball.Radius = 10.0;
            ball.VelocityX = 5.0;
            ball.VelocityY = -5.0;

            // Assert
            Assert.Equal(15.0, ball.X);
            Assert.Equal(25.0, ball.Y);
            Assert.Equal(10.0, ball.Radius);
            Assert.Equal(5.0, ball.VelocityX);
            Assert.Equal(-5.0, ball.VelocityY);
        }
    }
}