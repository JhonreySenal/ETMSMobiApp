using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.Maui.Controls;

namespace EmployeeManagementSystem.Pages
{
    public partial class AddUser : ContentPage
    {
        private readonly AuthService _authService;
        private readonly AdminPage _adminPage; 

        public AddUser(AuthService authService, AdminPage adminPage)
        {
            InitializeComponent();
            _authService = authService; 
            _adminPage = adminPage; 
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
          
            var newUser = new User
            {
                EmployeeName = EmployeeNameEntry.Text,
                Username = UsernameEntry.Text,
                Password = PasswordHelper.HashPassword(PasswordEntry.Text),
                Role = RolePicker.SelectedItem?.ToString(), 
               
            };

           
            _authService.AddUser(newUser);

            
            _adminPage.LoadUsers();

         
            await Navigation.PopModalAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
     
            await Navigation.PopModalAsync();
        }
    }
}