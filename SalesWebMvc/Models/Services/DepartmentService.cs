﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _Context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _Context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _Context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
