using EmployeeManagementSystem.Database;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EmployeeManagementSystem.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly DbContext _dbHelper;
        private readonly AuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "EmployeeManagement.db");
            _dbHelper = new DbContext(dbPath);
            _authService = new AuthService(_dbHelper);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            try
            {
                var user = _authService.Login(username, password);
                if (user != null)
                {
                   
                    var appShell = new AppShell(user);
                    Application.Current.MainPage = appShell;

            
                    if (user.Role == "Admin")
                        await Shell.Current.GoToAsync("//AdminPage");
                    else if (user.Role == "Employee")
                        await Shell.Current.GoToAsync("//EmployeePage");
                }
                else
                {
                    await DisplayAlert("Login Failed", "Invalid credentials", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

    }
}