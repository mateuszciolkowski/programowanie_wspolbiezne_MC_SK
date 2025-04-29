using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Data;
using System.Diagnostics; // Dodajemy przestrzeń nazw dla Debug


namespace Logic
{
    public class BoardLogic : IBoardLogic
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        private readonly List<IBall> _balls = new();
        private readonly object _ballLock = new();
        private readonly IBallLogic _ballLogic;
        private System.Timers.Timer _timer;
        private double _interval = 5; 

        public event Action BallsMoved; 

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
            _ballLogic = new BallLogic();

            // Ustawienie timera
            _timer = new System.Timers.Timer(_interval);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        // Funkcja wywoływana przez Timer co 16ms

        private void TimerElapsed(object sender, ElapsedEventArgs e)
         {

        MoveTheBalls(_interval / 1000.0);

        BallsMoved?.Invoke();
        }

    public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            var ball = _ballLogic.CreateBall(x, y, radius, velocityX, velocityY);
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
            Parallel.ForEach(snapshot, ball =>
            {
                _ballLogic.Move(ball, timeToMove);
                _ballLogic.Bounce(ball, Width, Height);
            });

            int count = snapshot.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    lock (_ballLock)
                    {
                        _ballLogic.BounceBeetwenBalls(snapshot[i], snapshot[j]);
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
