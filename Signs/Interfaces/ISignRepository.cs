using Signs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Interfaces
{
    public interface ISignRepository
    {
        IEnumerable<Sign> GetAll();

        Sign GetById(int id);

        IEnumerable<Sign> GetByAlef(int alef);

        IEnumerable<Sign> GetByLength(int length);

        IEnumerable<Sign> GetByValue(int value);

        void Add(Sign sign);

        void Edit(Sign sign);

        void Delete(Sign sign);
    }
}
