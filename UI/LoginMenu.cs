using System;
using Models;
using StoreBL;

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
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Please enter your phone number (no \"(, ), -\"");
                Console.WriteLine("[x] Leave");
                input = Console.ReadLine();

                if(input == "x")
                {
                    Console.WriteLine("Goodbye!");
                    exit = true;
                }
                else
                {
                    //call somthing that will check db for phone number given and return bool if valid
                    bool valid = true;
                    if(valid)
                    {
                        //send to shopping menu
                    }
                    else
                    {
                        Console.WriteLine("The Phonenumber you have tried is not valid, would you like to try another Phonenubmer?");
                        Console.WriteLine("[0] Yes");
                        Console.WriteLine("[1] No");

                        switch (input)
                        {
                            case "0":
                                break;

                            case "1":
                                exit = true;
                                break;

                            default:
                                Console.WriteLine("Sorry, what you typed in was not a valid responce");
                                break;
                        }

                    }
                }
            }while (!exit);
        }
    }
}