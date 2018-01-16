using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employees.BL;
using API;
using BL;
using API.Model;

namespace Employees.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        // GET: Home
        //public ActionResult Index()
        //{
        //    return View("~/Views/Authorization/Index.cshtml");
        //}
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Authorization/Index.cshtml");
        }
        #region Company

        [HttpPost]
        public ActionResult EditCompany(ICompany company)
        {
            CompanyManager.GetManager().Save(company);
            return RedirectToAction("ShowCompanys");
        }

        [HttpGet]
        public ActionResult EditCompany(int? idCompany)
        {
            var result =CompanyManager.GetManager().LoadEntity((idCompany));
            if (!result.Success)
                return View();
            
            return View(result.ResultEntity);
        }

        public ActionResult ShowCompanys()
        {
            var result = CompanyManager.GetManager().LoadEntityList(null);
            if (!result.Success)
                return HttpNotFound();
            return View(result.Entitys);
        }

        public ActionResult DeleteCompany(int idCompany)
        {
            var result = CompanyManager.GetManager().Delete(idCompany);
            if (!result.Success)
                return HttpNotFound();
            return RedirectToAction("ShowCompanys");
        }

        #endregion
        #region  Employees

        public ActionResult ShowEmployees(int? idCompany = null)
        {
            EmployeesFilter filter = new EmployeesFilter();
            if (idCompany != null)
            {
                var result = CompanyManager.GetManager().LoadEntity(idCompany);
                if (!result.Success)
                    return HttpNotFound();
                Session["Company"] = filter.Company = result.ResultEntity;
            }
            Result<IEmployee> resultLoadList = EmployeesManager.GetManager().LoadEntityList(filter);
            if (!resultLoadList.Success)
                return HttpNotFound();
            ;
            return View(resultLoadList.Entitys);
        }


        [HttpGet]
        public ActionResult CreateEmployee()
        {
            //Эту штуку добавил для того, чтобы мы могли выкинуть список имеющихся компаний на вьюхе
            var result = CompanyManager.GetManager().LoadEntityList(null);
            if (!result.Success)
                return HttpNotFound();
            ViewData["Companys"] = from company in result.Entitys
                                   select new SelectListItem { Text = company.Name, Value = company.Id.ToString() };
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(IEmployee employee)
        {
            EmployeesManager.GetManager().Save(employee);
            //сессии использую для того, чтобы запоминать какая въюха была до перехода на форму редактирования
            //(если мы перешли в редактирование пользователя из вьюхи со списком работников конкретной компании, то вернемся потом на эту же вьюху)
            ICompany company = (ICompany)Session["Company"];
            return RedirectToAction("ShowEmployees", new { idCompany = (company != null) ? company.Id : null });
        }

        [HttpGet]
        public ActionResult EditEmployee(int? idEmployee = null)
        {
            //Эту штуку добавил для того, чтобы мы могли заполнить список имеющихся компаний на вьюхе
            var result = CompanyManager.GetManager().LoadEntityList(null);
            if (!result.Success)
                return HttpNotFound();
            ViewData["Companys"] = from company in result.Entitys
                                   select new SelectListItem { Text = company.Name, Value = company.Id.ToString() };
            var resultLoadEntity = EmployeesManager.GetManager().LoadEntity((int)idEmployee);
            if (!resultLoadEntity.Success)
                return View();

            return View(resultLoadEntity.ResultEntity);
        }

        /// <summary>
        /// Редактирование работникаю. пост запрос
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEmployee(IEmployee employee)
        {
            EmployeesManager.GetManager().Save(employee);
            //сессии использую для того, чтобы запоминать какая въюха была до перехода на форму редактирования
            //(если мы перешли в редактирование пользователя из вьюхи со списком работников конкретной компании, то вернемся потом на эту же вьюху)
            ICompany company = (ICompany)Session["Company"];
            return RedirectToAction("ShowEmployees", new { idCompany = (company != null) ? company.Id : null });
        }

        /// <summary>
        /// Удаление работника
        /// </summary>
        /// <param name="idEmployee"></param>
        /// <returns></returns>
        public ActionResult DeleteEmployee(int idEmployee)
        {
            var result = EmployeesManager.GetManager().LoadEntity(idEmployee);
            if(!result.Success)
                return HttpNotFound();

            EmployeesManager.GetManager().Delete(result.ResultEntity);
            //сессии использую для того, чтобы запоминать какая въюха была до перехода на форму редактирования
            //(если мы перешли в редактирование пользователя из вьюхи со списком работников конкретной компании, то вернемся потом на эту же вьюху)
            ICompany company = (ICompany)Session["Company"];
            return RedirectToAction("ShowEmployees", new { idCompany = (company != null) ? company.Id : null });
        }

        #endregion
    }
}
