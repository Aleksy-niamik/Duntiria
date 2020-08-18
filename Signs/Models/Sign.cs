using Signs.Enums;
using System;
using System.Collections.Generic;
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

        public Sign(int alef)
        {
            Circles = new List<Directions>();
            Alef = alef;
        }

        private double FloatValue
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

        public bool IsValid => (FloatValue == Math.Round(FloatValue)) && Value >= 0 && Value < 144;
    }
}
