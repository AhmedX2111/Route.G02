using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Route.G02.DAL.Models
{
    public enum Gender 
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }

    public enum EmpType
    {
        FullTime = 1,
        PartTime = 2,
    }

    public class Employee : ModelBase
    {
        

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50, ErrorMessage ="Max Lenght of Name is 5o chars")]
        [MinLength(5, ErrorMessage = "Max Lenght of Name is 5 chars")]

        public string Name { get; set; }

        [Range(22,30)]

        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,5} [a-zA-Z0-9\s'-]{1,50}, [a-zA-Z\s'-]{1,50}, [a-zA-Z]{2,3}$",
                 ErrorMessage = "Address must be in the format: StreetNumber StreetName, City, CountryCode")]



        public string Address { get; set; }

        [DataType(DataType.Currency)]

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]

        public bool IsActive { get; set; }

        [EmailAddress]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]

        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]

        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmpType EmployeeType { get; set; }

        public DateTime CraetionDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        //[ForeignKey("Department")]
        public int? DepartmentId { get; set; } // Foregin key column



        //[InverseProperty(nameof(Models.Department.Employees))]
        // Navigational property = [one]
        public Department Department { get; set; }

    }
}
