using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Pages
{
    public partial class AssignTaskPage : ContentPage
    {
        private readonly TaskService _taskService;

        
        public AssignTaskPage(TaskService taskService)
        {
            InitializeComponent();
            _taskService = taskService; 
            loadTask();
            this.Appearing += (s, e) => loadTask();

        }

        private async void OpenAssignTaskModal(object sender, EventArgs e)
        {
            
            var assignTaskModal = new AssignTaskModal(_taskService, this);
            await Navigation.PushModalAsync(assignTaskModal);
        }
        private async void OnDeleteSelectedClicked(object sender, EventArgs e)
        {
          
            if (TaskListView.SelectedItem is Tasks selectedTask)
            {
                
                bool confirm = await DisplayAlert("Confirm Delete",
                    $"Are you sure you want to delete this task?", "Yes", "No");

                if (confirm)
                {
                   
                    _taskService.DeleteTask(selectedTask.Id);

                    
                    loadTask();

                    
                    TaskListView.SelectedItem = null;
                }
            }
            else
            {
               
                await DisplayAlert("No Selection", "Please select a task to delete", "OK");
            }
        }

        private Tasks _selectedTask; // Replace TaskModel with your actual model class

        private void OnTaskSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedTask = e.SelectedItem as Tasks;
        }

        private async void DeleteTask(object sender, EventArgs e)
        {
            if (TaskListView.SelectedItem is Tasks selectedTask)
            {
                bool confirm = await DisplayAlert("Confirm Delete",
                    $"Are you sure you want to delete Task ID {selectedTask.Id}?", "Yes", "No");

                if (confirm)
                {
                    _taskService.DeleteTask(selectedTask.Id); // Delete from DB
                    loadTask();                               // Reload the list from DB
                    TaskListView.SelectedItem = null;
                }
            }
            else
            {
                await DisplayAlert("No Selection", "Please select a task to delete.", "OK");
            }
        }


        public void loadTask()
        {
            var tasks = _taskService.GetTasks();
            TaskListView.ItemsSource = tasks;
        }
    }
}
