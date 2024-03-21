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
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext; // NULL

        public DepartmentRepository(ApplicationDbContext dbContext) // Ask CLR for creating object from "ApplicationDbContext"
        {
            //_dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
            _dbContext = dbContext;
        }

        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            //return _dbContext.Departments.Find(id);
            return _dbContext.Find<Department>(id); // EF Core 3.1 New Feature
            ///var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            ///
            ///if (department == null)
            ///{
            ///    department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            ///}
            ///return department;
        }

        public IEnumerable<Department> GetAll()
             =>  _dbContext.Departments.AsNoTracking().ToList();
        

        
    }
}
