using System;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepostories;
using Domain;
using Repository;

namespace Service.Services
{
    public class TeacherService : IService<Teacher>
    {
        private IRepository<Teacher> _db;

        public TeacherService(IRepository<Teacher> repository)
        {
            _db = repository;
        }

        public IEnumerable<Teacher> GetItemList()
        {
            return _db.GetItemList();
        }

        public Teacher GetItem(long id)
        {
            return _db.GetItem(id);
        }

        public void Create(Teacher item)
        {
            _db.Create(item);
        }

        public void Delete(long s)
        {
            _db.Delete(s);
        }


        public void Update(Teacher item)
        {
            _db.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
