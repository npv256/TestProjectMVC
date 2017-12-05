using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Domain;
using Service.Interfaces;
using LoginModels = UniversityControl.Models.LoginModels;
using System.Web.Security;

namespace UniversityControl.Controllers
{
    public class AccountController : Controller
    {
        readonly IStudentService<Student> _studService;
        readonly IService<Teacher> _teachService;
        private readonly IHash _hash;

        public AccountController(IService<Teacher> teachService, IStudentService<Student> studService, IHash hash)
        {
            _studService = studService;
            _hash = hash;
            _teachService = teachService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModels model)
        {
            if (ModelState.IsValid)
            {
                if (_studService.GetItemList().Select(s => s.Login).Contains(model.Name))
                {
                    Student student =
                        _studService.GetItemList()
                            .First(s => s.Login == model.Name && s.Password == _hash.GetHashString(model.Password));
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                }

                if (_teachService.GetItemList().Select(s => s.Login).Contains(model.Name))
                {
                    Teacher teacher =
                        _teachService.GetItemList()
                            .First(s => s.Login == model.Name && s.Password == _hash.GetHashString(model.Password));
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");                    
                }

                else
                {
                    ModelState.AddModelError("", "User with such login and password is not exist");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}