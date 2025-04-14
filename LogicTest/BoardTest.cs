using Data;
using Logic.Ball;
using Logic.Board;
namespace LogicTest
{
    public class BoardTest
    {
        [Fact]
        public void AddBallTest()
        {
            BallLogic ballLogic = new BallLogic();
            BoardLogic board = new BoardLogic(200, 200);
            Assert.Empty(board.Balls);
            board.AddBall(10, 10, 10, 10, 10);
            Assert.NotEmpty(board.Balls);
        }
        [Fact]
        public void MoveTheBallsTest()
        {
            BallLogic ballLogic = new BallLogic();
            BoardLogic board = new BoardLogic(200, 200);
            board.AddBall(10, 10, 10, 10, 10);
            Ball ball = board.Balls[0];
            double x_prev = ball.X;
            Thread.Sleep(100);
            board.MoveTheBalls(100);
            Assert.NotEqual(x_prev, board.Balls[0].X);

        }
    }
}