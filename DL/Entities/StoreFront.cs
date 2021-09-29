using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class StoreFront
    {
        public StoreFront()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
