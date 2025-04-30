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
        var tasks = _taskService.GetTasks();
        TaskListView.ItemsSource = tasks;
    }

}