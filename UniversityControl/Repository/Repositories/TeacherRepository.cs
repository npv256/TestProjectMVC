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
   public class TeacherRepository
    {
        private readonly dbcontext _db;

        public TeacherRepository(dbcontext context)
        {
            _db = context;
        }

        public IEnumerable<Teacher> GetItemList()
        {
            return _db.Teachers;
        }

        public Teacher GetItem(int id)
        {
            return _db.Teachers.Find(id);
        }

        public Teacher GetItem(long id)
        {
            return _db.Teachers.Find(id);
        }

        public void Create(Teacher item)
        {
            _db.Teachers.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Teacher Subject = _db.Teachers.Find(id);
            if (Subject != null)
            {
                _db.Teachers.Remove(Subject);
                _db.SaveChanges();
            }
        }

        public void Update(Teacher item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
