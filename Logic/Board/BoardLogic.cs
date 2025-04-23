using Data;
using Logic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class BoardLogic : IBoardLogic
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        private readonly List<IBall> _balls = new();
        private readonly object _ballLock = new();
        private readonly IBallLogic _balllogic;

        public IReadOnlyList<IBall> Balls
        {
            get
            {
                lock (_ballLock)
                {
                    return _balls.AsReadOnly();
                }
            }
        }

        public BoardLogic(double width, double height)
        {
            Width = width;
            Height = height;
            _balllogic = new BallLogic();
        }

        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            var ball = _balllogic.CreateBall(x, y, radius, velocityX, velocityY);
            lock (_ballLock)
            {
                _balls.Add(ball);
            }
        }

        public void RemoveBall()
        {
            lock (_ballLock)
            {
                if (_balls.Count > 0)
                {
                    _balls.RemoveAt(_balls.Count - 1);
                }
            }
        }

        public void ClearBalls()
        {
            lock (_ballLock)
            {
                _balls.Clear();
            }
        }

        public void MoveTheBalls(double timeToMove)
        {
            List<IBall> snapshot;
            lock (_ballLock)
            {
                snapshot = new List<IBall>(_balls);
            }

            // Równoległe poruszanie i odbijanie od ścian
            Parallel.ForEach(snapshot, ball =>
            {
                _balllogic.Move(ball, timeToMove);
                _balllogic.Bounce(ball, Width, Height);
            });

            // Kolizje między kulkami (z zachowaniem synchronizacji)
            int count = snapshot.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    lock (_ballLock)
                    {
                        _balllogic.BounceBeetwenBalls(snapshot[i], snapshot[j]);
                    }
                }
            }
        }

        public List<IBall> GetBalls()
        {
            lock (_ballLock)
            {
                return new List<IBall>(_balls);
            }
        }
    }
}
