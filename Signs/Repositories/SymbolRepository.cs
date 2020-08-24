using Signs.Interfaces;
using Signs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Repositories
{
    public class SymbolRepository : ISymbolRepository
    {
        private List<Symbol> list;

        public SymbolRepository()
        {
            list = new List<Symbol>();
        }
        public void Add(Symbol symbol)
        {
            if(symbol.IsValid)
            {
                symbol.Id = getFreeId();
                list.Add(symbol);
            }
        }

        public void AddRange(IEnumerable<Symbol> symbols)
        {
            foreach(Symbol symbol in symbols)
            {
                if (symbol.IsValid)
                {
                    symbol.Id = getFreeId();
                    list.Add(symbol);
                }
            }
        }

        public void Delete(Symbol symbol)
        {
            throw new NotImplementedException();
        }

        public void Edit(Symbol symbol)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Symbol> GetAll()
        {
            return list;
        }

        public IEnumerable<Symbol> GetByAlef(int alef)
        {
            return list.Where(symbol => symbol.Alef == alef);
        }

        public Symbol? GetById(int id)
        {
            return list.FirstOrDefault(symbol => symbol.Id == id);
        }

        public IEnumerable<Symbol> GetByLength(int length)
        {
            return list.Where(symbol => symbol.Length == length);
        }

        public IEnumerable<Symbol> GetByValue(int value)
        {
            return list.Where(symbol => symbol.Value == value);
        }

        public IEnumerable<Sign> ToSigns()
        {
            var signs = new List<Sign>();
            list.ForEach(symbol => 
            {
                var signWithSameValue = signs.FirstOrDefault(sign => sign.Value == symbol.Value);
                if (signWithSameValue == null)
                {
                    signs.Add(new Sign(symbol.Value));
                    signs.Last().Add(symbol);
                }
                else
                {
                    signWithSameValue.Add(symbol);
                }    
            });

            signs.ForEach(sign => sign.Symbols.Sort((symbol1,symbol2) => symbol1.Length - symbol2.Length));

            return signs;
        }

        private int getFreeId()
        {
            int i = 0;
            var random = new Random();
            while (list.Select(symbol => symbol.Id).Contains(i))
            {
                i = random.Next();
            }
            return i;
        }
    }
}
