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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : 
            base(dbContext)
        {
            
        }



    }
}
