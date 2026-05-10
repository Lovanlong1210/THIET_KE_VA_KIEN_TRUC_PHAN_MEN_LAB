using System;
using System.Text;
using System.Threading.Tasks;
using TodoApp;
using TodoAppV2;

public class Program
{
    private static async Task Main(string[] args)
    {
        // Hỗ trợ hiển thị tiếng Việt trên Console
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Khởi tạo Repository với chuỗi kết nối đã được sửa chuẩn
        var repository = new TodoRepository(@"Server=DESKTOP-OEFG8EG\SQLEXPRESS; Database=TodoDB; Integrated Security=true; TrustServerCertificate=true");

        // Khởi tạo Service và UI
        var service = new TodoService(repository);
        var ui = new TodoUI(service);

        // Chạy chương trình
        await ui.Run();
    }
}