using Signs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Interfaces
{
    public interface ISymbolRepository
    {
        IEnumerable<Symbol> GetAll();

        Symbol GetById(int id);

        IEnumerable<Symbol> GetByAlef(int alef);

        IEnumerable<Symbol> GetByLength(int length);

        IEnumerable<Symbol> GetByValue(int value);

        void Add(Symbol sign);

        void AddRange(IEnumerable<Symbol> symbols);

        void Edit(Symbol sign);

        void Delete(Symbol sign);

        IEnumerable<Sign> ToSigns();
    }
}
