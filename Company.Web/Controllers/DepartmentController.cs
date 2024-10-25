using Company.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Company.Repository.Interfaces;
using Company.Services.Interfaces.Departments;
using Company.Data.Entites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Company.Services.Interfaces.Departments.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Company.Web.Controllers
{
    //[Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]

        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            //TempData.Keep("TextTempMessage");
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create(DepartmentDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _departmentService.Add(department);


                    //TempData["TextTempMessage"] = "Hello from index (TempData)";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("DepartmentError", "ValidationErrors");

                return View(department);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepartmentError", ex.Message);

                return View(department);

            }


        }


        public IActionResult Details(int? id, string viewName = "Details")
        {
            var department = _departmentService.GetbyId(id);

            if (department == null)
                return RedirectToAction("NotFound", null, "Home");

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");

        }
        [HttpPost]
        public IActionResult Update(int? id , DepartmentDto department)
        {
            if(department.Id != id.Value)
                return RedirectToAction("NotFound",null, "Home");

            _departmentService.Update(department);

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(int id)
        {

            var department = _departmentService.GetbyId(id);

            if (department == null)
                return RedirectToAction("NotFound", null, "Home");

            _departmentService.Delete(department);
            return RedirectToAction(nameof(Index));
        }



    }
}
