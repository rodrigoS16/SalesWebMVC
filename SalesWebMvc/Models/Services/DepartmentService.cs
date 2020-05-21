using SalesWebMvc.Data;
using System;
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

        public List<Department> FindAll()
        {
            return _Context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
