using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    public class Manager
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Manager")]
        public List<Bank> Banks { get; set; }
    }
}
