using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.Maui.Controls;
using System;

namespace EmployeeManagementSystem.Pages
{
    public partial class AssignTaskModal : ContentPage
    {
        private readonly TaskService _taskService;
        private readonly AssignTaskPage _assignTaskPage;

        // Constructor accepts TaskService
        public AssignTaskModal(TaskService taskService, AssignTaskPage assignTaskPage)
        {
            InitializeComponent();
            _taskService = taskService; // Store the TaskService
            _assignTaskPage = assignTaskPage;
        }

        private async void AssignTaskButton(object sender, EventArgs e)
        {
            // Retrieve values from the input fields
            string employeeIdInput = EmployeeIdEntry.Text; // EmployeeId from the Entry field
            DateTime deadline = DeadlineDatePicker.Date; // Deadline from DatePicker
            string taskDescription = TaskDescriptionEntry.Text; // TaskDescription from the Entry field

            // Input validation
            if (string.IsNullOrWhiteSpace(employeeIdInput) || string.IsNullOrWhiteSpace(taskDescription))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Convert EmployeeId input to integer
            if (!int.TryParse(employeeIdInput, out int employeeId))
            {
                await DisplayAlert("Error", "Invalid Employee ID. Please enter a valid number.", "OK");
                return;
            }

            // Create a new task object
            var newTask = new Tasks
            {
                EmployeeId = employeeId, // Set the employeeId
                TaskDescription = taskDescription,
                Status = "Pending", // Default value
                Deadline = deadline, // Use the DateTime directly (no need to convert to string)
                CompletedDate = "Not done yet" // Default value for CompletedDate
            };

            try
            {
                // Save the new task to the database using the TaskService
                _taskService.AddTask(newTask);
                _assignTaskPage.loadTask();

                // Show success message
                await DisplayAlert("Success", "Task assigned successfully.", "OK");

                // Close the modal after successful task assignment
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., database issues)
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void CloseModal(object sender, EventArgs e)
        {
            // Close the modal when the close button is clicked
            await Navigation.PopModalAsync();
        }
    }
}
