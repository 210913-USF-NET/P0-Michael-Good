using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Migrations;
using Models;
using Serilog.Events;
using StoreBL;

namespace UI
{
    public class OrderMenu
    {
        private IBL _bl;
        public OrderMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start(int CustomerId)
        {
            Customer cust = _bl.GetCustomerByID(CustomerId);
            Order CurrentOrder;
            orderStart:
            Console.WriteLine("Select a Store to order from");
            List<StoreFront> allStores = _bl.GetALLStoreFront();
            if(allStores == null || allStores.Count == 0)
            {
                Console.WriteLine("No stores available");
                return;
            }
            for(int i = 0; i < allStores.Count; i++)
            {
                Console.WriteLine($"[{i}] {allStores[i].Address}");
            }
            string input = Console.ReadLine();
            int parsedInput;

            bool parseSuccess = Int32.TryParse(input, out parsedInput);
            if(parseSuccess && parsedInput >= 0 && parsedInput < allStores.Count)
            {
                StoreFront selectedStore = allStores[parsedInput];
                DateTime thisday = DateTime.Today;
                CurrentOrder = new Order(cust, selectedStore.Address, thisday.ToString());

                bool exit = false;
                Console.WriteLine($"You picked {selectedStore.Address}");
                do
                {
                    orderItemStart:
                    Console.WriteLine("Select an item to order");
                    if(selectedStore.Inventories == null || selectedStore.Inventories.Count == 0)
                    {
                        Console.WriteLine("This store has nothing in it");
                        return;
                    }
                    Console.WriteLine("[x] Send Order/Leave");
                    for(int i = 0; i < selectedStore.Inventories.Count; i++)
                    {
                        Console.WriteLine($"[{i}] {selectedStore.Inventories[i].Item.Name}: {selectedStore.Inventories[i].Quantity}");
                    }
                    input = Console.ReadLine();
                    parseSuccess = Int32.TryParse(input, out parsedInput);
                    if(parseSuccess && parsedInput >= 0 && parsedInput < selectedStore.Inventories.Count)
                    {
                        Inventory selectedInventory = selectedStore.Inventories[parsedInput];
                        orderManyStart:
                        Console.WriteLine("There are " + selectedInventory.Quantity + " " + selectedInventory.Item.Name + " left");
                        Console.WriteLine("How many do you want?");
                        input = Console.ReadLine();
                        if(parseSuccess && parsedInput >= 0 && parsedInput <= selectedInventory.Quantity)
                        {
                            int many = parsedInput;
                            OrderLine orderline = new OrderLine(selectedInventory.Item, many);
                            CurrentOrder.OrderItems.Add(new OrderLine(selectedInventory.Item, many));
                            selectedInventory.Quantity = selectedInventory.Quantity - many;
                            _bl.UpdateInventory(selectedInventory);
                            Console.WriteLine("Item added to Order");
                        }
                        else
                        {
                            Console.WriteLine("invalid input");
                            goto orderManyStart;
                        }

                    }
                    else if(input == "x")
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("invalid input");
                        goto orderItemStart;
                    }

                }while(exit);
            }
            else
            {
                Console.WriteLine("invalid input");
                goto orderStart;
            }

            _bl.SendOrder(CurrentOrder);
        }
    }
}