using System;
using Models;
using StoreBL;

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
                        //login menu
                        break;

                    case "1":
                        //sign up menu

                        break;

                    case "x":
                        Console.WriteLine("Goodbye!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Sorry, what you typed in was not a valid responce");
                        break;
                }
            }while (!exit);
        }
    }
}