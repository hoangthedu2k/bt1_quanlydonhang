// See https://aka.ms/new-console-template for more information
using QuanLyDonHang.Entity;
using QuanLyDonHang.Service;

// obtain an implementation of IOrderRepository (replace with your concrete instance or DI)
IOrderRepository repository = new OrderRepository();
string input = string.Empty;

do { Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
    Console.WriteLine("Chọn chức năng:");
    Console.WriteLine("1. Xem tất cả đơn hàng");
    Console.WriteLine("2. Xem đơn hàng theo ID");
    Console.WriteLine("3. Thêm đơn hàng mới");
    Console.WriteLine("4. Cập nhật trạng thái đơn hàng");
    Console.WriteLine("0. Thoát");

    Console.Write("Lựa chọn của bạn: ");
    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            var orders = repository.GetAll();
            Console.WriteLine($"Tổng số đơn hàng: {orders.Count}");
            break;
        case "2":
            Console.Write("Nhập ID đơn hàng: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            var order = repository.GetById(id);
            Console.WriteLine(order != null ? order.GetSummary() : "Không tìm thấy đơn hàng.");
            break;
        case "3":
            var newOrder = new Order
            {
                Customer = new Customer { FullName = "Khách hàng mới" },
                Items = new List<string> { "Sản phẩm A", "Sản phẩm B" },
                TotalAmount = 100.0,
                Status = "Mới"
            };
            repository.Add(newOrder);
            break;
        case "4":
            Console.Write("Nhập ID đơn hàng cần cập nhật: ");
            int updateId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nhập trạng thái mới: ");
            string newStatus = Console.ReadLine() ?? string.Empty;
            bool updated = repository.UpdateStatus(updateId, newStatus);
            if (updated)
            {
                Console.WriteLine("Trạng thái đơn hàng đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không thể cập nhật trạng thái đơn hàng.");
            }
            break;
        case "0":
            Console.WriteLine("Thoát chương trình.");
            return;
        default:
            Console.WriteLine("Lựa chọn không hợp lệ.");
            break;
    }
} while (input != "0");
