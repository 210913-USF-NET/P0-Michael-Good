using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PhoneNum { get; set; }
    }
}
