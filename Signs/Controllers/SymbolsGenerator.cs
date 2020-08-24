using Signs.Enums;
using Signs.Interfaces;
using Signs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Signs.Controllers
{
    public class SymbolsGenerator : ISymbolsGenerator
    {
        public IEnumerable<Symbol> Generate(int maxLength, int alef)
        {
            var symbolsTable =  new List<Symbol>[maxLength];
            for (int i = 0; i < symbolsTable.Length; i++)
                symbolsTable[i] = new List<Symbol>();

            symbolsTable[0].Add(new Symbol(alef));

            for(int i=1; i<maxLength; i++)
            {
                //dla każdej długości łańcucha kółek
                for(int j=0; j<symbolsTable[i-1].Count; j++)
                {
                    //dla każdego łacucha kółek z poprzedniego generowania (mniejszej o jeden długości)
                    
                    if(symbolsTable[i - 1][j].Circles.Any())
                    {
                        Symbol symbol1 = new Symbol(alef);
                        Symbol symbol2 = new Symbol(alef);
                        Symbol symbol3 = new Symbol(alef);

                        symbol1.Circles.AddRange(symbolsTable[i - 1][j].Circles);
                        symbol2.Circles.AddRange(symbolsTable[i - 1][j].Circles);
                        symbol3.Circles.AddRange(symbolsTable[i - 1][j].Circles);

                        symbol1.Circles.Add(symbolsTable[i - 1][j].Circles.Last());
                        symbol2.Circles.Add((Directions)((int)(symbolsTable[i - 1][j].Circles.Last() + 1) % 4));
                        symbol3.Circles.Add((Directions)((int)(symbolsTable[i - 1][j].Circles.Last() + 3) % 4));

                        if (symbol1.Status != Statuses.Loop) symbolsTable[i].Add(symbol1);
                        if (symbol2.Status != Statuses.Loop) symbolsTable[i].Add(symbol2);
                        if (symbol3.Status != Statuses.Loop) symbolsTable[i].Add(symbol3);
                    }
                    else
                    {
                        Symbol symbol1 = new Symbol(alef);
                        Symbol symbol2 = new Symbol(alef);
                        Symbol symbol3 = new Symbol(alef);
                        Symbol symbol4 = new Symbol(alef);

                        symbol1.Circles.AddRange(symbolsTable[i - 1][j].Circles);
                        symbol2.Circles.AddRange(symbolsTable[i - 1][j].Circles);
                        symbol3.Circles.AddRange(symbolsTable[i - 1][j].Circles);
                        symbol4.Circles.AddRange(symbolsTable[i - 1][j].Circles);

                        symbol1.Circles.Add(Directions.Up);
                        symbol2.Circles.Add(Directions.Down);
                        symbol3.Circles.Add(Directions.Left);
                        symbol4.Circles.Add(Directions.Right);
                        if (symbol1.Status != Statuses.Loop) symbolsTable[i].Add(symbol1);
                        if (symbol2.Status != Statuses.Loop) symbolsTable[i].Add(symbol2);
                        if (symbol3.Status != Statuses.Loop) symbolsTable[i].Add(symbol3);
                        if (symbol4.Status != Statuses.Loop) symbolsTable[i].Add(symbol4);
                    }
                }
            }
            for (int i = 0; i < symbolsTable.Length; i++)
            {
                symbolsTable[i].RemoveAll(symbol => !symbol.IsValid);
            }

            var symbols = new List<Symbol>();
            for (int i = 0; i < symbolsTable.Length; i++)
                symbols.AddRange(symbolsTable[i]);

            return symbols;
        }
    }
}
