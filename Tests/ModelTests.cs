using System;
using Xunit;
using Models;

namespace Tests
{
    public class ModelTests
    {
        [Fact]
        public void CustomerShouldSetValidName()
        {
            //Arrange
            Customer test = new Customer();
            string testName = "test Customer";

            //Act
            test.Name = testName;

            //Assert
            Assert.Equal(testName, test.Name);

        }

        [Fact]
        public void CustomerShouldSetValidPhoneNum()
        {
            //Arrange
            Customer test = new Customer();
            long testPhoneNum = 8434556926;

            //Act
            test.PhoneNum = testPhoneNum;

            //Assert
            Assert.Equal(testPhoneNum, test.PhoneNum);
        }

        [Fact]
        public void StoreFrontShouldSetValidAddress()
        {
             //Arrange
            StoreFront test = new StoreFront();
            string testAddress = "693 Liberty StreetProvidence, RI 02904";

            //Act
            test.Address = testAddress;

            //Assert
            Assert.Equal(testAddress, test.Address);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000000000)]
        [InlineData(10000)]
        public void CustomerShouldNotSetInvalidPhoneNum(long input)
        {
            Customer test = new Customer();

            Assert.Throws<InputInvalidException>(() => test.PhoneNum = input);
        }
    }
}
