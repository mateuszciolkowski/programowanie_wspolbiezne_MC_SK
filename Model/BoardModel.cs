using System.Collections.Generic;

namespace Model
{
    public class BoardModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public List<BallModel> Balls { get; set; }

        public BoardModel(double width, double height)
        {
            Width = width;
            Height = height;
            Balls = new List<BallModel>();
        }
    }
}
