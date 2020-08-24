using Signs.Enums;
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
            if (list.Any(ssign => ssign.Value == sign.Value)) throw new Exception("You cannot have two sings with same numbers in same repo");
            list.Add(sign);
        }

        public void AddRange(IEnumerable<Sign> signs)
        {
            foreach (Sign sign in signs)
            {
                if (list.Any(ssign => ssign.Value == sign.Value)) throw new Exception("You cannot have two sings with same numbers in same repo");
                list.Add(sign);
            }
        }

        public void Delete(Sign sign)
        {
            list.Remove(sign);
        }

        public void Edit(Sign sign)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sign> GetAll()
        {
            return list;
        }

        public IEnumerable<Sign> GetByFamily(SignFamilies family)
        {
            return list.Where(sign => sign.Family == family);
        }

        public IEnumerable<Sign> GetByLength(int length)
        {
            return list.Where(sign => sign.Length == length);
        }

        public IEnumerable<Sign> GetByNumber(SignNumbers number)
        {
            return list.Where(sign => sign.Number == number);
        }

        public IEnumerable<Sign> GetByValue(int value)
        {
            return list.Where(sign => sign.Value == value);
        }
    }
}
