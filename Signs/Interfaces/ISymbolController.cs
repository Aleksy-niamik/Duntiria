using Signs.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Interfaces
{
    public interface ISymbolController
    {
        Image SymbolToImage(Symbol symbol, int radius);

        Image SymbolToSquare(Symbol symbol, int side);
    }
}
