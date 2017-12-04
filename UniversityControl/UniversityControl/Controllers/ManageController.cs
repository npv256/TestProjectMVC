using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Service.Interfaces;
using UniversityControl.Models;

namespace UniversityControl.Controllers
{
    public class ManageController : Controller
    {
        readonly IStudentService<Student> _studService;
        readonly IService<Teacher> _teachService;

        public ManageController(IService<Teacher> teachService, IStudentService<Student> studService, IHash hash)
        {
            _studService = studService;
            _teachService = teachService;
        }

        [Authorize]
        public ActionResult Index()
        {
            if (_studService.GetItemList().Select(s => s.Login).Contains(User.Identity.Name))
            {
                var id = _studService.GetItemList().First(s => s.Login == User.Identity.Name).Id;
                return RedirectToAction("Details", "Student", new { @id = id });
            }

            if (_teachService.GetItemList().Select(s => s.Login).Contains(User.Identity.Name))
            {
                var id = _teachService.GetItemList().First(s => s.Login == User.Identity.Name).Id;
                return RedirectToAction("Details", "Teacher", new {@id = id});
            }

            return View();
        }

        public ActionResult Info()
        {
            InfoDadaModels infoNow = new InfoDadaModels()
            {
                Students = _studService.GetItemList().Count(),
                Teachers = _teachService.GetItemList().Count()
            };
            return PartialView(infoNow);
        }
    }
}