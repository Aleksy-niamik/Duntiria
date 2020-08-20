using Signs.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Models
{
    public class Sign
    {
        public int Id { get; set; }

        public List<Directions> Circles { get; set; }

        public int Length => Circles.Count + 1;

        public int Alef { get; set; }

        public Statuses Status 
        { 
            get
            {
                if (getPoints().Distinct().Count() != getPoints().Count())
                {
                    return Statuses.Loop;
                }
                
                if (FloatValue != Math.Round(FloatValue))
                {
                    return Statuses.Noninteger;
                }

                if (Value >= 144)
                {
                    return Statuses.TooBig;
                }

                if (Value < 0)
                {
                    return Statuses.Negative;
                }
                return Statuses.Valid;
            }
        }

        private List<Point> getPoints()
        {
            var points = new List<Point>();
            points.Add(new Point(0, 0));
            foreach (Directions circle in Circles)
            {
                switch (circle)
                {
                    case Directions.Right:
                        points.Add(new Point(points.Last().X + 1, points.Last().Y));
                        break;
                    case Directions.Left:
                        points.Add(new Point(points.Last().X - 1, points.Last().Y));
                        break;
                    case Directions.Up:
                        points.Add(new Point(points.Last().X, points.Last().Y - 1));
                        break;
                    case Directions.Down:
                        points.Add(new Point(points.Last().X, points.Last().Y + 1));
                        break;
                }
            }
            return points;
        }

        public int Height => getPoints().OrderByDescending(point => point.Y).First().Y - getPoints().OrderBy(point => point.Y).First().Y + 1;

        public int Width => getPoints().OrderByDescending(point => point.X).First().X - getPoints().OrderBy(point => point.X).First().X + 1;

        public int X => getPoints().OrderBy(point => point.X).First().X;

        public int Y => getPoints().OrderBy(point => point.Y).First().Y;

        public Sign(int alef)
        {
            Circles = new List<Directions>();
            Alef = alef;
        }

        public double FloatValue
        {
            get
            {
                double val = Alef;
                foreach (Directions circle in Circles)
                {
                    val = circle switch
                    {
                        Directions.Up => val * 3,
                        Directions.Down => val / 3,
                        Directions.Right => val + 3,
                        Directions.Left => val - 3
                    };
                }
                return Math.Round(val * 10000) / 10000;
            }
            set { }
        }
        public int Value => (int) Math.Round(FloatValue);

        public bool IsValid => Status == Statuses.Valid;
    }
}
