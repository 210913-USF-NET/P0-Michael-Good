using System.Runtime.CompilerServices;

namespace Models
{
    public class Inventory
    {
        public Inventory(Product item, int quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }
        public int Id{get;set;}
        public Product Item{get;set;}
        public int Quantity{get;set;}
        
    }
}