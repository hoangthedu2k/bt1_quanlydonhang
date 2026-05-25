using QuanLyDonHang.Entity;
using QuanLyDonHang.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Service
{
    public class OrderRepository : IOrderRepository
    {
        List<Order> _orders= new();
        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order? GetById(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public bool UpdateStatus(Guid id, OrderStatus status)
        {
            var order = GetById(id);
            if (order == null) return false;

            order.Status = status.ToString();
            return true;
        }
    }
}
