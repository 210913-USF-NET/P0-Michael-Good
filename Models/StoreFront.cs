using System.Collections.Generic;
using System;

namespace Models
{
    public class StoreFront
    {
        public StoreFront(string address)
        {
            this.Address = address;
        }

        public int Id{get;set;}
        public string Address{get;set;}
        public List<Inventory> Inventories{get;set;}
    }
}