using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;
using Route.G02.DAL.Models;
using System;

namespace Route.G02.PL.Controllers
{
    // Inhertiance : DepartmentController is a Controller 

    // Composition : DepartmentController has a DepartmentRepository

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo; // NULL
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentsRepo, IWebHostEnvironment env) // Ask CLR for Creating object from class Implmenting IdepartmentRepository
        {
            _departmentRepo = departmentsRepo;
            _env = env;
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

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //  400

            var department = _departmentRepo.Get(id.Value); 

            if (department is null)
                return NotFound();  //  404

            return View(viewName, department);

        }

        // /Department/Edit/10
        // /Department/Edit/
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //    return BadRequest();  //  400

            //var department = _departmentRepo.Get(id.Value);

            //if (department is null)
            //    return NotFound();  //  404

            //return View(department);

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Department department)
        {
            if(id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
                 return View(department);

            try
            {
                _departmentRepo.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message

                if(_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating the Department");

                return View(department);
            }
            
        }
    }
}
