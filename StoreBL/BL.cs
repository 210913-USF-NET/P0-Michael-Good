using System.Collections.Generic;
using System;
using Models;
using DL;

namespace StoreBL
{
    public class BL : IBL
    {
        private IRepo _repo;

        public BL(IRepo repo)
        {
            _repo = repo;
        }

        public List<StoreFront> GetALLStoreFront()
        {
            return _repo.GetALLStoreFront();
        }
        public void SendOrder(Order order)
        {
            _repo.SendOrder(order);
        }
        public Customer GetCustomerByID(int CustomerId)
        {
            return _repo.GetCustomerById(CustomerId);
        }
        public Customer GetCustomerByPhone(long phoneNum)
        {
            return _repo.GetCustomerByPhone(phoneNum);
        }
        public void AddNewStoreFront(StoreFront store)
        {
            _repo.AddNewStoreFront(store);
        }
        public void UpdateInventory(Inventory inventory)
        {
            _repo.UpdateInventory(inventory);
        }
        public void AddNewCustomer(Customer customer)
        {
            _repo.AddNewCustomer(customer);
        }
    }
}
