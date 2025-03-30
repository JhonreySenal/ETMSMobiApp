using SQLite; // ✅ Use this instead of SQLite.Net.Attributes
using System;

namespace EmployeeManagementSystem.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement] // ✅ Ensures ID auto-increments
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public double Salary { get; set; }
        public string EmployeeName { get; set; }

        public static User CurrentUser { get; set; } // Keeps track of the logged-in user
    }
}
