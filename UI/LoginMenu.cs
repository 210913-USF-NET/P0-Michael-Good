using System;
using Models;
using StoreBL;
using DL;

namespace UI
{
    public class LoginMenu : IMenu
    {
        private IBL _bl;
        public LoginMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            Customer customer = null;
            string input;
            LogStart:
            Console.WriteLine("Please enter your phone number no (, ), -");
            input = Console.ReadLine();
            long parsedInput;
            bool parseSuccess = Int64.TryParse(input, out parsedInput);
            if(parseSuccess && parsedInput >= 0)
            {
                customer = _bl.GetCustomerByPhone(parsedInput);
                if(customer.Id == 0)
                {   
                    phoneStart:
                    Console.WriteLine("That number is not registared, would you like to try another?");
                    Console.WriteLine("[0] Yes");
                    Console.WriteLine("[1] No");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            return;

                        case "0":
                            goto LogStart;

                        default:
                            Console.WriteLine("Sorry, what you typed in was not a valid responce");
                            goto phoneStart;
                    }
                }
            }
            else
            {
                Console.WriteLine("invalid input");
                goto LogStart;
            }

            new OrderMenu(new BL(new DBRepo())).Start(customer.Id);
        }
    }
}