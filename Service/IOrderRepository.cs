using QuanLyDonHang.Entity;
using QuanLyDonHang.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Service
{
    internal interface IOrderRepository
    {
        public List<Order> GetAll();
        public Order GetById(Guid id);

        public void Add(Order order);
        public bool UpdateStatus(Guid id, OrderStatus status);



    }
}
