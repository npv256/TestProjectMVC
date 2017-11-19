using Domain;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
   public class StudentRepository
    {
        private readonly dbcontext _db;

        public StudentRepository(dbcontext context)
        {
            _db = context;
        }

        public IEnumerable<Student> GetItemList()
        {
            return _db.Students;
        }

        public Student GetItem(int id)
        {
            return _db.Students.Find(id);
        }

        public Student GetItem(long id)
        {
            return _db.Students.Find(id);
        }

        public void Create(Student item)
        {
            _db.Students.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Student subject = _db.Students.Find(id);
            if (subject != null)
            {
                _db.Students.Remove(subject);
                _db.SaveChanges();
            }
        }

        public void Update(Student item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

    }
}
