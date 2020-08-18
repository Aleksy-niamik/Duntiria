using Signs.Interfaces;
using Signs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Repositories
{
    public class SignRepository : ISignRepository
    {
        private List<Sign> list;

        public SignRepository()
        {
            list = new List<Sign>();
        }
        public void Add(Sign sign)
        {
            if(sign.IsValid)
            {
                sign.Id = getFreeId();
                list.Add(sign);
            }
        }

        public void Delete(Sign sign)
        {
            throw new NotImplementedException();
        }

        public void Edit(Sign sign)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sign> GetAll()
        {
            return list;
        }

        public IEnumerable<Sign> GetByAlef(int alef)
        {
            return list.Where(sign => sign.Alef == alef);
        }

        public Sign? GetById(int id)
        {
            return list.FirstOrDefault(sign => sign.Id == id);
        }

        public IEnumerable<Sign> GetByLength(int length)
        {
            return list.Where(sign => sign.Length == length);
        }

        public IEnumerable<Sign> GetByValue(int value)
        {
            return list.Where(sign => sign.Value == value);
        }

        private int getFreeId()
        {
            int i = 0;
            while (list.Select(sign => sign.Id).Contains(i++)) ;
            return i;
        }
    }
}
