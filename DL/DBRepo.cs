using System.Collections.Generic;
using Models;

namespace DL
{
    public class DBRepo : IRepo
    {
        public List<StoreFront> GetALLStoreFront()
        {
            return new List<StoreFront>(){}; 
            
            throw new System.NotImplementedException();
        }
    }
}