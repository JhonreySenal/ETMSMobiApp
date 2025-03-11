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

        public TaskService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddTask(Tasks task)
        {
            _dbContext.AddTask(task);
        }

        public List<Tasks> GetTasks()
        {
            return _dbContext.GetTasks();
        }

        public void MarkTaskComplete(int taskId)
        {
            _dbContext.MarkTaskComplete(taskId);
        }

        public void DeleteTask(int taskId)
        {
            _dbContext.DeleteTask(taskId);
        }

        public void SeedTasks()
        {
            if (!_dbContext.GetTasks().Any())
            {
                var initialTasks = new List<Tasks>
                {
                    new Tasks { EmployeeId = 1, TaskDescription = "Complete project documentation", Status = "Pending", Deadline = DateTime.Now.AddDays(7) },
                    new Tasks { EmployeeId = 2, TaskDescription = "Fix login bug", Status = "In Progress", Deadline = DateTime.Now.AddDays(3) },
                    new Tasks { EmployeeId = 3, TaskDescription = "Update employee records", Status = "Pending", Deadline = DateTime.Now.AddDays(5) }
                };

                foreach (var task in initialTasks)
                {
                    _dbContext.AddTask(task);
                }
            }
        }
    }
}
