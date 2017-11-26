using Repository.IRepostories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository.Contexts;

namespace Repository.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
       private UserContext _db;
        private StudentRepository _studentRepository;
        private TeacherRepository _teacherRepository;
        private ScienceRepository _scienceRepository;


        public UnitOfWork(UserContext userContext)
        {
            _db = userContext;
        }

        public IRepository<Student> Students
        {
            get
            {
                if (_studentRepository == null)
                    _studentRepository = new StudentRepository(_db);
                return _studentRepository;
            }
        }

        public IRepository<Teacher> Teachers
        {
            get
            {
                if (_teacherRepository == null)
                    _teacherRepository = new TeacherRepository(_db);
                return _teacherRepository;
            }
        }

        public IRepository<Science> Sciences
        {
            get
            {
                if (_scienceRepository == null)
                    _scienceRepository = new ScienceRepository(_db);
                return _scienceRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}