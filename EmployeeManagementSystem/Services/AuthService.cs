using EmployeeManagementSystem.Database;
using EmployeeManagementSystem.Models;
using SQLite;

namespace EmployeeManagementSystem.Services
{
    public class AuthService
    {

        private readonly DbContext _dbContext;

        public AuthService(DbContext dbContext)
        {
            _dbContext = dbContext;
            SeedData(); // Call the method to add default users
        }

        public User Login(string username, string password)
        {
            // Find the user with matching credentials
            User user = _dbContext.GetUser(username, password);
            User.CurrentUser = user; // Set the static property
            return user;

        }

        public List<User> GetAllUsers()
        {
            // Fetch all users from the database
            return _dbContext.GetUsers();
        }

        private void SeedData()
        {
            // Seed only if no users exist in the database
            if (_dbContext.GetUser("admin", "admin123") == null && _dbContext.GetUser("employee", "employee123") == null)
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Password = "admin123",
                    Role = "Admin",
                    EmployeeName = "Admin User",
                    Salary = 0
                };

                var employeeUser = new User
                {
                    Username = "employee",
                    Password = "employee123",
                    Role = "Employee",
                    EmployeeName = "Employee User",
                    Salary = 50000
                };

                // Add users to the database
                _dbContext.AddUser(adminUser);
                _dbContext.AddUser(employeeUser);
            }
        }


    }
}
