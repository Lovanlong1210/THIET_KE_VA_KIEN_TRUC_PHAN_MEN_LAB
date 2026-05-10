using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp; // Đảm bảo class Todo nằm trong namespace này hoặc đã được using đúng

namespace TodoAppV2
{
    public class TodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection ConnectDB()
        {
            SqlConnection database = new SqlConnection(_connectionString);
            return database;
        }

        // Lấy danh sách tất cả Todo
        public async Task<List<Todo>> GetAllAsync()
        {
            using (var database = ConnectDB())
            {
                var result = await database.QueryAsync<Todo>("SELECT * FROM Todos");
                return result.ToList();
            }
        }

        // Lấy 1 Todo theo ID
        public async Task<Todo> GetAsync(int id)
        {
            using (var database = ConnectDB())
            {
                // Dùng QueryFirstOrDefaultAsync để trả về null nếu không tìm thấy, tránh lỗi Exception
                string query = "SELECT * FROM Todos WHERE Id = @Id";
                var result = await database.QueryFirstOrDefaultAsync<Todo>(query, new { Id = id });
                return result;
            }
        }

        // Thêm mới Todo
        public async Task AddAsync(string title)
        {
            using (var database = ConnectDB())
            {
                string query = "INSERT INTO Todos (Title) VALUES (@Title)";
                await database.ExecuteAsync(query, new { Title = title });
            }
        }

        // Cập nhật Todo
        public async Task UpdateAsync(Todo todo)
        {
            using (var database = ConnectDB())
            {
                string query = @"UPDATE Todos 
                                 SET Title = @Title, 
                                     IsCompleted = @IsCompleted 
                                 WHERE Id = @Id";

                // Dapper sẽ tự động map các thuộc tính Title, IsCompleted, Id từ object todo
                await database.ExecuteAsync(query, todo);
            }
        }

        // Xóa Todo
        public async Task DeleteAsync(int id)
        {
            using (var database = ConnectDB())
            {
                string query = "DELETE FROM Todos WHERE Id = @Id";
                await database.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}