namespace QULIX.WebUi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using App_Start;
    using Domain.QULEX.BusinessLayer.Abstract;

    using Models.Home;

    using QULIX.Domain.QULEX.BusinessLayer.Entities;
    using QULIX.Domain.QULEX.Core.Extensions;

    public class HomeController : Controller
    {
        private readonly IRepository<Employee> empRepo;

        private readonly IRepository<Company> compRepo;

        private readonly IRepository<CompanyType> compTypeRepo;

        public HomeController(IRepository<Employee> empRepo, IRepository<Company> compRepo, IRepository<CompanyType> compTypeRepo)
        {
            this.empRepo = empRepo;
            this.compRepo = compRepo;
            this.compTypeRepo = compTypeRepo;
        }

        // All the functionality to work with employees is placed here, CRUD functionality as well
        #region Working with employees

        // this method returns partial view if it is called from the other view as child action, and returns data in JSON format if the current request is AJAX request
        public ActionResult GetEmployeesData(EmployeesFilterModel employeesModel)
        {
            var empData = employeesModel.FilterEmloyees(this.empRepo.GetAll());
            if (this.Request.IsAjaxRequest())
            {
                return Json(empData.Select(
                    emp => new {
                        emp.FirstName,
                        emp.LastName,
                        emp.MiddleName,
                        emp.EmployeeId,
                        HiringDate = emp.HiringDate.ToShortDateString(),
                        Company = emp.Company.CompanyName,
                        Position = emp.Position.GetEnumStringValue()
                    }));
            }

            return PartialView(empData);
        }

        // returns the list of employees
        public ActionResult Employees()
        {
            ViewBag.Companies = this.compRepo.GetAll().OrderBy(c => c.CompanyName);
            return View(new EmployeesFilterModel());
        }

        [ActionName("Add-Employee")]
        public ActionResult AddEmployee()
        {
            return View(new Employee { HiringDate = DateTime.Now.Date });
        }

        [HttpPost]
        [ActionName("Add-Employee")]
        public ActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            if (this.empRepo.Create(employee))
                TempData["Message"] = $"Сотрудник '{employee.LastName} {employee.FirstName} {employee.MiddleName}' был успешно добавлен.";
            else
                TempData["Message"] = $"Не удалось добавить сотрудника '{employee.LastName} {employee.FirstName} {employee.MiddleName}'.";

            return RedirectToAction("Employees");
        }

        [ActionName("Edit-Employee")]
        public ActionResult EditEmployee(int id)
        {
            return View(this.empRepo.Get(id));
        }

        [HttpPost]
        [ActionName("Edit-Employee")]
        public ActionResult EditEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            if (this.empRepo.Update(employee))
                TempData["Message"] = $"Сотрудник '{employee.LastName} {employee.FirstName} {employee.MiddleName}' был успешно изменен.";
            else
                TempData["Message"] = $"Не удалось изменить сотрудника '{employee.LastName} {employee.FirstName} {employee.MiddleName}'.";

            return RedirectToAction("Employees");
        }

        // to delete some data the request shoukd not be of 'Get' type
        [HttpPost]
        public ActionResult DeleteEmployee(int employeeId)
        {
            if(this.empRepo.Delete(employeeId))
                TempData["Message"] = $"Сотрудник c id={employeeId} был успешно удален." ;
            else
                TempData["Message"] = $"Не удалось удалить сотрудника с id={employeeId}.";

            return RedirectToAction("Employees");
        }

        [ChildActionOnly]
        public PartialViewResult AddEditEmployeePartial(Employee employee)
        {
            ViewBag.Companies = this.compRepo.GetAll().OrderBy(c => c.CompanyName);

            // here we just ignore validation for all request types except POST type requests
            if (!this.Request.HttpMethod.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.Clear();
            }

            return PartialView(employee ?? new Employee { HiringDate = DateTime.Now.Date });
        }

        #endregion


        // just some helper actions, CRUd functionality is realized in Web-Api controller - CompanyController
        #region Working with companies 

        public ActionResult Companies()
        {
            ViewBag.CompanyTypes = this.compTypeRepo.GetAll();
            return View(this.compRepo.GetAll());
        }

        public PartialViewResult CreateCompanyType(string dropdownId)
        {
            return PartialView(new CreateCompanyTypeModel
               {
                   CompanyTypeDropdownId = dropdownId,
                   CompanyType = new CompanyType(),
                   LoadingElementId = "loading-element"
               });
        }

        [HttpPost]
        public ActionResult CreateCompanyType(CreateCompanyTypeModel model)
        {
            this.compTypeRepo.Create(model.CompanyType);
            return this.GetCompanyTypesDropdown(model.CompanyTypeDropdownId);
        }

        public PartialViewResult GetCompanyTypesDropdown(string dropdownId)
        {
            return PartialView(
                "GetCompanyTypesDropdown", 
                new CompanyTypeDropdownModel
                {
                    CompanyTypes = this.compTypeRepo.GetAll().OrderBy(x => x.CompanyTypeName),
                    CompanyTypeDropdownId = dropdownId
                });
        }

        protected override void Dispose(bool disposing)
        {
            this.compRepo.Dispose();
            this.compTypeRepo.Dispose();
            this.empRepo.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}