using Microsoft.Extensions.DependencyInjection;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;

namespace Route.G02.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            //services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
