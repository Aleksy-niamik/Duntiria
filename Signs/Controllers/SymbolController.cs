using Signs.Enums;
using Signs.Interfaces;
using Signs.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Controllers
{
    public class SymbolController : ISymbolController
    {
        /*dwa główne warianty:
            -metoda rysująca w określonej ramce
            -metoda rysująca jak wyjdzie, z podaniem promienia kółka
        */
        public Image SymbolToImage(Symbol symbol, int radius)
        {
            var dist = radius * 2 - 1;
            var maxSide = symbol.Length * dist * 2;
            var bitmap = new Bitmap(maxSide, maxSide);

            var g = Graphics.FromImage(bitmap);
           // g.Clear(Color.Transparent);

            int x = maxSide / 2;
            int y = maxSide / 2;
            int prevX = x;
            int prevY = y;
            g.DrawEllipse(new Pen(Color.BlueViolet, 2), new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius));
            g.DrawEllipse(new Pen(Color.BlueViolet, 2), new Rectangle(x - radius / 3, y - radius/3, 2 * radius / 3, 2 * radius / 3));

            for (int i=0; i<symbol.Circles.Count; i++)
            {
                prevX = x;
                prevY = y;
                switch (symbol.Circles[i])
                {
                    case Directions.Right:
                        x += dist;
                        break;
                    case Directions.Left:
                        x -= dist;
                        break;
                    case Directions.Up:
                        y -= dist;
                        break;
                    case Directions.Down:
                        y += dist;
                        break;
                }
                if(i == 0)
                {
                    prevX = (prevX * 5 + x) / 6;
                    prevY = (prevY * 5 + y) / 6;
                }
                g.DrawEllipse(new Pen(Color.BlueViolet, 2), new Rectangle(x - radius, y - radius, 2*radius, 2*radius));
                if (i == symbol.Circles.Count - 1)
                {
                    g.FillEllipse(new SolidBrush(Color.BlueViolet), new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius));
                    x = (prevX + x) / 2;
                    y = (prevY + y) / 2;
                }
                g.FillRectangle(new SolidBrush(Color.BlueViolet), 
                    new Rectangle(Math.Min(x, prevX), Math.Min(y, prevY), 
                        Math.Abs(prevX-x)+2, Math.Abs(prevY-y)+2));
            }
            g.Dispose();

            var bitmap2 = bitmap.Clone(new Rectangle(
                symbol.X * 2 * radius - radius - symbol.X + maxSide/2,
                symbol.Y * 2 * radius - radius - symbol.Y + maxSide / 2,
                symbol.Width * 2 * radius - symbol.Width + 2,
                symbol.Height * 2 * radius - symbol.Height + 2), bitmap.PixelFormat);

            return bitmap2;
        }

        public Image SymbolToSquare(Symbol symbol, int side)
        {
            var bigger = Math.Max(symbol.Width, symbol.Height);
            var radius = (side - 2 + bigger) / (2 * bigger);
            var img = SymbolToImage(symbol, radius);
            var biggerSide = Math.Max(img.Width, img.Height);

            var img2 = new Bitmap(biggerSide, biggerSide);
            var g = Graphics.FromImage(img2);
            //g.Clear(Color.Blue);
            if(img.Width >= img.Height)
            {
                g.DrawImage(img, new Point(0, (biggerSide - img.Height) / 2));
            }
            else
            {
                g.DrawImage(img, new Point((biggerSide - img.Width) / 2, 0));
            }
            g.Dispose();

            return img2;
        }
    }
}
