using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Pages;

public partial class EmployeePage : ContentPage
{
    private readonly TaskService _taskService;
    private Tasks _selectedTask;
    public EmployeePage(TaskService taskService)
    {
        InitializeComponent();
        _taskService = taskService;
        loadTask();
    }




    private async void OnMarkAsCompleteClicked(object sender, EventArgs e)
    {
        // Check if an item is selected
        if (TaskListView.SelectedItem is Tasks selectedTask)
        {
            // Show confirmation dialog
            bool confirm = await DisplayAlert("Confirm Complete",
                $"Are you sure you want to mark this task as complete?", "Yes", "No");
            if (confirm)
            {
                // Mark the selected task as complete
                _taskService.MarkTaskComplete(selectedTask.Id);
                // Refresh the ListView
                loadTask(); // Make sure to use your method name for refreshing tasks
                            // Clear selection
                TaskListView.SelectedItem = null;
            }
        }
        else
        {
            // Inform the user that no item is selected
            await DisplayAlert("No Selection", "Please select a task to mark as complete", "OK");
        }
    }
    

    public void loadTask()
    {
        var tasks = _taskService.GetTasks();
        TaskListView.ItemsSource = tasks;
    }

}