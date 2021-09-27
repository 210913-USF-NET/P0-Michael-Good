using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    public class DBRepo : IRepo
    {
        public List<StoreFront> GetALLStoreFront()
        {
            return new List<StoreFront>(){}; 
            
        }
        public void SendOrder(Order order)
        {

        }
        public void AddNewCustomer(Customer customer)
        {
            
        }
        public Customer GetCustomerById(int id)
        {
            return new Customer();
        }
        public Customer GetCustomerByPhone(long phoneNum)
        {
            return new Customer();
        }
        public void AddNewStoreFront(StoreFront store)
        {

        }
        public void UpdateInventory(Inventory inventory)
        {

        }
    }
}