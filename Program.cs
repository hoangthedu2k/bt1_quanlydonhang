// See https://aka.ms/new-console-template for more information
using QuanLyDonHang.Entity;
using QuanLyDonHang.Enum;
using QuanLyDonHang.Message.Request;
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
    Console.WriteLine("5. Xem thống kê đơn hàng");
    Console.WriteLine("0. Thoát");

    Console.Write("Lựa chọn của bạn: ");
    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            var orders = await repository.GetAllAsync();
            foreach (Order item in orders)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.GetSummary());
                Console.WriteLine(item.Status);
            }
            break;
        case "2":
            Console.Write("Nhập ID đơn hàng: ");
            Guid id = Guid.Parse(Console.ReadLine() ?? Guid.Empty.ToString());
            var order = await repository.GetByIdAsync(id);
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
            CreateOrderRequest request = new CreateOrderRequest(customerName, items, price, OrderStatus.Processing);

            var newOrder = await repository.AddAsync(request);
            Console.WriteLine($"Đơn hàng đã được tạo: ");
            Console.WriteLine($"ID: {newOrder.Id}");
            Console.WriteLine($"Khách hàng: {newOrder.CustomerName}");
            Console.WriteLine($"Tổng số tiền: {newOrder.TotalAmount}");
            Console.WriteLine($"Trạng thái: {newOrder.Status}");
            break;
        case "4":
            Console.Write("Nhập ID đơn hàng cần cập nhật: ");
            Guid updateId = Guid.Parse(Console.ReadLine() ?? Guid.Empty.ToString());
            Console.Write("Nhập trạng thái mới: ");
            string newStatus = Console.ReadLine() ?? string.Empty;
            if (!Enum.TryParse(newStatus, ignoreCase: true, out OrderStatus parsedStatus))
            {   
                Console.WriteLine("Trạng thái không hợp lệ.");
                break;
            }
            if (! await repository.UpdateStatusAsync(updateId, parsedStatus))
            {
                Console.WriteLine("Không thể cập nhật trạng thái đơn hàng.");
            }
            else
            {
                Console.WriteLine("Trạng thái đơn hàng đã được cập nhật.");
            }
            break;
        case "5":
            var statisticalObj = await repository.StatisticalAsync();
            
                Console.WriteLine("Số lượng đơn hàng theo trạng thái:");
                foreach (var item in statisticalObj)
                {
                    Console.WriteLine($"  {item.Status}: {item.Count}, Doanh thu: {item.TotalRevenue}");
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
