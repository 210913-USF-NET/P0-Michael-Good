using System;
using Models;
using StoreBL;
using DL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<IIDBContext> options = new DbContextOptionsBuilder<IIDBContext>().UseSqlServer(connectionString).Options;
            IIDBContext context = new IIDBContext(options);

            new MainMenu(new BL(new DBRepo(context))).Start();
            
        }
    }
}
