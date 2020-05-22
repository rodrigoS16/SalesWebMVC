using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [
            Required(ErrorMessage = "{0} Required"),
            StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")
        ]
        public string Name { get; set; }

        [
            EmailAddress(ErrorMessage = " Enter a valid {0}"),
            Required(ErrorMessage = "{0} Required"),
            Display(Name = "E-mail"),
            DataType(DataType.EmailAddress)
        ]
        public string Email { get; set; }

        [
            Required(ErrorMessage = "{0} Required"),
            Display(Name = "Birth date"),
            DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")
        ]
        public DateTime BirthDate { get; set; }

        [
            Range(100.0, 50000.0, ErrorMessage = "{0} size should be between {2} and {1}"),
            Required(ErrorMessage = "{0} Required"),            
            Display(Name = "Base salary"),
            DisplayFormat(DataFormatString = "R$ {0:F2}")
            //,DataType(DataType.Currency)
            
        ]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

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

        public void AddSalesRecord(SalesRecord salesRecord)
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
