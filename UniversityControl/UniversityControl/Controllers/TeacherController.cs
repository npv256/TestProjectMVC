using AutoMapper;
using Domain;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UniversityControl.Models;

namespace UniversityControl.Controllers
{
    public class TeacherController : Controller
    {
        readonly IService<Student> _studService;
        readonly IService<Teacher> _teachService;
        readonly IService<Science> _scieService;
        public TeacherController(IService<Teacher> teachService, IService<Science> scieService, IService<Student> studService)
        {
            _studService = studService;
            _teachService = teachService;
            _scieService = scieService;
        }

        // GET: Teacher
        public ActionResult Index()
        {
            _studService.GetItemList();
            var asd = _scieService.GetItemList().Last();
            var ssq = _teachService.GetItemList().Last();
          var s =  _studService.GetItemList().ToList();
          var s2 =  _scieService.GetItemList().ToList();
            var s3 = _teachService.GetItemList();
            return View(_teachService.GetItemList());
        }

        // GET: Teacher/Details/5
        public ActionResult Details(long id)
        {

            return View();
        }

        // GET: Teacher/Create
        [HttpGet]
        public ActionResult Create()
        {
            TeacherDTO teacher = new TeacherDTO();
            ScienceDTO science = new ScienceDTO();
            teacher.Science = science;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());
            var mapper = config.CreateMapper();
            teacher.Science.Students = mapper.Map<List<Student>, List<StudentDTO>>(_studService.GetItemList().ToList());
            return View(teacher);
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TeacherDTO teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    teacher.Science.Students = teacher.Science.Students.Where(st => st.Checked == true).ToList();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>()
                    .ForMember("Science", opt => opt.MapFrom(c => c.Science))
                    );
                    var mapper = config.CreateMapper();
                    Teacher someTeacher = mapper.Map<TeacherDTO, Teacher>(teacher);
                    someTeacher.Science.Teacher = someTeacher;
                    _scieService.Create(someTeacher.Science);
                    _teachService.Create(someTeacher);
                    _teachService.Save();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View(teacher);
            }
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(long id)
        {
            TeacherDTO teacher = new TeacherDTO();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>());
            var mapper = config.CreateMapper();
            teacher = mapper.Map<Teacher, TeacherDTO>(_teachService.GetItem(id));
            config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());
            mapper = config.CreateMapper();
            teacher.Science.Students = mapper.Map<List<Student>, List<StudentDTO>>(_studService.GetItemList().ToList());
            foreach (var stud in _scieService.GetItem(id).Students)
            {
                teacher.Science.Students.First(st => st.Id == stud.Id).Checked = true;
            }
            teacher.Password = "";
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        public ActionResult Edit(TeacherDTO teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    teacher.Science.Students = teacher.Science.Students.Where(st => st.Checked == true).ToList();
                    Teacher someTeacher = _teachService.GetItem(teacher.Id);
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>());
                    var mapper = config.CreateMapper();
                    someTeacher = mapper.Map<TeacherDTO, Teacher>(teacher);
                  //  someTeacher.Role = _teachService.GetItemList().First(t => t.Login == someTeacher.Login).Role;
                    someTeacher.Science.Teacher = someTeacher;
                    someTeacher.Science.Id = someTeacher.Id;
                    _scieService.Update(someTeacher.Science);
                    _teachService.Update(someTeacher);
                    _teachService.Save();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                return View(teacher);
            }
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(long id)
        {
            try
            {
                if (_teachService.GetItem(id) != null)
                    _teachService.Delete(id);
                _teachService.Save();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
