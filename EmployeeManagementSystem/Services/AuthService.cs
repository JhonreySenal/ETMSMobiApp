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
            //ResetUsers();
        }

        public User Login(string username, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            User user = _dbContext.GetUser(username, hashedPassword);
            User.CurrentUser = user;
            return user;
        }


        public List<User> GetAllUsers()
        {
          
            return _dbContext.GetUsers();
        }

        public void SeedData()
        {
            if (_dbContext.GetUser("admin", PasswordHelper.HashPassword("admin123")) == null &&
                _dbContext.GetUser("employee", PasswordHelper.HashPassword("employee123")) == null)
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Password = PasswordHelper.HashPassword("admin123"),
                    Role = "Admin",
                    EmployeeName = "Admin User",
                };

                var employeeUser = new User
                {
                    Username = "employee",
                    Password = PasswordHelper.HashPassword("employee123"),
                    Role = "Employee",
                    EmployeeName = "Employee User",
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
        //public void ResetUsers()
        //{
        //    _dbContext.ResetUsersWithIdRestart();
        //}

        }
}
