using Domain;
using Repository.Contexts;
using Repository.IRepostories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
   public class StudentRepository : IRepository<Student>
    {
        private readonly dbcontext _db;

        private bool _disposed = false;

        public StudentRepository(dbcontext context)
        {
            _db = context;
        }

        public IEnumerable<Student> GetItemList()
        {
            return _db.Students;
        }

        public Student GetItem(long id)
        {
            return _db.Students.Find(id);
        }

        public void Create(Student item)
        {
            _db.Students.Add(item);
        }

        public void Delete(long id)
        {
            Student Subject = _db.Students.Find(id);
            if (Subject != null)
            {
                _db.Students.Remove(Subject);
            }
        }

        public void Update(Student item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
