using QuanLyDonHang.Entity;
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
        public Order GetById(int id);

        public void Add(Order order);
        public bool UpdateStatus(int id, string status);



    }
}
