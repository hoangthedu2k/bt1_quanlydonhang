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
          
            Order order = new Order
            {
                Customer = new Customer { FullName = req.CustomerName },
                Items = req.Items,
                TotalAmount = req.TotalAmount,
                Status = req.Status,
            };
            _orders.Add(order);
            return await Task.FromResult(QuanLyDonHang.Helper.OrderConvert.ToOrderResponse(order));

        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await Task.FromResult(_orders);
        }       

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
        }

        public async Task<List<OrderStatistic>> StatisticalAsync()
        {
           var stats = _orders
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatistic(g.Key, g.Count(), g.Sum(x => x.TotalAmount)))
                .ToList();
            return await Task.FromResult(stats);
        }

        public async Task<bool> UpdateStatusAsync(Guid id, OrderStatus status)
        {
            var order = await GetByIdAsync(id);
            if (order == null) return false;

            order.Status = status;
            return await Task.FromResult(true);
        }
    }
}
