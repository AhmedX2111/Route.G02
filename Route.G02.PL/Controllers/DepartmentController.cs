using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;
using Route.G02.DAL.Models;
using Route.G02.PL.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Route.G02.PL.Controllers
{
    // Inhertiance : DepartmentController is a Controller 

    // Composition : DepartmentController has a DepartmentRepository

    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepo; // NULL
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper, IDepartmentRepository departmentsRepo, IWebHostEnvironment env) // Ask CLR for Creating object from class Implmenting IdepartmentRepository
        {
            _mapper = mapper;
            _departmentRepo = departmentsRepo;
            _env = env;
        }

        //  /Department/Index
        [HttpGet]
        public IActionResult Index(string searchInput)
        {
            var depaartments = _departmentRepo.GetAll();

            var mappedEmp = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(depaartments);


            return View(mappedEmp);
            

        }

        // /Departmet/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid) // Server Side Vl=alidation
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                var count = _departmentRepo.Add(mappedEmp);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departmentVM);
        }

        // /Department/Deatils/10
        // /Department/Deatils/
        [HttpGet]

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();  //  400

            var department = _departmentRepo.Get(id.Value);

            var mappedEmp = _mapper.Map<Department, DepartmentViewModel>(department);

            if (department is null)
                return NotFound();  //  404

            return View(viewName, mappedEmp);

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
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if(id!= departmentVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                 return View(departmentVM);

            try
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _departmentRepo.Update(mappedEmp);
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

                return View(departmentVM);
            }

        }


        // /Department/Delete/10
        // /Department/Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _departmentRepo.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log exception
                // 2. Friendly Message

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occured during Updating the Department");

                return View(departmentVM);
            }
        }
    }
}
