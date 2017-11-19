using Repository.Contexts;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class UnitOfWork : IDisposable
    {
        private dbcontext _db = new dbcontext();
        private ScienceRepository scienceRepository;
        private StudentRepository studentRepository;
        private TeacherRepository teacherRepository;

        public ScienceRepository Sciences
        {
            get
            {
                if (scienceRepository == null)
                    scienceRepository = new ScienceRepository(_db);
                return scienceRepository;
            }
        }

        public StudentRepository Students
        {
            get
            {
                if (studentRepository == null)
                    studentRepository = new StudentRepository(_db);
                return studentRepository;
            }
        }

        public TeacherRepository Teachers
        {
            get
            {
                if (teacherRepository == null)
                    teacherRepository = new TeacherRepository(_db);
                return teacherRepository;
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
