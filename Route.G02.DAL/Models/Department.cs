using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.G02.DAL.Models
{
    // Model
    public class Department : ModelBase
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //[InverseProperty(nameof(Employee.Department))]
        // Navigational property => [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
