using System;
using System.Collections;
using Models;
using Serilog.Events;
using StoreBL;
using DL;

namespace UI
{
    public class RegistrationMenu : IMenu
    {
        private IBL _bl;
        public RegistrationMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            Customer customer;
            string input;
            RegStart:
            Console.WriteLine("Please enter your phone number no (, ), -");
            input = Console.ReadLine();
            long parsedInput;
            bool parseSuccess = Int64.TryParse(input, out parsedInput);
            if(parseSuccess && parsedInput >= 0)
            {
                customer = _bl.GetCustomerByPhone(parsedInput);
                if(customer.Id == 0)
                {   
                    bool exit = false;
                    do{
                        Console.WriteLine("That number is already registared, would you like to try another?");
                        Console.WriteLine("[0] Yes");
                        Console.WriteLine("[1] No");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "0":
                                return;

                            case "1":
                                goto RegStart;

                            default:
                                Console.WriteLine("Sorry, what you typed in was not a valid responce");
                                break;
                        }
                    }while(exit);
                }
            }
            else
            {
                Console.WriteLine("invalid input");
                goto RegStart;
            }

            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();

            Customer temp = new Customer(name, parsedInput);
            
            //add customer to database
            
            new OrderMenu(new BL(new DBRepo())).Start(customer.Id);
        }
    }
}