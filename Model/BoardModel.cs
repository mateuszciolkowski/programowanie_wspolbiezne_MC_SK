using Logic.BoardLogicNamespace;

namespace Presentation.Model
{
    public class BoardModel
    {
        private static readonly Random _random = new Random();
        private readonly IBoardLogic _boardLogic;

        public event Action? Updated;

        public BoardModel(double width, double height, int amountOfBalls)
        {
            _boardLogic = new BoardLogic(width, height);
            for (int i = 0; i < amountOfBalls; i++)
            {
                double xRandom = _random.NextDouble() * width;
                double yRandom = _random.NextDouble() * height;
                double radiusRandom = _random.Next(20,30);
                double vxRandom = _random.Next(-3, 3);
                double vyRandom = _random.Next(-3, 3);
                   _boardLogic.AddBall(xRandom, yRandom, radiusRandom, vxRandom, vyRandom);
            }
        }


        public void AddBall(double x, double y, double radius, double vx, double vy)
        {
            _boardLogic.AddBall(x, y, radius, vx, vy);
            Updated?.Invoke();
        }
        public void RemoveBall()
        {
            _boardLogic.RemoveBall();
            Updated?.Invoke();
        }

    }
}
