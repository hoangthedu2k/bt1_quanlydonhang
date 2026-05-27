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
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAllAsync();
        public Task<Order?> GetByIdAsync(Guid id);

        public Task<OrderResponse> AddAsync(CreateOrderRequest req);
        public Task<bool> UpdateStatusAsync(Guid id, OrderStatus status);

        public Task<List<OrderStatistic>> StatisticalAsync();



    }
}
