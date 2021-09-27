using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using Serilog.Events;
using StoreBL;
using DL;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient.Server;

namespace UI
{
    public class AdminMenu : IMenu
    {
        private IBL _bl;
        public AdminMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Admin menu");
                Console.WriteLine("[0] Restock Store");
                Console.WriteLine("[1] Make new Store");
                Console.WriteLine("[x] Leave");
                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        FillInventory();
                        break;

                    case "1":

                        break;

                    case "x":
                        exit = true;
                        break;
                    
                    default:
                        Console.WriteLine("Sorry, what you typed in was not a valid responce");
                        break;
                }
            }while (!exit);
        }
        public void FillInventory()
        {
            
            fillStart:
            Console.WriteLine("Select a Store to Fill Inventory");
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
                bool storeExit = false;
                Console.WriteLine($"You picked {selectedStore.Address}");
                do
                {
                    fillItemStart:
                    Console.WriteLine("Select an item to Fill");
                    if(selectedStore.Inventories == null || selectedStore.Inventories.Count == 0)
                    {
                        Console.WriteLine("This store has nothing in it");
                        return;
                    }
                    Console.WriteLine("[x] Leave");
                    for(int i = 0; i < selectedStore.Inventories.Count; i++)
                    {
                        Console.WriteLine($"[{i}] {selectedStore.Inventories[i].Item.Name}: {selectedStore.Inventories[i].Quantity}");
                    }
                    input = Console.ReadLine();
                    parseSuccess = Int32.TryParse(input, out parsedInput);
                    if(parseSuccess && parsedInput >= 0 && parsedInput < selectedStore.Inventories.Count)
                    {
                        Inventory selectedInventory = selectedStore.Inventories[parsedInput];
                        fillManyStart:
                        Console.WriteLine("How much is being added");
                        input = Console.ReadLine();
                        if(parseSuccess && parsedInput >= 0)
                        {
                            int many = parsedInput;
                            selectedInventory.Quantity = selectedInventory.Quantity + many;
                            _bl.UpdateInventory(selectedInventory);
                        }
                        else
                        {
                            Console.WriteLine("invalid input");
                            goto fillManyStart;
                        }

                    }
                    else if(input == "x")
                    {
                        storeExit = true;
                    }
                    else
                    {
                        Console.WriteLine("invalid input");
                        goto fillItemStart;
                    }

                }while(storeExit);
            }
            else
            {
                Console.WriteLine("invalid input");
                goto fillStart;
            }
        }

        public void CreateStore()
        {
            Console.WriteLine("please enter the address for the new store");
            string input = Console.ReadLine();
            StoreFront store = new StoreFront(input);
            _bl.AddNewStoreFront(store);
        }
    }
}