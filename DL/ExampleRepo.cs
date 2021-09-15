using System.Collections.Generic;
using System;
using Models;

namespace DL
{
    public class ExampleRepo : IRepo
    {
        public List<StoreFront> GetALLStoreFront()
        {
            //placeholder for now
            return new List<StoreFront>() {};
        }
    }
}
