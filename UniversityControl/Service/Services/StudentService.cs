using System;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository;
using Repository.IRepostories;

namespace Service.Services
{
    public class StudentService : IService<Student>
    {
        private IRepository<Student> _db;

        public StudentService(IRepository<Student> repository)
        {
            _db = repository;
        }

        public IEnumerable<Student> GetItemList()
        {
            return _db.GetItemList();
        }

        public Student GetItem(long id)
        {
            return _db.GetItem(id);
        }

        public void Create(Student item)
        {
            _db.Create(item);
        }

        public void Delete(long s)
        {
            _db.Delete(s);
        }


        public void Update(Student item)
        {
            _db.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
