using Logic;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(100, 100);

            Ball ball1 = new Ball(10, 20, 2,1,1);
            Ball ball2 = new Ball(30, 40, 3,1,1);
            Ball ball3 = new Ball(50, 60, 1, 1, 1);

            board.AddBall(ball1);
            board.AddBall(ball2);
            board.AddBall(ball3);

            board.Display();

        }
    }
}
