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
            SeedData();
        }

        public User Login(string username, string password)
        {
         
            User user = _dbContext.GetUser(username, password);
            User.CurrentUser = user; 
            return user;

        }

        public List<User> GetAllUsers()
        {
          
            return _dbContext.GetUsers();
        }

        private void SeedData()
{
    if (_dbContext.GetUser("admin", "admin123") == null && _dbContext.GetUser("employee", "employee123") == null)
    {
        var adminUser = new User
        {
          
            Username = "admin",
            Password = "admin123",
            Role = "Admin",
            EmployeeName = "Admin User",
         //   Salary = 0
        };

        var employeeUser = new User
        {
          
            Username = "employee",
            Password = "employee123",
            Role = "Employee",
            EmployeeName = "Employee User",
           // Salary = 50000
        };

        _dbContext.AddUser(adminUser);
        _dbContext.AddUser(employeeUser);
    }
}

        public void AddUser(User user)
        {
          
            _dbContext.AddUser(user);
        }

        public void DeleteUser(User user)
        {
         
            _dbContext.DeleteUser(user);
        }


    }
}
