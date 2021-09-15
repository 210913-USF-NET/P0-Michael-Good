using System;
using Models;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to My Store!");

            new MainMenu(new BL(new ExampleRepo()));



        }
    }
}
