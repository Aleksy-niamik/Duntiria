using Signs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Interfaces
{
    public interface ISymbolsGenerator
    {
        IEnumerable<Symbol> Generate(int maxLength, int alef);
    }
}
