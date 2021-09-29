using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal? Total { get; set; }
        public string Date { get; set; }
        public string StoreAddress { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
