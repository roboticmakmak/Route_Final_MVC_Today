using Company.Data.Entites;
using Company.Repository.Interfaces;
using Company.Services.Interfaces;
using Company.Services.Interfaces.Departments;
using Company.Services.Interfaces.Employee.Dto;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Company.Web.Controllers
{
    //[Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;


        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService) {
           _employeeService = employeeService;
            _departmentService = departmentService;
        }

     
        public IActionResult Index(string searchInp)
        {
            //ViewBag.Message = "Hello from index (view)";
            //ViewData["TextMessage"] = "Hello from index (viewData)";



            IEnumerable<EmployeeDto> employees = new List<EmployeeDto>();
            if(string.IsNullOrEmpty(searchInp))
            {

                 employees = _employeeService.GetAll();
            }
            else
            {
                 employees = _employeeService.GetEmployeesByName(searchInp);
              
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll(); // Change this to match the view
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _employeeService.Add(employee);

                    return RedirectToAction(nameof(Index));
                }

                return View(employee);

            }
            catch (Exception ex)
            {
                return View(employee);

            }

        }
    }
}
