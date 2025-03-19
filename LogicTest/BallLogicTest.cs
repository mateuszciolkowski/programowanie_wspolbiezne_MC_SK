using Logic;
using System.Threading;
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

    }
}