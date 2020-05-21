using Microsoft.EntityFrameworkCore.Internal;
using SalesWebMvc.Data;
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
    }
}
