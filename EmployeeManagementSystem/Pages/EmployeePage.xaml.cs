using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Pages;

public partial class EmployeePage : ContentPage
{
    private readonly TaskService _taskService;
    private Tasks _selectedTask;
    private User _currentUser;
    public EmployeePage(TaskService taskService)
    {
        InitializeComponent();
        _taskService = taskService;
        _currentUser = User.CurrentUser;
        loadTask();
    }




    private async void OnMarkAsCompleteClicked(object sender, EventArgs e)
    {
       
        if (TaskListView.SelectedItem is Tasks selectedTask)
        {
           
            bool confirm = await DisplayAlert("Confirm Complete",
                $"Are you sure you want to mark this task as complete?", "Yes", "No");
            if (confirm)
            {
              
                _taskService.MarkTaskComplete(selectedTask.Id);
                
                loadTask(); 
                          
                TaskListView.SelectedItem = null;
            }
        }
        else
        {
           
            await DisplayAlert("No Selection", "Please select a task to mark as complete", "OK");
        }
    }


    public void loadTask()
    {
        // Check if current user is an employee
        if (_currentUser != null && _currentUser.Role == "Employee")
        {
            // Only get tasks for this employee
            var tasks = _taskService.GetTasksByEmployeeId(_currentUser.Id);
            TaskListView.ItemsSource = tasks;
        }
        else if (_currentUser != null && _currentUser.Role == "Admin")
        {
            // Admins can see all tasks
            var tasks = _taskService.GetTasks();
            TaskListView.ItemsSource = tasks;
        }
        else
        {
            // No user logged in or unknown role
            TaskListView.ItemsSource = new List<Tasks>();
        }
    }

}