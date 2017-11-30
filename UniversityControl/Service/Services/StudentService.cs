using System;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository;
using Repository.IRepostories;

namespace Service.Services
{
    public class StudentService : IService<Student>
    {
        private readonly IUnitOfWork _db;

        public StudentService(IUnitOfWork db)
        {
            _db = db;
        }

        public IEnumerable<Student> GetItemList()
        {
            return _db.Students.GetItemList();
        }

        public Student GetItem(long id)
        {
            var s = getAverageBall(_db.Students.GetItem(id));
            return _db.Students.GetItem(id);
        }

        public void Create(Student item)
        {
            Hash hash = new Hash();
            Student student = new Student();
            student = item;
            student.Role = "Student";
            student.Password = hash.GetHashString(student.Password);
            _db.Students.Create(item);
        }

        public void Delete(long s)
        {
            _db.Students.Delete(s);
        }


        public void Update(Student item)
        {
            Hash hash = new Hash();
            Student student = new Student();
            student = item;
            student.Role = "Student";
            student.Password = hash.GetHashString(student.Password);
            foreach (var science in student.Sciences)
            {
                _db.Sciences.Update(science);
            }
            _db.Students.Update(item);
        }

        public double getAverageBall(Student student)
        {
            var summ = 0.0;
            var count = 0;
            var sciences = _db.Sciences.GetItemList().Where(sc => sc.Students.Exists(st => st.Id == student.Id)).ToList();
            foreach (var science in sciences)
            {
                summ += science.Marks.First(m=>m.Key==student.Id).Value;
                count++;
            }
            var result = summ/count;
            return Math.Round(result,2);
        }

        public void Save()
        {
            _db.Save();
        }
    }
}
