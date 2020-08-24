using Signs.Enums;
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

        IEnumerable<Sign> GetByValue(int value);

        IEnumerable<Sign> GetByFamily(SignFamilies family);

        IEnumerable<Sign> GetByNumber(SignNumbers number);

        IEnumerable<Sign> GetByLength(int length);

        void Add(Sign sign);

        void AddRange(IEnumerable<Sign> signs);

        void Edit(Sign sign);

        void Delete(Sign sign);
    }
}
