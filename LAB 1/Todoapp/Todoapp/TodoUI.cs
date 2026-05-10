using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TodoAppV2
{
    public class TodoUI
    {
        private readonly TodoService _todoService;
        public TodoUI(TodoService todoService)
        {
            _todoService = todoService;
        }
        public async Task Run()
        {
            while (true)
            {
                Console.Clear();
                await ShowTodos();
                ShowMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

              
                AddTodo();
                        break;
                    case "2":
                        DeleteTodo();
                        break;
                    case "3":
                        ToggleTodo();
                        break;
                    case "4":
                        EditTodo();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("L ựa ch ọn không h ợp l ệ!");
                        break;
                }
                Console.WriteLine("Nhấ n Enter đ ể ti ếp t ục...");
                Console.ReadLine();
            }
        }
        private async Task ShowTodos()
        {
            var todos = await _todoService.GetAllAsync();
            Console.WriteLine("=== DANH SÁCH CÔNG VI ỆC ===");
            foreach (var todo in todos)
            {
                Console.WriteLine(todo);
            }
            if (todos.Count == 0)
                Console.WriteLine("Chưa có công vi ệc nào.");
        }
        private void ShowMenu()
        {
            Console.WriteLine(" \nChức năng:");
            Console.WriteLine("1. Thêm Todo");
            Console.WriteLine("2. Xoá Todo");
            Console.WriteLine("3. Đánh d ấ u hoàn thành");
            Console.WriteLine("4. S ửa n ội dung");
            Console.WriteLine("0. Thoát");
            Console.Write("Ch ọn: ");
        }
        private async Task AddTodo()
        {
            Console.Write("Nh ập n ội dung công vi ệc: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
                await _todoService.addASync(title);
        }
        private async Task DeleteTodo()
        {
            Console.Write("Nh ập ID công vi ệc c ần xoá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                await _todoService.DeleteAsync(id);
        }
        private async Task ToggleTodo()
        {
            Console.Write("Nh ập ID c ần đánh d ấ u hoàn thành: ");
        if (int.TryParse(Console.ReadLine(), out int id))
                await _todoService.ToggleTodoAsync(id);
        }
        private async Task EditTodo()
        {
            Console.Write("Nh ập ID c ần s ửa: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Nh ập n ội dung m ới: ");
                var newTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newTitle))
                    await _todoService.UpdateAsync(id, newTitle);
            }
        }
    }
}