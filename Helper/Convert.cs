using QuanLyDonHang.Message.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Helper
{
    public class Convert
    {
        public static OrderResponse ToOrderResponse(Entity.Order order)
        {
            return new OrderResponse(order.Id, order.Customer.FullName,order.TotalAmount,  order.Status, order.CreatedAt);
        }
    }
}
