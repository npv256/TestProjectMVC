using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using PagedList;
using Service.Interfaces;
using UniversityControl.Models;

namespace UniversityControl.Controllers
{
    public class HomeController : Controller
    {
        readonly IStudentService<Student> _studService;
        readonly IService<Teacher> _teachService;
        readonly IService<Science> _scieService;

        public HomeController(IService<Teacher> teachService, IService<Science> scieService, IStudentService<Student> studService)
        {
            _studService = studService;
            _teachService = teachService;
            _scieService = scieService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentListIndex(string sortOrder, string currentFilter, string searchString,  string minFilter , string maxFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName desc" : "LastName";
            ViewBag.AverageBalSortParm = sortOrder == "AverageBal" ? "AverageBal desc" : "AverageBal";
            ViewBag.MinFilter = minFilter;
            ViewBag.MaxFilter = maxFilter;
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;
            var students = _studService.GetItemList();
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!String.IsNullOrEmpty(minFilter) && !String.IsNullOrEmpty(maxFilter))
                students =
                students.Where(s => s.AverageBal >= double.Parse(minFilter) && s.AverageBal <= double.Parse(maxFilter));
            switch (sortOrder)
            {
                case "FirstName desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "AverageBal":
                    students = students.OrderBy(s => s.AverageBal);
                    break;
                case "AverageBal desc":
                    students = students.OrderByDescending(s => s.AverageBal);
                    break;
                case "LastName":
                    students = students.OrderBy(s => s.LastName);
                    break;
                case "LastName desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageIndex = (page ?? 1);
            return View(students.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult TeacherListIndex(string sortOrder, string currentFilter, string searchString, string minFilter, string maxFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName desc" : "LastName";
            ViewBag.CountSortParm = sortOrder == "Count" ? "Count desc" : "Count";
            ViewBag.ScienceSortParm = sortOrder == "Science" ? "Science desc" : "Science";
            ViewBag.MinFilter = minFilter;
            ViewBag.MaxFilter = maxFilter;
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;
            var teachers = _teachService.GetItemList().Skip(1);
            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstName.ToUpper().Contains(searchString.ToUpper()) || s.Science.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!String.IsNullOrEmpty(minFilter) && !String.IsNullOrEmpty(maxFilter))
                teachers =
                    teachers.Where(s => s.Science.Students.Count >= double.Parse(minFilter) && s.Science.Students.Count <= double.Parse(maxFilter));
            switch (sortOrder)
            {
                case "FirstName desc":
                    teachers = teachers.OrderByDescending(s => s.FirstName);
                    break;
                case "Count":
                    teachers = teachers.OrderBy(s => s.Science.Students.Count);
                    break;
                case "Count desc":
                    teachers = teachers.OrderByDescending(s => s.Science.Students.Count);
                    break;
                case "Science":
                    teachers = teachers.OrderBy(s => s.Science.Name);
                    break;
                case "Science desc":
                    teachers = teachers.OrderByDescending(s => s.Science.Name);
                    break;
                case "LastName":
                    teachers = teachers.OrderBy(s => s.LastName);
                    break;
                case "LastName desc":
                    teachers = teachers.OrderByDescending(s => s.LastName);
                    break;
                default:
                    teachers = teachers.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageIndex = (page ?? 1);
            return View(teachers.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}