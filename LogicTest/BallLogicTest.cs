using Xunit;
using Logic;
using Data;

namespace LogicTests
{
    public class BallLogicTests
    {
        private readonly BallLogic _ballLogic;

        public BallLogicTests()
        {
            _ballLogic = new BallLogic();
        }

        [Fact]
        public void CreateBall_ShouldReturnCorrectBall()
        {
            var ball = _ballLogic.CreateBall(10, 20, 5, 2, 3);

            Assert.Equal(5, ball.Radius);
            Assert.Equal(10 - 5, ball.X);
            Assert.Equal(20 - 5, ball.Y);
            Assert.Equal(2, ball.VelocityX);
            Assert.Equal(3, ball.VelocityY);
        }

        [Fact]
        public void Move_ShouldUpdateBallPosition()
        {
            var ball = new Ball(0, 0, 5, 10, 20); 
            _ballLogic.Move(ball, 1.0);

            Assert.Equal(-5 + 10, ball.X); 
            Assert.Equal(-5 + 20, ball.Y); 
        }

       
        [InlineData(-1, 10, 5, 50, 50, true, false)]
        [InlineData(48, 10, 5, 50, 50, true, false)] 
        [InlineData(10, -1, 5, 50, 50, false, true)]
        [InlineData(10, 48, 5, 50, 50, false, true)] 
        [InlineData(10, 10, 5, 50, 50, false, false)] 
        public void Bounce_ShouldReverseVelocityWhenOutOfBounds(
             double x, double y, double radius, double width, double height,
               bool expectXReversed, bool expectYReversed)
        {
          
            var initialVelocityX = 3;
            var initialVelocityY = 4;
            var ball = new Ball(x, y, radius, initialVelocityX, initialVelocityY);

           
            _ballLogic.Bounce(ball, width, height);

            
            var expectedVelocityX = expectXReversed ? -initialVelocityX : initialVelocityX;
            var expectedVelocityY = expectYReversed ? -initialVelocityY : initialVelocityY;

            Assert.Equal(expectedVelocityX, ball.VelocityX);
            Assert.Equal(expectedVelocityY, ball.VelocityY);
        }
    }
}
