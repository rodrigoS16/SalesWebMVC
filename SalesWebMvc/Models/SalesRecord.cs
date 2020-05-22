using SalesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [
            Display(Name = "Transaction date"),
            DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")
        ]
        public DateTime TransDate { get; set; }

        [
            Display(Name = "Amount"),
            DisplayFormat(DataFormatString = "R$ {0:F2}")
        ]
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }

        public Seller Seller { get; set; }
        public int SellerId { get; set; }

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
