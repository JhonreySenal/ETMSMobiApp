using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.Maui.Controls;

namespace EmployeeManagementSystem.Pages
{
    public partial class AddUser : ContentPage
    {
        private readonly AuthService _authService;
        private readonly AdminPage _adminPage; // Reference to AdminPage

        public AddUser(AuthService authService, AdminPage adminPage)
        {
            InitializeComponent();
            _authService = authService; // Initialize the AuthService
            _adminPage = adminPage; // Store the reference to AdminPage
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            // Create a new user from the input fields
            var newUser = new User
            {
                EmployeeName = EmployeeNameEntry.Text,
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                Role = RolePicker.SelectedItem?.ToString(), // Get selected role
                //Salary = double.TryParse(SalaryEntry.Text, out double salary) ? salary : 0 // Convert to decimal
            };

            // Add the new user to the database using AuthService
            _authService.AddUser(newUser);

            // Refresh the user list in AdminPage
            _adminPage.LoadUsers();

            // Close the modal
            await Navigation.PopModalAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            // Close the modal without adding a user
            await Navigation.PopModalAsync();
        }
    }
}