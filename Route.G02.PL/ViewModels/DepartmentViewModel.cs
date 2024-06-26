﻿using Route.G02.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Route.G02.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Is Required!!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required!!")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //[InverseProperty(nameof(Employee.Department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
