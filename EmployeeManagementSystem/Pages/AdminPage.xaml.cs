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

            
            LoadUsers();
        }

        public void LoadUsers()
        {
            var users = _authService.GetAllUsers();
            UsersListView.ItemsSource = users;
        }


        private async void OnAddUserClicked(object sender, EventArgs e)
        {
           
            var addUser = new AddUser(_authService, this);
            await Navigation.PushModalAsync(addUser);
        }
        private async void OnDeleteUser(object sender, EventArgs e)
        {
            // Get the ID from the button's CommandParameter
            var button = (Button)sender;
            var userId = (int)button.CommandParameter;

            // Find the user by ID
            var userToDelete = _authService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            if (userToDelete != null)
            {
                // Confirm deletion
                bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {userToDelete.Username}?", "Yes", "No");
                if (confirm)
                {
                    // Call the delete method
                    _authService.DeleteUser(userToDelete);
                    LoadUsers(); // Refresh the user list
                }
            }
            else
            {
                await DisplayAlert("Error", "User  not found.", "OK");
            }
        }

        public void RefreshUserList()
        {
            LoadUsers();
        }
    }
}