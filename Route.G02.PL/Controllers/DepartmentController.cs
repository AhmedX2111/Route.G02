using Microsoft.AspNetCore.Mvc;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;

namespace Route.G02.PL.Controllers
{
    // Inhertiance : DepartmentController is a Controller 

    // Composition : DepartmentController has a DepartmentRepository

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo; // NULL

        public DepartmentController(IDepartmentRepository departmentsRepo) // Ask CLR for Creating object from class Implmenting IdepartmentRepository
        {
            _departmentRepo = departmentsRepo;
        }

        //  /Department/Index
        public IActionResult Index()
        {
            var depaartments = _departmentRepo.GetAll();

            return View(depaartments);
        }
    }
}
