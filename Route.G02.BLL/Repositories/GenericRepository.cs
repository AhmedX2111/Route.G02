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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext; // NULL

        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR for creating object from "ApplicationDbContext"
        {
            //_dbContext = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());
            _dbContext = dbContext;
        }

        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //_dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            //_dbContext.Update(entity); //EF core 3.1
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            //_dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            //return _dbContext.Set<T>().Find(id);
            return _dbContext.Find<T>(id); // EF Core 3.1 New Feature
            ///var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
            ///
            ///if (Employee == null)
            ///{
            ///    Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
            ///}
            ///return Employee;
        }

        public IEnumerable<T> GetAll()
             => _dbContext.Set<T>().AsNoTracking().ToList();
    }
}
