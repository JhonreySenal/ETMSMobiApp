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

        public AssignTaskModal(TaskService taskService, AssignTaskPage assignTaskPage)
        {
            InitializeComponent();
            _taskService = taskService; 
            _assignTaskPage = assignTaskPage;
        }

        private async void AssignTaskButton(object sender, EventArgs e)
        {
         
            string employeeIdInput = EmployeeIdEntry.Text; 
            DateTime deadline = DeadlineDatePicker.Date;
            string taskDescription = TaskDescriptionEntry.Text; 

          
            if (string.IsNullOrWhiteSpace(employeeIdInput) || string.IsNullOrWhiteSpace(taskDescription))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

           
            if (!int.TryParse(employeeIdInput, out int employeeId))
            {
                await DisplayAlert("Error", "Invalid Employee ID. Please enter a valid number.", "OK");
                return;
            }

          
            var newTask = new Tasks
            {
                EmployeeId = employeeId, 
                TaskDescription = taskDescription,
                Status = "Pending",
                Deadline = deadline,
                CompletedDate = "Not done yet"
            };

            try
            {
               
                _taskService.AddTask(newTask);
                _assignTaskPage.loadTask();

                
                await DisplayAlert("Success", "Task assigned successfully.", "OK");

                
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
           
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void CloseModal(object sender, EventArgs e)
        {
    
            await Navigation.PopModalAsync();
        }
    }
}
