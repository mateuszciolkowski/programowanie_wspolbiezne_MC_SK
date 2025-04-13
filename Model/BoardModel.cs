using System.ComponentModel;
using Logic;

namespace Model
{
    public class BoardModel 
    {
        private IBoard _board;

        public BoardModel(double width, double height)
        {
            _board = new Board(width, height);
        }

        public IReadOnlyList<IBall> GeTBalls() => _board.Balls;


    }
}
