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
        private readonly IUnitOfWork _db;

        public ScienceService(IUnitOfWork db)
        {
            _db = db;
        }

        public IEnumerable<Science> GetItemList()
        {
            return _db.Sciences.GetItemList();
        }

        public Science GetItem(long id)
        {
            return _db.Sciences.GetItem(id);
        }

        public void Create(Science science)
        {
            if(science.Students!=null && science.Students.Count!=0)
            {
                List<Student> selectedStudents =new List<Student>();
                selectedStudents.AddRange(science.Students); 
                science.Students.Clear();
                Random rnd = new Random();
                foreach (var student in selectedStudents)
                {
                    science.Rating.Add(student.Id, rnd.Next(1, 6));
                    // Генеририуем рандомные оценки студентам.
                    Student editStud = new Student();
                    editStud = _db.Students.GetItem(student.Id);
                    editStud.Sciences.Add(science);
                    science.Students.Add(editStud);
                    _db.Students.Update(editStud);
                    var s = _db.Students.GetItemList().ToList();
                }
                _db.Sciences.Create(science);
            }

        }

        public void Delete(long s)
        {
            _db.Sciences.Delete(s);
        }


        public void Update(Science item)
        {
            _db.Sciences.Update(item);
        }

        public void Save()
        {
            _db.Save();
        }

    }
}
