using Data;
using Logic;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class BoardLogic : IBoardLogic
    {
        public double Width { get;  set; }
        public double Height { get;  set; }

        private List<IBall> _balls;
        private IBallLogic _balllogic;


        public IReadOnlyList<IBall> Balls => _balls.AsReadOnly();

        public BoardLogic(double width, double height)
        {
            Width = width;
            Height = height;
            _balls = new List<IBall>();
            _balllogic = new BallLogic();
        }
        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            IBall newBall = _balllogic.CreateBall(x, y, radius, velocityX, velocityY);
            _balls.Add(newBall);
        }

        public void RemoveBall()
        {
            if (_balls.Count > 0)
            {
                _balls.RemoveAt(_balls.Count - 1);
            }
        }

        public void ClearBalls()
        {
            _balls.Clear();
        }

        public void MoveTheBalls(double timeToMove)
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                _balllogic.Move(_balls[i], timeToMove);
                _balllogic.Bounce(_balls[i], Width, Height);
            }

            for (int i = 0; i < _balls.Count; i++)
            {
                for (int j = i + 1; j < _balls.Count; j++)
                {
                    _balllogic.BounceBeetwenBalls(_balls[i], _balls[j]);
                }
            }
        }
        public List<IBall> GetBalls()
        {
            return _balls;
        }
    }

}
