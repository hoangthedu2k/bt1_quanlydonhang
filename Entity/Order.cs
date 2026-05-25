using QuanLyDonHang.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDonHang.Entity
{
    public class Order : BaseEntity
    {
        public Customer Customer { get; set; } = new Customer();
        public List<string> Items { get; set; } = new List<string>();

        public double TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string GetSummary()
        {
            return $"Order for {Customer.FullName}: {Items.Count} items, Total: ${TotalAmount:F2}";
        }
    }
}
