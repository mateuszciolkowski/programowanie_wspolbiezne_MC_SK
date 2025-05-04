using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using Data;

namespace Logic
{
    public class BoardLogic : IBoardLogic, IDisposable
    {
        public double Width { get; private set; }
        public double Height { get; private set; }

        private readonly List<IBall> _balls = new List<IBall>();
        private readonly object _ballLock = new object();
        private readonly IBallLogic _ballLogic;
        private readonly System.Timers.Timer _timer;
        private readonly double _intervalMs = 1; // ~60 FPS
        //private readonly ReaderWriterLockSlim _ballLock = new();

        public event Action BallsMoved;

        public IReadOnlyList<IBall> Balls
        {
            get
            {
                lock (_ballLock)
                {
                    return _balls.ToList();
                }
            }
        }

        public BoardLogic(double width, double height)
        {
            Width = width;
            Height = height;
            _ballLogic = new BallLogic();

            _timer = new System.Timers.Timer(_intervalMs);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Synchronous update to avoid task overhead
            UpdateBalls(_intervalMs / 1000.0);
            BallsMoved?.Invoke();
        }

        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY, double mass)
        {
            var ball = _ballLogic.CreateBall(x, y, radius, velocityX, velocityY, mass);
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
                    _balls.RemoveAt(_balls.Count - 1);
            }
        }

        public void ClearBalls()
        {
            lock (_ballLock)
            {
                _balls.Clear();
            }
        }

        private void UpdateBalls(double deltaTime)
        {
            List<IBall> snapshot;
            lock (_ballLock)
            {
                snapshot = _balls.ToList();
            }

            foreach (var ball in snapshot)
            {
                _ballLogic.Move(ball, deltaTime);
                _ballLogic.Bounce(ball, Width, Height);
            }

            int count = snapshot.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    _ballLogic.BounceBeetwenBalls(snapshot[i], snapshot[j]);
                }
            }
        }

        public List<IBall> GetBalls()
        {
            lock (_ballLock)
            {
                return _balls.ToList();
            }
        }

        public async Task<List<IBall>> GetBallsAsync()
        {
            // Simulacja operacji asynchronicznej, np. pobieranie z bazy danych, API itp.
            return await Task.FromResult(new List<IBall>(_balls));
        }


        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}