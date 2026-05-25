using QuanLyDonHang.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Service
{
    internal class OrderRepository : IOrderRepository
    {
        public void Add(Order order)
        {
            Console.WriteLine("Order added: " + order.Id);
        }

        public List<Order> GetAll()
        {
            Console.WriteLine("Retrieving all orders..."); return new List<Order>();
        }

        public Order GetById(int id)
        {
            Console.WriteLine("Retrieving order with ID: " + id); return new Order();
        }

        public bool UpdateStatus(int id, string status)
        {
            Console.WriteLine("Updating status for order with ID: " + id + " to: " + status);
            return true;
        }
    }
}
