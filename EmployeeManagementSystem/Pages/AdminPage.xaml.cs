using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.Maui.Controls;

namespace EmployeeManagementSystem.Pages
{
    public partial class AdminPage : ContentPage
    {
        private readonly User _currentUser;
        private readonly AuthService _authService;

        public AdminPage(User user, AuthService authService)
        {
            InitializeComponent();
            _currentUser = user;
            _authService = authService;

            // Show the list of users on page load
            LoadUsers();
        }

        public void LoadUsers()
        {
            var users = _authService.GetAllUsers();
            UsersListView.ItemsSource = users;
        }


        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            // Create an instance of the AddUser  Page and pass the AuthService and this AdminPage
            var addUser = new AddUser(_authService, this);
            await Navigation.PushModalAsync(addUser);
        }

        public void RefreshUserList()
        {
            LoadUsers();
        }
    }
}