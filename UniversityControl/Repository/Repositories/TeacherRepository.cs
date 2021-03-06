﻿using Domain;
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
        private readonly UserContext _db;

        private bool _disposed = false;

        public TeacherRepository(UserContext context)
        {
            _db = context;
        }

        public IEnumerable<Teacher> GetItemList()
        {
            return _db.Teachers.Include(s=>s.Science).Include(s=>s.Science.Students);
        }

        public Teacher GetItem(long id)
        {
            var teachers = _db.Teachers.Include(s => s.Science).Include(s => s.Science.Students);
            return teachers.First(teacher =>teacher.Id == id);
        }

        public void Create(Teacher item)
        {
            _db.Teachers.Add(item);
        }

        public void Delete(long id)
        {
            Teacher subject = _db.Teachers.Find(id);
            if (subject != null)
            {
                _db.Teachers.Remove(subject);
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
