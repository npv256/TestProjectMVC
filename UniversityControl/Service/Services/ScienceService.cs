using Domain;
using Repository;
using Repository.IRepostories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ScienceService : IService<Science>
    {
        private IRepository<Science> _db;

        public ScienceService(IRepository<Science> repository)
        {
            _db = repository;
        }

        public IEnumerable<Science> GetItemList()
        {
            return _db.GetItemList();
        }

        public Science GetItem(long id)
        {
            return _db.GetItem(id);
        }

        public void Create(Science item)
        {
            _db.Create(item);
        }

        public void Delete(long s)
        {
            _db.Delete(s);
        }


        public void Update(Science item)
        {
            _db.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }

    }
}
