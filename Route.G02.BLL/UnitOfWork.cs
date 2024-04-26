using Route.G02.BLL.Interfaces;
using Route.G02.BLL.Repositories;
using Route.G02.DAL.Data;
using Route.G02.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.G02.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        //private Dictionary<string, IGenericRepository<ModelBase>> _repositories;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext) // Ask CLR for Craeting object from dbContext
        {
            _dbContext = dbContext;
            _repositories= new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name; // Employee

            if (!_repositories.ContainsKey(key))
            {
                
                if (key == nameof(Employee))
                {
                   var repository = new EmployeeRepository(_dbContext);
                   _repositories.Add(key, repository);

                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(key, repository);

                }
            }

            return _repositories[key] as IGenericRepository<T>;
        }


        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync(); // close connection
        }

    }
}
