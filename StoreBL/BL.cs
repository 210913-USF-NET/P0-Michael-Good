using System.Collections.Generic;
using System;
using Models;
using DL;

namespace StoreBL
{
    public class BL : IBL
    {
        private IRepo _repo;

        public BL(IRepo repo)
        {
            _repo = repo;
        }

        public List<StoreFront> GetALLStoreFront()
        {
            return _repo.GetALLStoreFront();
        }
    }
}
