using System;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepostories;
using Domain;
using Repository;
using Repository.Contexts;
using Repository.Repositories;

namespace Service.Services
{
    public class TeacherService : IService<Teacher>
    {

        //IRepository<Teacher> _db
        private readonly IUnitOfWork _db;

        public TeacherService(IUnitOfWork uof)
        {
            _db = uof;
        }

        public IEnumerable<Teacher> GetItemList()
        {
            return _db.Teachers.GetItemList();
        }

        public Teacher GetItem(long id)
        {
            return _db.Teachers.GetItem(id);
        }

        public void Create(Teacher teacher)
        {
            var s2 = _db.Students.GetItemList().ToList();
            Hash hashObj = new Hash();
            Teacher someTeacher = teacher;
            someTeacher.Password = hashObj.GetHashString(teacher.Password);
            someTeacher.Role = "Teacher";
            someTeacher.Science = _db.Sciences.GetItem(teacher.Id);
            _db.Teachers.Create(someTeacher);
            var s1 = _db.Students.GetItemList().ToList();
        }

        public void Delete(long s)
        {
            _db.Teachers.Delete(s);
        }


        public void Update(Teacher item)
        {
            _db.Teachers.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
