using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }

        public ICollection<SalesRecord> SalesRecords = new List<SalesRecord>();
        
        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSalesRecord(SalesRecord      salesRecord)
        {
            SalesRecords.Add(salesRecord);
        }

        public void RemoveSalesRecord(SalesRecord salesRecord)
        {
            SalesRecords.Remove(salesRecord);
        }

        public double TotalSales(DateTime fromDate, DateTime toDate)
        {
            double ret = 0;

            ret = SalesRecords.Where(sales => sales.TransDate >= fromDate && sales.TransDate <= toDate).Sum(sales => sales.Amount);

            return ret;
        }
    }
}
