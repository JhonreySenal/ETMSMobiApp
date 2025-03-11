namespace EmployeeManagementSystem;
    using EmployeeManagementSystem.Pages;

    public partial class App : Application
{
    [Obsolete]
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new LoginPage());
    }

  
}
