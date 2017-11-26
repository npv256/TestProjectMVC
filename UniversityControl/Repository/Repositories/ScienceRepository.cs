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
   public class ScienceRepository : IRepository<Science>
    {
        private readonly UserContext _db;

        private bool _disposed = false;

        public ScienceRepository(UserContext context)
        {
            _db = context;
            _db.Sciences.Load();
        }

        public IEnumerable<Science> GetItemList()
        {
            return _db.Sciences.Include(s => s.Students).ToList();
        }

        public Science GetItem(long id)
        {
            return _db.Sciences.Find(id);
        }

        public void Create(Science item)
        {
            _db.Sciences.Add(item);
        }

        public void Delete(long id)
        {
            Science subject = _db.Sciences.Find(id);
            if (subject != null)
            {
                _db.Sciences.Remove(subject);
            }
        }

        public void Update(Science item)
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

