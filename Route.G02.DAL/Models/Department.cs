using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.G02.DAL.Models
{
    // Model
    internal class Department
    {
        public int Id { get; set; } // PK

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
