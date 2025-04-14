using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BallModel
    {
        public BallModel(double x, double y, double radius, double velocityX, double velocityY)
        {
            X = x;
            Y = y;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

    }
}
