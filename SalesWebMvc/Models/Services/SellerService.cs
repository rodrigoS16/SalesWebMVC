using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SalesWebMvc.Data;
using SalesWebMvc.Models.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _Context;

        public SellerService(SalesWebMvcContext context)
        {
            _Context = context;
        }

        public List<Seller> FindAll()
        {
            return _Context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {            
            _Context.Seller.Add(seller);
            _Context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _Context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Delete(int id)
        {
            Seller seller = _Context.Seller.Find(id);

            _Context.Seller.Remove(seller);
            _Context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (_Context.Seller.Any(x => x.Id == seller.Id))
            {                
                try
                {
                    _Context.Update(seller);
                    _Context.SaveChanges();
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
