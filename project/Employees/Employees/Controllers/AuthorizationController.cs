using Employees.BL.Manager;
using Employees.Models;
using Employees.Models.Autorisation;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Employees.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ValidateUserAction(string Login, string Password)
        {
            //if (string.IsNullOrEmpty(Login))
            //{
            //    ViewBag.Success = false;
            //    ViewBag.Message = "Введите логин и пароль";
            //    return View("Index");
            //}
            //Result result = UserManager.CheckValidationUser(Login, Password);
            //if (result.Success && result.ResultObject != null)
            //{
            //    ViewBag.Success = true;
            //    FormsAuthentication.SetAuthCookie((result.ResultObject as User).Login, true);
            //}
            //else
            //    ViewBag.Success = false;
            //ViewBag.Message = result.Message;
            return View("Index");
        }

        [HttpGet]
        public ActionResult RegistrationUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationUser(RegisterUser user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (user == null)
            {
                throw new Exception();
            }
            var result = UserManager.GetManager.Save(user);
            if (!result.Success)
                return HttpNotFound();
            return View();

        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;

                var result = UserManager.GetManager.GetUserByNameAndPassword(model.Name, model.Password);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                user = result.ResultEntity;
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUser model)
        {
            //if (ModelState.IsValid)
            //{
            //    User user = null;
            //    Result result = UserManager.CheckValidationUser(model.Name, model.Password);
            //    if (!result.Success)
            //    {
            //        ModelState.AddModelError("", result.Message);
            //        return View(model);
            //    }
            //    user = result.ResultObject as User;
            //    if (user == null)
            //    {
            //        // создаем нового пользователя

            //        UserManager.GetManager().Update(model);


            //        Result result1 = UserManager.CheckValidationUser(model.Name, model.Password);
            //        if (!result.Success)
            //        {
            //            ModelState.AddModelError("", result1.Message);
            //            return View(model);
            //        }
            //        user = result1.ResultObject as User;
            //        // если пользователь удачно добавлен в бд
            //        if (user != null)
            //        {
            //            FormsAuthentication.SetAuthCookie(user.Login, true);
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            //    }
            //}

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }




    }
}