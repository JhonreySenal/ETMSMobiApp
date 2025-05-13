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
        List<Tasks> tasks = new List<Tasks>();

        // Check if current user is an employee
        if (_currentUser != null && _currentUser.Role == "Employee")
        {
            // Only get tasks for this employee
            tasks = _taskService.GetTasksByEmployeeId(_currentUser.Id);

            // Sort tasks by deadline (newest first)
            if (tasks != null && tasks.Any())
            {
                tasks = tasks.OrderByDescending(t => t.Deadline).ToList();
            }

            TaskListView.ItemsSource = tasks;

            // Check for upcoming deadlines for employee's tasks
            CheckForUpcomingDeadlines(tasks);
        }
       
        else
        {
      
            TaskListView.ItemsSource = new List<Tasks>();
        }
    }

    private async void CheckForUpcomingDeadlines(IEnumerable<Tasks> tasks)
    {
        // Get current date/time
        DateTime now = DateTime.Now;

        // Tasks with deadlines within the next 24 hours
        var upcomingTasks = new List<Tasks>();

        foreach (var task in tasks)
        {
            // Skip tasks that are already completed
            if (task.Status == "Completed")
                continue;

            DateTime deadlineDate;

            // Parse the deadline (adjust as needed based on your Task class implementation)
            if (task.Deadline is DateTime deadline)
            {
                deadlineDate = deadline;
            }
            else if (DateTime.TryParse(task.Deadline.ToString(), out DateTime parsedDate))
            {
                deadlineDate = parsedDate;
            }
            else
            {
                // Skip if we can't parse the deadline
                continue;
            }

            // Calculate time until deadline
            TimeSpan timeUntilDeadline = deadlineDate - now;

            // Check if deadline is within next 24 hours but not passed
            if (timeUntilDeadline.TotalHours <= 24 && timeUntilDeadline.TotalHours > 0)
            {
                upcomingTasks.Add(task);
            }
        }

        // If there are upcoming tasks, show an alert
        if (upcomingTasks.Any())
        {
            string alertTitle = "Upcoming Deadlines";
            string taskList = "";

            // Customize message based on user role
            if (_currentUser.Role == "Employee")
            {
                taskList = "You have tasks due within the next 24 hours:\n\n";
            }

            foreach (var task in upcomingTasks)
            {
                taskList += $"- Task ID {task.Id}: {task.TaskDescription} (Due: {task.Deadline})\n";
            }

            await DisplayAlert(alertTitle, taskList, "OK");
        }
    }

}