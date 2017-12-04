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
            List<Student> checkList = new List<Student>(science.Students);
            science.Students.Clear();
            if(checkList.Count != 0)
            {
                Random rnd = new Random();
                science.Marks = new List<Mark>();
                foreach (var student in checkList)
                {
                    // Генеририуем рандомные оценки студентам.
                    science.Marks.Add(new Mark()
                    {
                         Key = student.Id,
                         Value = rnd.Next(1, 6),
                    });
                    var editStud = _db.Students.GetItem(student.Id);
                    editStud.Sciences.Add(science);
                    science.Students.Add(editStud);
                    _db.Students.Update(editStud);
                }
                _db.Sciences.Create(science);
            }

        }

        public void Delete(long s)
        {
            _db.Sciences.Delete(s);
        }


        public void Update(Science science)
        {
            Random rnd = new Random();
            if (science.Marks.Count == 0)
                science.Marks = _db.Sciences.GetItem(science.Id).Marks;
            foreach (var stud in science.Students)
            {
                if(science.Marks.First(m=>m.Key==stud.Id)==null)
                {
                    science.Marks.Add(new Mark()
                    {
                        Key = stud.Id,
                        Value = rnd.Next(1, 6)
                    });
                    _db.Students.Update(stud);
                }

            }
            _db.Sciences.Update(science);
        }

        public void Save()
        {
            _db.Save();
        }

    }
}
