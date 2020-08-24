using Signs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Signs.Models
{
    public class Sign
    {
        public List<Symbol> Symbols { get; private set; }

        public int Value { get; private set; }

        public SignFamilies Family => (SignFamilies)(Value / 12);

        public SignNumbers Number => (SignNumbers)(Value % 12);

        public int Length => Symbols.Select(symbol => symbol.Length).Min();

        public Sign(int value)
        {
            if (value < 0 || value >= 144) throw new Exception("value must be between 0 and 143");
            Value = value;
            Symbols = new List<Symbol>();
        }

        public void Add(Symbol symbol)
        {
            if (!symbol.IsValid) throw new Exception("symbol must be valid");
            if (symbol.Value != Value) throw new Exception("symbol must have the same value as sign");
            Symbols.Add(symbol);
        }
    }
}
