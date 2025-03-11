using EmployeeManagementSystem.Models;
using Microsoft.Maui.Controls;

namespace EmployeeManagementSystem.Pages
{
    public partial class AppShell : Shell
    {
        public AppShell(User user)
        {
            InitializeComponent();
            SetUserRole(user);
            Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
            Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
            Routing.RegisterRoute(nameof(AssignTaskPage), typeof(AssignTaskPage));
            Routing.RegisterRoute(nameof(ExportPdfPage), typeof(ExportPdfPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

        }


        private void SetUserRole(User user)
        {
            if (user == null) return;

            // Show or hide flyout items based on the user's role
            AdminFlyout.IsVisible = user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            EmployeeFlyout.IsVisible = user.Role.Equals("Employee", StringComparison.OrdinalIgnoreCase);
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Logout",
                "Are you sure you want to log out?",
                "Yes",
                "Cancel"
            );

            if (confirm)
            {
                // Navigate to the login page or reset the app state
                 Application.Current.MainPage = new LoginPage();
            }
        }

    }

}

