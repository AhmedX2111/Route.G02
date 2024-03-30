using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;
using Route.G02.DAL.Models;
using Route.G02.PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Route.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IEmployeeRepository _employeeRepo; // NULL
        //private readonly IDepartmentRepository _departmentRepo;
        private readonly IHostEnvironment _env;

        public EmployeeController(
            IUnitOfWork unitOfWork,
            //IEmployeeRepository employeesRepo
            //, IDepartmentRepository  departmentRepo,
            IMapper mapper, 
            IHostEnvironment env) // Ask CLR for Creating object from class Implmenting IEmployeeRepository
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            //_employeeRepo = employeesRepo;
            //_departmentRepo = departmentRepo;
        }

        //  /Employee/Index
        //[HttpGet]
        public IActionResult Index(string searchInp)
        {
            ///TempData.Keep();
            ///Binding Through views Dictionary : Transfer Data from Action to View[one way]
            /// 1.ViewData is a Dictionary Type Property(Introduced in ASP.Net Framework 3.5)
            ///          => it helps us to transfer the data from controller[Action] to View
            ///ViewData["Message"] = "Hello ViewData";
            ///1.ViewBag is a Dictionary Type Property(Introduced in ASP.Net Framework 4.0 based on dynamic feature)
            ///          => it helps us to transfer the data from controller[Action] to View
            ///ViewBag.Message = "Hello ViewBag";

            var employees = Enumerable.Empty<Employee>();

            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(searchInp))
                 employees = employeeRepo.GetAll();
            else
               employees = employeeRepo.SearchByName(searchInp.ToLower());

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);


            return View(mappedEmps);
            

        }

        // /Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // Server Side Vl=alidation
            {
                // Manual Mapping
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Address = employeeVM.Address,
                ///    Salary = employeeVM.Salary,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    IsActive = employeeVM.IsActive,
                ///    HiringDate = employeeVM.HiringDate
                ///};

                //Employee mappedEmp = (Employee) employeeVM;


                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                 _unitOfWork.Repository<Employee>().Add(mappedEmp);

                /// 3. TempData is a Dictionary Type Property (introduce in Asp.Net Framework 3.5)
                ///         => is used to pass data between two consecutive requests

                // 2. Update Department
                // _UnitOfWork.Repository<Department>().Update(department);


                // 3. Delete project
                // _UnitOfWork.Repository<project>().Remove(project);


                //_dbContext.Savechanges();
                var count = _unitOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Department is Created Successfully";
                else
                    TempData["Message"] = "An Error Has Occured, Department not created";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        // /Employee/Deatils/10
        // /Employee/Deatils/
        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //  400

            var employee = _unitOfWork.Repository<Employee>().Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();  //  404

            return View(viewName, mappedEmp);

        }

        // /Employee/Edit/10
        // /Employee/Edit/
        //[HttpGet]
        public IActionResult Edit(int? id)
        {
            //ViewBag.Departments = _departmentRepo.GetAll();
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.Repository<Employee>().Update(mappedEmp);
                _unitOfWork.Complete();
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

                return View(employeeVM);
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
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.Repository<Employee>().Delete(mappedEmp);
                _unitOfWork.Complete();
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

                return View(employeeVM);
            }
        }
    }
}
