using QuanLyDonHang.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Message.Request
{
    public record CreateOrderRequest(string CustomerName, List<string> Items, double TotalAmount, OrderStatus Status);
    
   
}
