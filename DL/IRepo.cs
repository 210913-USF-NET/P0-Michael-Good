using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        List<StoreFront> GetALLStoreFront();
        void SendOrder(Order order);
        Customer GetCustomerById(int id);
        Customer GetCustomerByPhone(long phoneNum);
        void AddNewStoreFront(StoreFront store);
        void AddNewCustomer(Customer customer);
        void UpdateInventory(Inventory inventory);
    }
    
}