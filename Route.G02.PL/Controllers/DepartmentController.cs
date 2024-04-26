using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.G02.BLL.Interfaces;
using Route.G02.DAL.Models;
using Route.G02.PL.ViewModels;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Route.G02.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index(string searchInput)
        {
            // Retrieve departments from the repository
            var departments = _unitOfWork.Repository<Department>().GetAllAsync();

            // Map departments to DepartmentViewModels
            var departmentViewModels = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            // Pass the DepartmentViewModels to the view
            return View(departmentViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                // Map DepartmentViewModel to Department entity
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                // Add department to the repository
                _unitOfWork.Repository<Department>().Add(department);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();

            // Retrieve department from the repository
            var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

            if (department == null)
                return NotFound();

            // Map Department to DepartmentViewModel
            var departmentVM = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(departmentVM);

            try
            {
                // Map DepartmentViewModel to Department entity
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                // Update department in the repository
                _unitOfWork.Repository<Department>().Update(department);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log exception and return friendly message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred during updating the department");

                return View(departmentVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                // Map DepartmentViewModel to Department entity
                var department = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                // Delete department from the repository
                _unitOfWork.Repository<Department>().Delete(department);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log exception and return friendly message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred during deleting the department");

                return View(departmentVM);
            }
        }
    }
}
