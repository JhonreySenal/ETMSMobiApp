using SQLite;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Database
{
    public class DbContext
    {
        private readonly SQLiteConnection _database;

        public SQLiteConnection Database => _database; // Expose the database connection if needed

        public DbContext(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<User>();
            _database.CreateTable<Tasks>();
        }

        // 🟩 User Management
        public void AddUser(User user)
        {
            _database.Insert(user);
        }

        public User GetUser(string username, string password)
        {
            return _database.Table<User>().FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public List<User> GetUsers()
        {
            return _database.Table<User>().ToList();
        }

        // 🟩 Task Management
        public void AddTask(Tasks task)
        {
            _database.Insert(task);
        }

        public List<Tasks> GetTasks()
        {
            return _database.Table<Tasks>().ToList();
        }

        public void MarkTaskComplete(int taskId)
        {
            var task = _database.Find<Tasks>(taskId);
            if (task != null)
            {
                task.Status = "Completed";
                task.CompletedDate = DateTime.Now;
                _database.Update(task);
            }
        }

        public void DeleteTask(int taskId)
        {
            var task = _database.Find<Tasks>(taskId);
            if (task != null)
            {
                _database.Delete(task);
            }
        }
    }
}
