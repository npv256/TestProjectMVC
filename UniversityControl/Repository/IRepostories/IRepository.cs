﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepostories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetItemList();
        T GetItem(long id);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
        void Save();
    }
}
