using EmployeeManagementSystem.Database;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Services
{
    public class TaskService
    {
        private readonly DbContext _dbContext;

        // Constructor ensures that dbContext is not null
        public TaskService(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));  // Ensure dbContext is not null
        }

        public void AddTask(Tasks task)
        {
            _dbContext.AddTask(task); // Save task to database
        }

        public List<Tasks> GetTasks()
        {
            return _dbContext.GetTasks(); // Get list of tasks from the database
        }

        public void MarkTaskComplete(int taskId)
        {
            _dbContext.MarkTaskComplete(taskId); // Mark task as complete
        }

        public void DeleteTask(int taskId)
        {
            _dbContext.DeleteTask(taskId); // Delete a task from database
        }
    }
}
