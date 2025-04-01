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

        public void loadTask()
        {
            var tasks = _taskService.GetTasks();
            TaskListView.ItemsSource = tasks;
        }
    }
}
