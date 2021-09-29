using System;
using System.Collections.Generic;
using System.Linq;
using Model = Models;
using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using DL.Entities;
using Models;

namespace DL
{
    public class DBRepo : IRepo
    {
        private Entity.IIDBContext _context;
        public  DBRepo(Entity.IIDBContext context)
        {
            _context = context;
        }
        public List<Model.StoreFront> GetALLStoreFront()
        {
            List<Model.StoreFront> sFronts = _context.StoreFronts.Select(StoreFront => new Model.StoreFront()
                {
                    Id = StoreFront.Id,
                    Address = StoreFront.Address,
                    
                }
            ).ToList();
            foreach(Model.StoreFront s in sFronts)
            {
                List<Model.Inventory> inventories = _context.Inventories.Include("Product").Where(i => i.StoreFrontId == s.Id).Select(i => new Model.Inventory{
                Id = i.Id,
                Quantity = i.Quantity ?? 0,
                Item = new Model.Product(){
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price ?? 0
                }
                }).ToList();

                s.Inventories = inventories;
            }
            return sFronts;
        }
        
        /// <summary>
        /// Gets Store from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model.StoreFront with Id == id</returns>
        public Model.StoreFront GetStoreFrontById(int id)
        {
            Entity.StoreFront StoreById = _context.StoreFronts.Include("Inventories").FirstOrDefault(s => s.Id == id);
            if(StoreById == null)
            {
                return new Model.StoreFront(){};
            }
            Model.StoreFront store = new Model.StoreFront(){
                Id = StoreById.Id,
                Address = StoreById.Address
            };

            List<Model.Inventory> inventories = _context.Inventories.Include("Product").Where(i => i.StoreFrontId == StoreById.Id).Select(i => new Model.Inventory{
            Id = i.Id,
            Quantity = i.Quantity ?? 0,
            Item = new Model.Product(){
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price ?? 0
                }
            }).ToList();
            store.Inventories = inventories;
            return store;

        }
        public void SendOrder(Model.Order order)
        {
            Entity.Order orderSend = new Entity.Order(){
                CustomerId = order.Cust.Id,
                StoreAddress = order.StoreAddress,
                Total = order.Total,
                Date = order.DateOfOrder
            };
            _context.Add(orderSend);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            foreach(Model.OrderLine ol in order.OrderItems)
            {
                Entity.OrderLine orderLineSend = new Entity.OrderLine(){
                    Id = ol.Id,
                    Quantity = ol.Quantity,
                    ProductId = ol.Item.Id,
                    OrderId = orderSend.Id
                };
                _context.Add(orderLineSend);
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
            }
        }
        public void AddNewCustomer(Model.Customer customer)
        {
            Entity.Customer customerAdd = new Entity.Customer(){
                Name = customer.Name,
                PhoneNum = customer.PhoneNum
            };
            _context.Add(customerAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
        public Model.Customer GetCustomerById(int id)
        {
            Entity.Customer customerById = _context.Customers.FirstOrDefault(c => c.Id == id);
            return new Model.Customer(){
                Id = customerById.Id,
                Name = customerById.Name,
                PhoneNum = customerById.PhoneNum
            };
        }
        public Model.Customer GetCustomerByPhone(long phoneNum)
        {
            Entity.Customer customerById = _context.Customers.FirstOrDefault(c => c.PhoneNum == phoneNum);
            if(customerById == null)
            {
                return new Model.Customer(){};
            }
            
            return new Model.Customer(){
                Id = customerById.Id,
                Name = customerById.Name,
                PhoneNum = customerById.PhoneNum
            };
        }
        public void AddNewStoreFront(Model.StoreFront store)
        {
            Entity.StoreFront storeAdd = new Entity.StoreFront(){
                Address = store.Address
            };
            _context.Add(storeAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            for(int i = 1; i <= 4; i++)
            {
                Entity.Inventory inventoryAdd = new Entity.Inventory(){
                    Quantity = 0,
                    ProductId = i,
                    StoreFrontId = storeAdd.Id
                };
                _context.Add(inventoryAdd);
            }
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
        public void UpdateInventory(Model.Inventory inventory, int storeId)
        {
            Entity.Inventory inventoryUpdate = new Entity.Inventory(){
                Id  = inventory.Id,
                ProductId = inventory.Item.Id,
                StoreFrontId = storeId,
                Quantity = inventory.Quantity
            };
            _context.Update(inventoryUpdate);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public List<Model.Order> GetAllOrdersByCustomerByDate(int customerId)
        {
            List<Model.Order> orders = _context.Orders.Include("Customer").Where(Order => Order.CustomerId == customerId).Select(Order => new Model.Order{
                Id = Order.Id,
                Cust = new Model.Customer(){
                    Id = Order.Customer.Id,
                    Name = Order.Customer.Name,
                    PhoneNum = Order.Customer.PhoneNum
                },
                StoreAddress = Order.StoreAddress,
                Total = Order.Total ?? 0,
                DateOfOrder = Order.Date
            }).ToList();
            foreach(Model.Order o in orders)
            {
                List<Model.OrderLine> orderLines = _context.OrderLines.Include("Product").Where(ol => ol.OrderId == o.Id).Select(i => new Model.OrderLine{
                Id = i.Id,
                Quantity = i.Quantity ?? 0,
                Item = new Model.Product(){
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price ?? 0
                }
                }).ToList();

                o.OrderItems = orderLines;
            }
            return orders;
        }

        public List<Model.Order> GetAllOrdersByCustomerByCost(int customerId)
        {
            List<Model.Order> orders = _context.Orders.Include("Customer").Where(Order => Order.CustomerId == customerId).Select(Order => new Model.Order{
                Id = Order.Id,
                Cust = new Model.Customer(){
                    Id = Order.Customer.Id,
                    Name = Order.Customer.Name,
                    PhoneNum = Order.Customer.PhoneNum
                },
                StoreAddress = Order.StoreAddress,
                Total = Order.Total ?? 0,
                DateOfOrder = Order.Date
            }).ToList();
            foreach(Model.Order o in orders)
            {
                List<Model.OrderLine> orderLines = _context.OrderLines.Include("Product").Where(ol => ol.OrderId == o.Id).Select(i => new Model.OrderLine{
                Id = i.Id,
                Quantity = i.Quantity ?? 0,
                Item = new Model.Product(){
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price ?? 0
                }
                }).ToList();

                o.OrderItems = orderLines;
            }
            return orders;
        }

        public List<Model.Order> GetAllOrdersByStoreByDate(string storeAddress)
        {
            List<Model.Order> orders = _context.Orders.Include("Customer").Where(Order => Order.StoreAddress == storeAddress).Select(Order => new Model.Order{
                Id = Order.Id,
                Cust = new Model.Customer(){
                    Id = Order.Customer.Id,
                    Name = Order.Customer.Name,
                    PhoneNum = Order.Customer.PhoneNum
                },
                StoreAddress = Order.StoreAddress,
                Total = Order.Total ?? 0,
                DateOfOrder = Order.Date
            }).ToList();
            foreach(Model.Order o in orders)
            {
                List<Model.OrderLine> orderLines = _context.OrderLines.Include("Product").Where(ol => ol.OrderId == o.Id).Select(i => new Model.OrderLine{
                Id = i.Id,
                Quantity = i.Quantity ?? 0,
                Item = new Model.Product(){
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price ?? 0
                }
                }).ToList();

                o.OrderItems = orderLines;
            }
            return orders;
        }

        public List<Model.Order> GetAllOrdersByStoreByCost(string storeAddress)
        {
            List<Model.Order> orders = _context.Orders.Include("Customer").Where(Order => Order.StoreAddress == storeAddress).Select(Order => new Model.Order{
                Id = Order.Id,
                Cust = new Model.Customer(){
                    Id = Order.Customer.Id,
                    Name = Order.Customer.Name,
                    PhoneNum = Order.Customer.PhoneNum
                },
                StoreAddress = Order.StoreAddress,
                Total = Order.Total ?? 0,
                DateOfOrder = Order.Date
            }).ToList();
            foreach(Model.Order o in orders)
            {
                List<Model.OrderLine> orderLines = _context.OrderLines.Include("Product").Where(ol => ol.OrderId == o.Id).Select(i => new Model.OrderLine{
                Id = i.Id,
                Quantity = i.Quantity ?? 0,
                Item = new Model.Product(){
                    Id = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price ?? 0
                }
                }).ToList();

                o.OrderItems = orderLines;
            }
            return orders;
        }
    }
}