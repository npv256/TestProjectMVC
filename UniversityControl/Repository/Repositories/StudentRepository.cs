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
        private readonly UserContext _db;

        private bool _disposed = false;

        public StudentRepository(UserContext context)
        {
            _db = context;
            _db.Students.Load();
        }

        public IEnumerable<Student> GetItemList()
        {
            return _db.Students.Include(s => s.Sciences).ToList();
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
            Student subject = _db.Students.Find(id);
            if (subject != null)
            {
                _db.Students.Remove(subject);
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
