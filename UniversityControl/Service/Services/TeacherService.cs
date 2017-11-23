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
        private readonly IRepository<Teacher> _dbTeacher;

        public TeacherService(IRepository<Teacher> reposTeacher)
        {
            _dbTeacher = reposTeacher;
        }

        public IEnumerable<Teacher> GetItemList()
        {
            return _dbTeacher.GetItemList();
        }

        public Teacher GetItem(long id)
        {
            return _dbTeacher.GetItem(id);
        }

        public void Create(Teacher teacher)
        {
            Hash hashObj = new Hash();
            Teacher someTeacher = teacher;
            Science someScience = new Science();
            someTeacher.Password = hashObj.GetHashString(teacher.Password);
            someTeacher.Role = "Teacher";
            _dbTeacher.Create(someTeacher);
        }

        public void Delete(long s)
        {
            _dbTeacher.Delete(s);
        }


        public void Update(Teacher item)
        {
            _dbTeacher.Update(item);
        }

        public void Save()
        {
            _dbTeacher.Save();
        }
    }
}
