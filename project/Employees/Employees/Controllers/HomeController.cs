using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employees.Models;
using Employees.BL;

namespace Employees.Controllers
{
    public class HomeController : Controller
    {
        public static List<Company> listCompany = new List<Company>();
        public static List<Employee> listEmployee = new List<Employee>();

        public HomeController()
        {
            if (listCompany.Count == 0)
            {
                listCompany.Add(new Company { Id = 1, Name = "ХУЙ", OrganizationForm = "EF", SizeCompany = 3 });
                listCompany.Add(new Company { Id = 2, Name = "ХУЙ1", OrganizationForm = "FDHSD", SizeCompany = 6 });
                listCompany.Add(new Company { Id = 3, Name = "ХУЙ2", OrganizationForm = "FHFGH", SizeCompany = 12 });
            }
            if (listEmployee.Count == 0)
            {
                listEmployee.Add(new Employee { Id = 1, FirstName = "перс1", CompanyId = 1, SecondName="loh1" });
                listEmployee.Add(new Employee { Id = 2, FirstName = "перс2", CompanyId = 1, SecondName = "loh2" });
                listEmployee.Add(new Employee { Id = 3, FirstName = "перс3", CompanyId = 1, SecondName = "loh3" });
            }

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
        public ActionResult EditCompany(Company company)
        {
            CompanyManager.GetManager().Update(company);
            return RedirectToAction("ShowCompanys");
        }

        [HttpGet]
        public ActionResult EditCompany(int? idCompany)
        {
            if (idCompany != null)
            {
                Company company = CompanyManager.GetManager().GetEntityById((int)idCompany);
                return View(company);
            }
            return View();
        }

        public ActionResult ShowCompanys()
        {
            var list = CompanyManager.GetManager().GetEntitys();
            return View(list);
        }

        public ActionResult DeleteCompany(int idCompany)
        {
            Company company = CompanyManager.GetManager().GetEntityById(idCompany);
            if (company == null)
                return HttpNotFound();
            CompanyManager.GetManager().Delete(company);
            return RedirectToAction("ShowCompanys");
        }

        #endregion

        #region  Employees

        public ActionResult ShowEmployees(int? idCompany = null)
        {
            if (idCompany != null)
            {
                Company company = CompanyManager.GetManager().GetEntityById((int)idCompany);
                Session["Company"] = company;
                if (company != null)
                {
                    List<Employee> collection = EmployeesManager.GetManager().GetEntitysForCompany(company);
                    return View(collection);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                Session["Company"] = null;
                List<Employee> collection = EmployeesManager.GetManager().GetEntitys();
                return View(collection);
            }
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            //Эту штуку добавил для того, чтобы мы могли выкинуть список имеющихся компаний на вьюхе
            ViewData["Companys"] = from company in CompanyManager.GetManager().GetEntitys()
                                   select new SelectListItem { Text = company.Name, Value = company.Id.ToString() };
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            EmployeesManager.GetManager().Update(employee);
            //сессии использую для того, чтобы запоминать какая въюха была до перехода на форму редактирования
            //(если мы перешли в редактирование пользователя из вьюхи со списком работников конкретной компании, то вернемся потом на эту же вьюху)
            Company company = (Company)Session["Company"];
            return RedirectToAction("ShowEmployees", new { idCompany = (company != null) ? company.Id : null });
        }

        [HttpGet]
        public ActionResult EditEmployee(int? idEmployee = null)
        {
            //Эту штуку добавил для того, чтобы мы могли заполнить список имеющихся компаний на вьюхе
            ViewData["Companys"] = from company in CompanyManager.GetManager().GetEntitys()
                                   select new SelectListItem { Text = company.Name, Value = company.Id.ToString() };
            if (idEmployee != null)
            {
                Employee employee = EmployeesManager.GetManager().GetEntityById((int)idEmployee);
                return View(employee);
            }
            return View();
        }

        /// <summary>
        /// Редактирование работникаю. пост запрос
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            EmployeesManager.GetManager().Update(employee);
            //сессии использую для того, чтобы запоминать какая въюха была до перехода на форму редактирования
            //(если мы перешли в редактирование пользователя из вьюхи со списком работников конкретной компании, то вернемся потом на эту же вьюху)
            Company company = (Company)Session["Company"];
            return RedirectToAction("ShowEmployees", new { idCompany = (company != null) ? company.Id : null });
        }

        /// <summary>
        /// Удаление работника
        /// </summary>
        /// <param name="idEmployee"></param>
        /// <returns></returns>
        public ActionResult DeleteEmployee(int idEmployee)
        {
            Employee employee = EmployeesManager.GetManager().GetEntityById(idEmployee);
            if (employee == null)
                return HttpNotFound();
            EmployeesManager.GetManager().Delete(employee);
            //сессии использую для того, чтобы запоминать какая въюха была до перехода на форму редактирования
            //(если мы перешли в редактирование пользователя из вьюхи со списком работников конкретной компании, то вернемся потом на эту же вьюху)
            Company company = (Company)Session["Company"];
            return RedirectToAction("ShowEmployees", new { idCompany = (company != null) ? company.Id : null });
        }

        #endregion
    }
}