using Domain;
using Repository;
using Repository.IRepostories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ScienceService : IService<Science>
    {
        private readonly IRepository<Science> _db;

        public ScienceService(IRepository<Science> repository)
        {
            _db = repository;
        }

        public IEnumerable<Science> GetItemList()
        {
            return _db.GetItemList();
        }

        public Science GetItem(long id)
        {
            return _db.GetItem(id);
        }

        public void Create(Science item)
        {
            if(item.Students!=null && item.Students.Count!=0)
            {
                Random rnd = new Random();
                foreach (var student in item.Students)
                {
                    item.Rating.Add(student.Id, rnd.Next(1, 6));
                    // Генеририуем рандомные оценки студентам
                }
                _db.Create(item);
            }

        }

        public void Delete(long s)
        {
            _db.Delete(s);
        }


        public void Update(Science item)
        {
            _db.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }

    }
}
