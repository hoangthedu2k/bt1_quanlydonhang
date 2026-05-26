using QuanLyDonHang.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Message.Response
{
    public record OrderResponse(Guid Id, string CustomerName, double TotalAmount, OrderStatus Status,DateTime CreatedAt);
    
    
}
