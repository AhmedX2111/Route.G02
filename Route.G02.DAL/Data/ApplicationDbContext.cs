﻿using Microsoft.EntityFrameworkCore;
using Route.G02.DAL.Data.Configurations;
using Route.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.G02.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        { 


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department> Departments { get; set; }
    }
}