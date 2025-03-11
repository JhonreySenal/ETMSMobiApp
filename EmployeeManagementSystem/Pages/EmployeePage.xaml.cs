using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Pages;

public partial class EmployeePage : ContentPage
{
    public EmployeePage(User user)
    {
        InitializeComponent();
        WelcomeLabel.Text = $"Welcome, {user.EmployeeName}";
        SalaryLabel.Text = $"Your Salary: {user.Salary:C}";
    }

   
}