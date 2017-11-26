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
        private readonly IUnitOfWork _db;

        public StudentService(IUnitOfWork db)
        {
            _db = db;
        }

        public IEnumerable<Student> GetItemList()
        {
            return _db.Students.GetItemList();
        }

        public Student GetItem(long id)
        {
            return _db.Students.GetItem(id);
        }

        public void Create(Student item)
        {
            _db.Students.Create(item);
        }

        public void Delete(long s)
        {
            _db.Students.Delete(s);
        }


        public void Update(Student item)
        {
            _db.Students.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
