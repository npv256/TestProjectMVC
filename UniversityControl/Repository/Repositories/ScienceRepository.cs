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
        private readonly dbcontext _db;

        public ScienceRepository(dbcontext context)
        {
            _db = context;
        }

        public IEnumerable<Science> GetItemList()
        {
            return _db.Sciences;
        }

        public Science GetItem(int id)
        {
            return _db.Sciences.Find(id);
        }

        public Science GetItem(long id)
        {
            return _db.Sciences.Find(id);
        }

        public void Create(Science item)
        {
            _db.Sciences.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Science property = _db.Sciences.Find(id);
            if (property != null)
            {
                _db.Sciences.Remove(property);
                _db.SaveChanges();
            }
        }

        public void Update(Science item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

    }
}

