using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using System;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _Context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _Context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? fromDate,
                                                             DateTime? toDate)
        {
            var result = from obj in _Context.SalesRecord select obj;          

            if (fromDate.HasValue)
            {
                result = result.Where(x => x.TransDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                result = result.Where(x => x.TransDate <= toDate.Value);
            }

            return await result.Include(x => x.Seller)
                               .Include(x => x.Seller.Department)
                               .OrderByDescending(x => x.TransDate)
                               .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? fromDate,
                                                                     DateTime? toDate)
        {
            var result = from obj in _Context.SalesRecord select obj;

            if (fromDate.HasValue)
            {
                result = result.Where(x => x.TransDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                result = result.Where(x => x.TransDate <= toDate.Value);
            }

            return await result.Include(x => x.Seller)
                               .Include(x => x.Seller.Department)
                               .OrderByDescending(x => x.TransDate)
                               .GroupBy(x => x.Seller.Department)
                               .ToListAsync();
        }

    }
}
