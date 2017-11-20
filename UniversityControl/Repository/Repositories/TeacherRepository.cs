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
   public class TeacherRepository : IRepository<Teacher>
    {
        private readonly dbcontext _db;

        private bool _disposed = false;

        public TeacherRepository(dbcontext context)
        {
            _db = context;
        }

        public IEnumerable<Teacher> GetItemList()
        {
            return _db.Teachers;
        }

        public Teacher GetItem(long id)
        {
            return _db.Teachers.Find(id);
        }

        public void Create(Teacher item)
        {
            _db.Teachers.Add(item);
        }

        public void Delete(long id)
        {
            Teacher Subject = _db.Teachers.Find(id);
            if (Subject != null)
            {
                _db.Teachers.Remove(Subject);
            }
        }

        public void Update(Teacher item)
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
