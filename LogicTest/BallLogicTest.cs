using Data;
using Logic.Ball;
namespace LogicTest
{
    public class BallLogicTest
    {

        [Fact]
        public void CreateBallTest()
        {
            BallLogic ballLogic = new BallLogic();
            Data.Ball ball = ballLogic.CreateBall(40, 40, 20, 20, 20);
            Assert.NotNull(ball);
        }

        [Fact]
        public void BallMoveTest()
        {
            BallLogic ballLogic = new BallLogic();
            Data.Ball ball = ballLogic.CreateBall(40, 40, 20, 20, 20);
            double X_prev = ball.X;
            Thread.Sleep(500);
            ballLogic.Move(ball, 1);
            Assert.NotEqual(ball.X, X_prev);

        }
     

        [Theory]
        [InlineData(5, 10, 5, -2, 3, 20, 20)] 
        [InlineData(15, 10, 5, 2, 3, 20, 20)] 
        [InlineData(10, 5, 5, 2, -3, 20, 20)] 
        [InlineData(10, 15, 5, 2, 3, 20, 20)] 
        public void BounceTest(double x, double y, double radius, double velocityX, double velocityY, double width, double height)
        {
            BallLogic ballLogic = new BallLogic();

            Ball ball = ballLogic.CreateBall(x, y, radius, velocityX, velocityY);
            var initialVelocityX = ball.VelocityX;
            var initialVelocityY = ball.VelocityY;

            ballLogic.Bounce(ball, width, height);

            if (x - radius <= 0 || x + radius >= width)
            {
                Assert.Equal(-initialVelocityX, ball.VelocityX);
            }
            else
            {
                Assert.Equal(initialVelocityX, ball.VelocityX);
            }

            if (y - radius <= 0 || y + radius >= height)
            {
                Assert.Equal(-initialVelocityY, ball.VelocityY);
            }
            else
            {
                Assert.Equal(initialVelocityY, ball.VelocityY);
            }
        }

    }
}