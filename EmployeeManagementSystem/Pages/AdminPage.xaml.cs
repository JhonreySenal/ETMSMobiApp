using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Pages;

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
    
    private void LoadUsers()
    {
        var users = _authService.GetAllUsers();
        UsersListView.ItemsSource = users;
    }

}
