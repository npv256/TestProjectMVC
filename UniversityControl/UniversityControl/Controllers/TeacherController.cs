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
            var s = _teachService.GetItemList().Last();
            var s1 = _studService.GetItemList().Count();
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
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>());
                    var mapper = config.CreateMapper();
                    Teacher someTeacher = mapper.Map<TeacherDTO, Teacher>(teacher);
                    someTeacher.Science.Teacher = someTeacher;
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
            return View();
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        public ActionResult Edit(long id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(long id)
        {
            return View();
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        public ActionResult Delete(long id, FormCollection collection)
        {
            try
            {
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
