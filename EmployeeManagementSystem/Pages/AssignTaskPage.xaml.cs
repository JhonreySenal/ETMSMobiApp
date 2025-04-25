using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Pages
{
    public partial class AssignTaskPage : ContentPage
    {
        private readonly TaskService _taskService;

        // Constructor for AssignTaskPage, accepts TaskService
        public AssignTaskPage(TaskService taskService)
        {
            InitializeComponent();
            _taskService = taskService; // Store TaskService reference
            loadTask();
        }

        private async void OpenAssignTaskModal(object sender, EventArgs e)
        {
            // Create an instance of AssignTaskModal and pass TaskService
            var assignTaskModal = new AssignTaskModal(_taskService, this);
            await Navigation.PushModalAsync(assignTaskModal);
        }
        private async void OnDeleteSelectedClicked(object sender, EventArgs e)
        {
            // Check if an item is selected
            if (TaskListView.SelectedItem is Tasks selectedTask)
            {
                // Show confirmation dialog
                bool confirm = await DisplayAlert("Confirm Delete",
                    $"Are you sure you want to delete this task?", "Yes", "No");

                if (confirm)
                {
                    // Delete the selected task
                    _taskService.DeleteTask(selectedTask.Id);

                    // Refresh the ListView
                    loadTask();

                    // Clear selection
                    TaskListView.SelectedItem = null;
                }
            }
            else
            {
                // Inform the user that no item is selected
                await DisplayAlert("No Selection", "Please select a task to delete", "OK");
            }
        }


        public void loadTask()
        {
            var tasks = _taskService.GetTasks();
            TaskListView.ItemsSource = tasks;
        }
    }
}
