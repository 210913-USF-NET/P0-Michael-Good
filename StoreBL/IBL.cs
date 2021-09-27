using System.Collections.Generic;
using Models;

namespace StoreBL
{
    public interface IBL
    {
        List<StoreFront> GetALLStoreFront();
        void SendOrder(Order order);
        Customer GetCustomerByID(int CustomerId);
        Customer GetCustomerByPhone(long phoneNum);
        void AddNewStoreFront(StoreFront store);
        void UpdateInventory(Inventory inventory);
        void AddNewCustomer(Customer customer);
    }
}