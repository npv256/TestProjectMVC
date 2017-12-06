using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain;
using Microsoft.Ajax.Utilities;
using Service.Interfaces;
using UniversityControl.Models;

namespace UniversityControl.Controllers
{
    public class StudentController : Controller
    {

        readonly IStudentService<Student> _studService;
        readonly IService<Teacher> _teachService;
        readonly IService<Science> _scieService;

        public StudentController(IService<Teacher> teachService, IService<Science> scieService, IStudentService<Student> studService)
        {
            _studService = studService;
            _teachService = teachService;
            _scieService = scieService;
        }


        private StudentDTO CreateStudentDto(StudentDTO student)
        {
            StudentDTO studentDto = new StudentDTO();
            if (student != null)
            {
                studentDto = student;
            }
            var sciencesDomain = _scieService.GetItemList().ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Science, ScienceCheckModel>());
            var mapper = config.CreateMapper();
            studentDto.Sciences = new List<ScienceCheckModel>(mapper.Map<List<Science>, List<ScienceCheckModel>>(sciencesDomain));
            return studentDto;
        }

        // GET: Student
        [Authorize(Roles = "Teacher,admin")]
        public ActionResult Index()
        {
            return View(_studService.GetItemList());
        }

        // GET: Student/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>().ForMember(s => s.Sciences, opt => opt.Ignore()));
            var mapper = config.CreateMapper();
            StudentDTO studentDto = mapper.Map<Student, StudentDTO>(_studService.GetItem(id));
            config = new MapperConfiguration(cfg => cfg.CreateMap<Science, ScienceCheckModel>());
            mapper = config.CreateMapper();
            studentDto.Sciences = mapper.Map<List<Science>, List<ScienceCheckModel>>(_scieService.GetItemList().ToList());
            studentDto.Password = "";
            foreach (var science in _studService.GetItem(id).Sciences)
            {
                studentDto.Sciences.First(s => s.Id == science.Id).Checked = true;
            }
            return View(studentDto);
        }

        // GET: Student/Create
        [Authorize(Roles = "Teacher,admin")]
        public ActionResult Create()
        {
                StudentDTO studentDto = new StudentDTO();
                return View(CreateStudentDto(studentDto));
        }

        // POST: Student/Create
        [HttpPost]
        [Authorize(Roles = "Teacher,admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,Password,FirstName,LastName,Sciences")]StudentDTO studentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogins = _studService.GetItemList().Select(t => t.Login).ToList();
                    userLogins.AddRange(_teachService.GetItemList().Select(t => t.Login).ToList());
                    if (userLogins.Contains(studentDto.Login))
                    {
                        ModelState.AddModelError("Login", "User with such login already exists");
                        return View(CreateStudentDto(studentDto));
                    }
                    if (!studentDto.Sciences.Select(s => s.Checked).Contains(true))
                    {
                        ModelState.AddModelError("", "Not one student is selected");
                        return View(CreateStudentDto(studentDto));
                    }

                    studentDto.Sciences = studentDto.Sciences.Where(s => s.Checked == true).ToList();
                    var config =
                        new MapperConfiguration(
                            cfg => cfg.CreateMap<StudentDTO, Student>().ForMember(s => s.Sciences, opt => opt.Ignore()));
                    var mapper = config.CreateMapper();
                    Student studentDomain = mapper.Map<StudentDTO, Student>(studentDto);
                    studentDomain.Sciences.Clear();
                    foreach (var science in studentDto.Sciences)
                    {
                        studentDomain.Sciences.Add(_scieService.GetItem(science.Id));
                    }
                    _studService.Create(studentDomain);
                    _studService.Save();
                    return RedirectToAction("Index");
                }
                return View(CreateStudentDto(studentDto));
            }
            catch(Exception e)
            {
                return View(CreateStudentDto(studentDto));
            }
        }

        // GET: Student/Edit/5
        [Authorize(Roles = "Teacher,admin")]
        public ActionResult Edit(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>().ForMember(s => s.Sciences, opt => opt.Ignore()));
            var mapper = config.CreateMapper();
            StudentDTO studentDto = mapper.Map<Student, StudentDTO>(_studService.GetItem(id));
            config = new MapperConfiguration(cfg => cfg.CreateMap<Science, ScienceCheckModel>());
            mapper = config.CreateMapper();
            studentDto.Sciences = mapper.Map<List<Science>, List<ScienceCheckModel>>(_scieService.GetItemList().ToList());
            studentDto.Password = "";
            foreach (var science in _studService.GetItem(id).Sciences)
            {
                studentDto.Sciences.First(s => s.Id == science.Id).Checked = true;
            }
            return View(studentDto);
        }

        // POST: Student/Edit/5
        [Authorize(Roles = "Teacher,admin")]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,FirstName,LastName,Sciences")]StudentDTO studentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogins = _studService.GetItemList().Select(t => t.Login).ToList();
                    userLogins.AddRange(_teachService.GetItemList().Select(t => t.Login).ToList());
                    if (userLogins.Contains(studentDto.Login)&&_studService.GetItem(studentDto.Id).Login!=studentDto.Login)
                    {
                        ModelState.AddModelError("Login", "User with such login already exists");
                        return View(CreateStudentDto(studentDto));
                    }
                    if (!studentDto.Sciences.Select(s => s.Checked).Contains(true))
                    {
                        ModelState.AddModelError("", "Not one student is selected");
                        return View(CreateStudentDto(studentDto));
                    }
                    studentDto.Sciences = studentDto.Sciences.Where(s => s.Checked == true).ToList();
                    Student studentDomain = _studService.GetItem(studentDto.Id);
                    studentDomain.Password = studentDto.Password;
                    studentDomain.Login = studentDto.Login;
                    studentDomain.FirstName = studentDto.FirstName;
                    studentDomain.LastName = studentDto.LastName;
                    studentDomain.Sciences.Clear();
                    foreach (var science in studentDto.Sciences)
                    {
                        studentDomain.Sciences.Add(_scieService.GetItem(science.Id));
                    }
                    _studService.Update(studentDomain);
                    _studService.Save();
                    return RedirectToAction("Index");
                }
                return View(CreateStudentDto(studentDto));
            }
            catch (Exception e)
            {
                return View(CreateStudentDto(studentDto));
            }
        }

        [Authorize(Roles = "Teacher,admin")]
        public ActionResult Delete(int id)
        {
            if (_studService.GetItem(id) != null)
                _studService.Delete(id);
            _studService.Save();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Teacher,admin")]
        public ActionResult ReportAverageBal()
        {
            var allAverageBals = _studService.GetItemList().Sum(s => s.AverageBal)/_studService.GetItemList().Count();
            var goodStudents = _studService.GetItemList().Where(t => t.AverageBal > allAverageBals).ToList();
            return View(goodStudents);
        }
    }
}
