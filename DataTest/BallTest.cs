using Data;

namespace DataTests
{
    public class BallTests
    {
      

        [Fact]
        public void Properties_ShouldBeSettable()
        {
            IBall ball = new Ball(0, 0, 1, 0, 0);

            ball.X = 15.0;
            ball.Y = 25.0;
            ball.Radius = 10.0;
            ball.VelocityX = 5.0;
            ball.VelocityY = -5.0;

            Assert.Equal(15.0, ball.X);
            Assert.Equal(25.0, ball.Y);
            Assert.Equal(10.0, ball.Radius);
            Assert.Equal(5.0, ball.VelocityX);
            Assert.Equal(-5.0, ball.VelocityY);
        }
    }
}