﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using Logic;

namespace Model
{
    public class BoardModel : IBoardModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        BoardLogic boardLogic;
        // Publiczna ObservableCollection
        public ObservableCollection<BallModel> Balls { get; set; }

        public BoardModel(double width, double height)
        {
            Width = width;
            Height = width;
            boardLogic = new BoardLogic(width, height);
            Balls = new ObservableCollection<BallModel>(); 
        }

        public void ResizeBoard(double width, double height)
        {
            Width = width;
            Height = height;
            boardLogic.ResizeBoard(width, height);
        }

        public void AddBall(double x, double y, double radius, double velocityX, double velocityY)
        {
            // Tworzymy nowy obiekt BallModel za pomocą konstruktora
            var ball = new BallModel(x, y, radius, velocityX, velocityY);

            // Dodajemy go do kolekcji Balls w BoardModel
            Balls.Add(ball);

            // Jeśli konieczne, możemy dodać piłkę do logiki gry
            boardLogic.AddBall(x, y, radius, velocityX, velocityY);
        }


        public void RemoveBall()
        {
            if (Balls.Count > 0)
            {
                Balls.RemoveAt(Balls.Count - 1);
            }
        }

        public void MoveTheBalls(double timeToMove)
        {

            for (int i = 0; i < Balls.Count; i++)
            {
                var ball = Balls[i];

                // Aktualizacja pozycji na podstawie prędkości
                ball.X += ball.VelocityX * timeToMove;
                ball.Y += ball.VelocityY * timeToMove;

                // Logowanie pozycji

                // Odbicie od krawędzi ekranu
                if (ball.X - ball.Radius < 0 || ball.X + ball.Radius > Width)
                {
                    ball.VelocityX = -ball.VelocityX; // Zmieniamy kierunek na przeciwny
                }
                if (ball.Y - ball.Radius < 0 || ball.Y + ball.Radius > Height)
                {
                    ball.VelocityY = -ball.VelocityY; // Zmieniamy kierunek na przeciwny
                }
            }
        }


        public void ClearBalls()
        {
            Balls.Clear();
        }
    }
}
