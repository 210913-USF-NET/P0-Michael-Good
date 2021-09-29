using System.Collections.Generic;

using System;

namespace Models
{
    public class Customer
    {
        public Customer(){
            Id = 0;
        }
        public Customer(string name, long phoneNum)
        {
            this.PhoneNum = phoneNum;
            this.Name = name;
            Id = 0;
        }

        public int Id{get;set;}
        public string Name{get;set;}
        private long _PhoneNum;
        public long PhoneNum
        {
            get
            {
                return PhoneNum;
            }
            set
            {
                if(value > 9999999999 || value < 1000000000)
                {
                    throw new InputInvalidException("Rating must be a valid");
                }
                else
                {
                    _PhoneNum = value;
                }
            }
        }

    }
}
