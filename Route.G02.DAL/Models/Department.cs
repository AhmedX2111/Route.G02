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
 
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        // Navigational property => [Many]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}
