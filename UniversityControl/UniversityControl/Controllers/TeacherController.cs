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
            TeacherModels teacher = new TeacherModels();
            teacher.Students = _studService.GetItemList().ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, CheckStudentModel>());
            var mapper = config.CreateMapper();
            teacher.StudentsCheck = mapper.Map<List<Student>, List<CheckStudentModel>>(teacher.Students);
            return View(teacher);
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TeacherModels teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Science someScience = new Science();
                    foreach (var checkStud in teacher.StudentsCheck.Where(sc=>sc.Checked==true))
                    {
                        teacher.Students.Add(_studService.GetItem(checkStud.Id));
                    }
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TeacherModels, Teacher>());
                    var mapper = config.CreateMapper();
                    Teacher someTeacher = mapper.Map<TeacherModels, Teacher>(teacher);
                    someScience.Name = teacher.ScienceName;
                    someScience.Students = teacher.Students;
                    someScience.Teacher = someTeacher;
                    someTeacher.Science = someScience;
                    _teachService.Create(someTeacher);
                    _scieService.Create(someScience);
                    _scieService.Save();
                    _teachService.Save();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
               // throw new Exception(e.Message);
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
