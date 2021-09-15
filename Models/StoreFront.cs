using System.Collections.Generic;

namespace Models;
{
    public class StoreFront
    {
        public StoreFront(){}

        public StoreFront(int num, string address)
        {
            this.Num = num;
            this.Address = address;
        }

        public int Num{get;set;}
        public string Address{get;set;}
        public List<Inventory> Inventories{get;set;}

        public override string ToString()
        {
            return $"Store Name: {this.Name} \nAddress: {this.Address}"
        }
    }
}