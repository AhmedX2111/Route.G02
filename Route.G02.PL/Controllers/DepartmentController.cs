using Microsoft.AspNetCore.Mvc;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;
using Route.G02.DAL.Models;

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

        // /Departmet/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) // Server Side Vl=alidation
            {
               var count = _departmentRepo.Add(department);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        // /Department/Deatils/10
        // /Department/Deatils/
        [HttpGet]

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();  //  400

            var department = _departmentRepo.Get(id.Value); 

            if (department is null)
                return BadRequest();  //  404

            return View(department);

        }
    }
}
