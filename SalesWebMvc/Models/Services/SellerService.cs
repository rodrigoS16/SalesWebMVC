using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SalesWebMvc.Data;
using SalesWebMvc.Models.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _Context;

        public SellerService(SalesWebMvcContext context)
        {
            _Context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _Context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {            
            _Context.Seller.Add(seller);
            await _Context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _Context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            Seller seller = await _Context.Seller.FindAsync(id);

            _Context.Seller.Remove(seller);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _Context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (hasAny)
            {                
                try
                {
                    _Context.Update(seller);
                    await _Context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    throw new DbConcurrencyException(e.Message);
                }
            }
            else
            {
                throw new NotFoundException("Id not found");
            }
        }
    }
}
