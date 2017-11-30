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
            Hash hashObj = new Hash();
            Teacher someTeacher = teacher;
            someTeacher.Password = hashObj.GetHashString(teacher.Password);
            someTeacher.Role = "Teacher";
            someTeacher.Science = _db.Sciences.GetItem(teacher.Id);
            _db.Teachers.Create(someTeacher);
        }

        public void Delete(long s)
        {
            _db.Teachers.Delete(s);
        }

        public void Update(Teacher teacher)
        {
            Hash hash = new Hash();
            teacher.Role = "Teacher";
            teacher.Password =  hash.GetHashString(teacher.Password);
            _db.Teachers.Update(teacher);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
