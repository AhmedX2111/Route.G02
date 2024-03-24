using Microsoft.EntityFrameworkCore;
using Route.G02.BLL.Interfaces;
using Route.G02.DAL.Data;
using Route.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.G02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) 
            :base(dbContext) 
        
        {
            //_dbContext = dbContext;
        }

        

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
        }
    }
}
