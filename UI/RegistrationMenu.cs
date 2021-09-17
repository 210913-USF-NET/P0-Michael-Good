using System;
using Models;
using StoreBL;

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
            
            Console.WriteLine("Please enter your phone number (no \"(, ), -\"");
            //int phoneNum = Console.ReadLine();
            
            Console.WriteLine("[x] Leave");
            string name = Console.ReadLine();

        }
    }
}