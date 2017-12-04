using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStudentService<T> where T : class
    {
        IEnumerable<T> GetItemList();
        T GetItem(long id);
        double GetAverageBall(T item);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
        void Save();
    }
}
