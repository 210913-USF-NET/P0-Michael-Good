using System;
using Models;
using StoreBL;
using DL;
using System.Transactions;

namespace UI
{
    public class MainMenu : IMenu
    {
        private IBL _bl;
        public MainMenu(IBL bl)
        {
            _bl = bl;
        }

        bool exit = false;
            string input = "";
        public void Start()
        {   
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Welcome to My Store!");
                Console.WriteLine("Have you shopped with us before?");
                Console.WriteLine("[0] Yes");
                Console.WriteLine("[1] No");
                Console.WriteLine("[x] Leave");
                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        new LoginMenu(new BL(new DBRepo())).Start();
                        break;

                    case "1":
                        new RegistrationMenu(new BL(new DBRepo())).Start();
                        break;

                    case "x":
                        Console.WriteLine("Goodbye!");
                        exit = true;
                        break;
                    
                    case "admin":
                        Console.WriteLine("enter password");
                        input = Console.ReadLine();
                        if(input == "qwerty")
                        {
                            new AdminMenu(new BL(new DBRepo())).Start();
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password");
                        }
                        break;

                    default:
                        Console.WriteLine("Sorry, what you typed in was not a valid responce");
                        break;
                }
            }while (!exit);
        }
    }
}