using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace U4.Domain.Entities
{
    public class Address
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}