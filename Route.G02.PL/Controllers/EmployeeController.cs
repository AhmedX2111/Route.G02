using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.G02.BLL.Interfaces;
using Route.G02.DAL.Models;
using System;

namespace Route.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo; // NULL
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeesRepo, IWebHostEnvironment env) // Ask CLR for Creating object from class Implmenting IEmployeeRepository
        {
            _employeeRepo = employeesRepo;
            _env = env;
        }

        //  /Employee/Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepo.GetAll();

            return View(employees);

        }

        // /Departmet/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) // Server Side Vl=alidation
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }

        // /Employee/Deatils/10
        // /Employee/Deatils/
        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //  400

            var employee = _employeeRepo.Get(id.Value);

            if (employee is null)
                return NotFound();  //  404

            return View(viewName, employee);

        }

        // /Employee/Edit/10
        // /Employee/Edit/
        //[HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

            ///if (!id.HasValue)
            ///    return BadRequest();  //  400
            ///var Employee = _EmployeeRepo.Get(id.Value);
            ///if (Employee is null)
            ///    return NotFound();  //  404
            ///return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
                return View(employee);

            try
            {
                _employeeRepo.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating the Employee");

                return View(employee);
            }

        }


        // /Employee/Delete/10
        // /Employee/Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log exception
                // 2. Friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating the Employee");

                return View(employee);
            }
        }
    }
}
