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
        readonly IStudentService<Student> _studService;
        readonly IService<Teacher> _teachService;
        readonly IService<Science> _scieService;
        public TeacherController(IService<Teacher> teachService, IService<Science> scieService, IStudentService<Student> studService)
        {
            _studService = studService;
            _teachService = teachService;
            _scieService = scieService;
        }

        private TeacherDTO CreateTeacherDto(TeacherDTO teacher)
        {
                TeacherDTO teacherDto = new TeacherDTO();
            if (teacher != null)
            {
               teacherDto = teacher;
            }
            ScienceDTO scienceDto = new ScienceDTO();
            teacherDto = teacher;
            teacherDto.Science = scienceDto;
            var studentsDomain = _studService.GetItemList().ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentCheckModel>().MaxDepth(3));
            var mapper = config.CreateMapper();
            teacherDto.Science.Students.AddRange(mapper.Map<List<Student>, List<StudentCheckModel>>(studentsDomain));
            return teacherDto;
        }

        // GET: Teacher
        [Authorize(Roles = "admin")]
        [Authorize]
        public ActionResult Index()
        {
            return View(_teachService.GetItemList().Skip(1));
        }

        // GET: Teacher/Details/5
        [Authorize(Roles = "Teacher")]
        public ActionResult Details(long id)
        {
            Teacher teacherDomain = _teachService.GetItem(id);
            TeacherDTO teacherDto = new TeacherDTO();
            ScienceDTO scienceDto = new ScienceDTO();
            teacherDto.Science = scienceDto;
            teacherDto.FirstName = teacherDomain.FirstName;
            teacherDto.LastName = teacherDomain.LastName;
            teacherDto.Id = teacherDomain.Id;
            teacherDto.Login = teacherDomain.Login;
            teacherDto.Password = teacherDto.Password;
            teacherDto.Role = teacherDomain.Role;
            teacherDto.Science.Name = teacherDomain.Science.Name;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentCheckModel>().MaxDepth(3));
            var mapper = config.CreateMapper();
            teacherDto.Science.Students.AddRange(mapper.Map<List<Student>, List<StudentCheckModel>>(_scieService.GetItem(id).Students));
            foreach (var stud in teacherDto.Science.Students)
            {
                teacherDto.Science.Students.First(st => st.Id == stud.Id).Checked = true;
            }
            return View(teacherDto);
        }

        // GET: Teacher/Create
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
                TeacherDTO teacherDto = new TeacherDTO();
                return View(CreateTeacherDto(teacherDto));
        }

        // POST: Teacher/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,Password,FirstName,LastName,Science,Students")] TeacherDTO teacher)
        {
            try
            {

                var userLogins = _teachService.GetItemList().Select(t=>t.Login).ToList();
                userLogins.AddRange(_studService.GetItemList().Select(t => t.Login).ToList());
                if (userLogins.Contains(teacher.Login) && _teachService.GetItem(teacher.Id).Login!=teacher.Login)
                {
                    ModelState.AddModelError("Login", "User with such login already exists");
                    return View(CreateTeacherDto(teacher));
                }

                if (!teacher.Science.Students.Select(s=>s.Checked).Contains(true))
                {
                    ModelState.AddModelError("", "Not one student is selected");
                    return View(CreateTeacherDto(teacher));
                }

                var scienceNames = _scieService.GetItemList().Select(s => s.Name);
                if (scienceNames.Contains(teacher.Science.Name))
                {
                    ModelState.AddModelError("Science", "This science alredy exests");
                    return View(CreateTeacherDto(teacher));
                }

                if (ModelState.IsValid)
                {   
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TeacherDTO, Teacher>().ForMember(s=>s.Science,opt=>opt.Ignore()));
                    var mapper = config.CreateMapper();
                    Teacher someTeacher = mapper.Map<TeacherDTO, Teacher>(teacher);
                    Science someScience = new Science
                    {
                        Id = teacher.Science.Id,
                        Name = teacher.Science.Name
                    };
                    someTeacher.Science = someScience;
                    var checkedList = teacher.Science.Students.Where(st => st.Checked == true).ToList();
                    someTeacher.Science.Students = new List<Student>();
                    foreach (var student in checkedList)
                    {
                        someTeacher.Science.Students.Add(_studService.GetItem(student.Id));
                    }
                    _scieService.Create(someTeacher.Science);
                    _teachService.Create(someTeacher);
                    _teachService.Save();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch(AutoMapperConfigurationException e)
            {
                return View(teacher);
            }
        }

        // GET: Teacher/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long id)
        {
            ScienceDTO scienceDto = new ScienceDTO();
            Teacher teacherDomain = _teachService.GetItem(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Teacher, TeacherDTO>().ForMember(t=>t.Science,opt=>opt.Ignore()));
            var mapper = config.CreateMapper();
            TeacherDTO teacherDto = mapper.Map<Teacher, TeacherDTO>(teacherDomain);
            teacherDto.Password = "";
            teacherDto.Science = scienceDto;
            teacherDto.Science.Id = teacherDomain.Science.Id;
            teacherDto.Science.Name = teacherDomain.Science.Name;
            foreach (var stud in _studService.GetItemList())
            {
                StudentCheckModel studCheck = new StudentCheckModel
                {
                    Id = stud.Id,
                    FirstName = stud.FirstName,
                    LastName = stud.LastName
                };
                if (teacherDomain.Science.Students.Select(s => s.Id).Contains(stud.Id))
                    studCheck.Checked = true;
                teacherDto.Science.Students.Add(studCheck);
            }
            return View(teacherDto);

        }

        // POST: Teacher/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,FirstName,LastName,Science,Students")] TeacherDTO teacherDto)
        {
            try
            {
                if (!teacherDto.Science.Students.Select(s => s.Checked).Contains(true))
                {
                    ModelState.AddModelError("", "Not one student is selected");
                    return View(CreateTeacherDto(teacherDto));
                }
                if (ModelState.IsValid)
                    {
                        Teacher teacherDomain = _teachService.GetItem(teacherDto.Id);
                        teacherDomain.FirstName = teacherDto.FirstName;
                        teacherDomain.LastName = teacherDto.LastName;
                        teacherDomain.Password = teacherDto.Password;

                        Science scienceDomain = _scieService.GetItem(teacherDto.Id);
                        scienceDomain.Name = teacherDto.Science.Name;
                        scienceDomain.Students.Clear();
                        foreach (var stud in teacherDto.Science.Students.Where(s => s.Checked == true))
                        {
                            scienceDomain.Students.Add(_studService.GetItem(stud.Id));
                        }

                        teacherDomain.Science = scienceDomain;
                        scienceDomain.Teacher = teacherDomain;
                        _scieService.Update(scienceDomain);
                        _teachService.Update(teacherDomain);
                        _scieService.Save();
                        return RedirectToAction("Index");
                    }
            }
            catch (Exception e)
            {
                return View(CreateTeacherDto(teacherDto));
            }
            return View(teacherDto);
        }

        // GET: Teacher/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long id)
        {
                if (_teachService.GetItem(id) != null)
                    _teachService.Delete(id);
                _teachService.Save();
                return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReportAllStudents()
        {
            var countStudents = _studService.GetItemList().Count();
            List<Teacher> teachers = new List<Teacher>(_teachService.GetItemList()
                .Where(t => t.Science != null)
                .Where(t => t.Science.Students.Count == countStudents).ToList());
            return View(teachers);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ReportMinStudents()
        {
            List<Teacher> teachers = new List<Teacher>(_teachService.GetItemList().Where(t => t.Science != null)
                .OrderByDescending(t => t.Science.Students.Count).ToList());
            var countMin = teachers.Last().Science.Students.Count;
            teachers = teachers.Where(t => t.Science.Students.Count == countMin).ToList();
            return View(teachers);
        }
    }
}
