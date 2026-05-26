using QuanLyDonHang.Entity;
using QuanLyDonHang.Enum;
using QuanLyDonHang.Message.Request;
using QuanLyDonHang.Message.Response;
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
        public async Task<OrderResponse> AddAsync(CreateOrderRequest req)
        {
            if (!System.Enum.TryParse(req.Status, ignoreCase: true, out OrderStatus parsedStatus))
            {
                throw new ArgumentException("Trạng thái không hợp lệ.");
            }
            Order order = new Order
            {
                Customer = new Customer { FullName = req.CustomerName },
                Items = req.Items,
                TotalAmount = req.TotalAmount,
                Status = parsedStatus
            };
            _orders.Add(order);
            return new OrderResponse(order.Id, order.Customer.FullName, order.TotalAmount, order.Status, order.CreatedAt);
        }
            
        public async Task<List<Order>> GetAllAsync()
        {
            return _orders;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public async Task<object> StatisticalAsync()
        {
           var stats = _orders
                .GroupBy(o => o.Status)
                .Select(g => new { Status = g.Key, Count = g.Count(), TotalRevenue = g.Sum(x => x.TotalAmount) })
                .ToList();
            return stats;
        }

        public async Task<bool> UpdateStatusAsync(Guid id, OrderStatus status)
        {
            var order = await GetByIdAsync(id);
            if (order == null) return false;

            order.Status = status;
            return true;
        }
    }
}
