using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public Order(Customer cust, string address, string date)
        {
            this.Cust = cust;
            this.StoreAddress = address;
            this.DateOfOrder = date;
        }
        public int Id{get;set;}
        public Customer Cust{get;set;}
        public List<OrderLine> OrderItems{get;set;}
        public decimal Total{get;set;}
        public string DateOfOrder{get;set;}
        public string StoreAddress{get;set;}
    }
}