using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    public class Atm
    {
        public Guid Id { get; set; }
        public double CashBalance { get; set; }

        [ForeignKey("BankId")]
        public Bank Bank { get; set; }
        public Guid BankId { get; set; }
    }
}
