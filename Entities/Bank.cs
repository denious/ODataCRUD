using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    public class Bank
    {
        public Guid Id { get; set; }
        public string Address { get; set; }

        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }
        public Guid ManagerId { get; set; }

        [InverseProperty("Bank")]
        public List<Atm> Atms { get; set; }
    }
}
