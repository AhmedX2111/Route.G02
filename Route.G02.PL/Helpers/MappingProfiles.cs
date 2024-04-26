using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Route.G02.DAL.Models;
using Route.G02.PL.ViewModels;

namespace Route.G02.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
