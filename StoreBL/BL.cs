using System;

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
