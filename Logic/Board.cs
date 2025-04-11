using Data;
using System;
using System.Collections.Generic;

// Board i interfejs boardu korzysta z interfejsu IBallLogic, bo IballLogic odpowiedzialne jest za 1 kule, a
// board i IBoard odpowiedzialne jest za grupe piłek, dlatego 2 api w tej warstwie

namespace Logic
{
    public class Board : IBoard
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        private List<IBall> _balls = new List<IBall>();
        private IBallLogic _balllogic = new BallLogic();


        public IReadOnlyList<IBall> Balls => _balls.AsReadOnly();

        public Board(double width, double height)
        {
            Width = width;
            Height = height;
            _balls = [];
            _balllogic = new BallLogic();
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            IBall newBall = _balllogic.CreateBall(x, y, radius, velocityX, velocityY);
            _balls.Add(newBall);
        }

        void IBoard.MoveTheBalls(double timeToMove)
        {
            foreach (var IBall in _balls)
            {
                _balllogic.Move(IBall, timeToMove);
                _balllogic.Bounce(IBall, Width, Height);
            }
        }
    }

}
