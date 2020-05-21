using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime TransDate { get; set; }
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }

        public Seller Seller { get; set; }

        public SalesRecord()
        {

        }

        public SalesRecord(int id, DateTime transDate, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            TransDate = transDate;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
