// See https://aka.ms/new-console-template for more information
using QuanLyDonHang.Entity;
using QuanLyDonHang.Enum;
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
            foreach(Order item in orders)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.GetSummary());
                Console.WriteLine(item.Status);
            }
            break;
        case "2":
            Console.Write("Nhập ID đơn hàng: ");
            Guid id = Guid.Parse(Console.ReadLine() ?? Guid.Empty.ToString());
            var order = repository.GetById(id);
            Console.WriteLine(order != null ? order.GetSummary() : "Không tìm thấy đơn hàng.");
            break;
        case "3":
            Console.Write("Tên khách hàng: ");
            string customerName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Tên sản phẩm (Ngan cach nhau boi dau phay): ");
            var itemInput = Console.ReadLine() ?? string.Empty;
            var items = itemInput.Split(',').Select(i => i.Trim()).ToList();
            Console.WriteLine("Tổng số tiền: ");
            double price = Convert.ToDouble(Console.ReadLine());

            var newOrder = new Order
            {
                Customer = new Customer { FullName = customerName },
                Items = items,
                TotalAmount = price,
                Status = OrderStatus.Processing
            };
            repository.Add(newOrder);
            break;
        case "4":
            Console.Write("Nhập ID đơn hàng cần cập nhật: ");
            Guid updateId = Guid.Parse(Console.ReadLine() ?? Guid.Empty.ToString());
            Console.Write("Nhập trạng thái mới: ");
            string newStatus = Console.ReadLine() ?? string.Empty;
            if (!Enum.TryParse(newStatus, out OrderStatus parsedStatus))
            {
                Console.WriteLine("Trạng thái không hợp lệ.");
                break;
            }
            if (!repository.UpdateStatus(updateId, parsedStatus))
            {
                Console.WriteLine("Không thể cập nhật trạng thái đơn hàng.");
            }
            else
            {
                Console.WriteLine("Trạng thái đơn hàng đã được cập nhật.");
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
