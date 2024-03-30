using Microsoft.Extensions.DependencyInjection;
using Route.G02.BLL;
using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories; 

namespace Route.G02.PL.Helpers
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
