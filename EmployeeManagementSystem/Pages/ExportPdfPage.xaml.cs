using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Storage;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using PdfSharpCore.Fonts;

namespace EmployeeManagementSystem.Pages
{
    public partial class ExportPdfPage : ContentPage
    {
        private readonly TaskService _taskService;
        private List<Tasks> _allTasks = new();

        public ExportPdfPage(TaskService taskService)
        {
            InitializeComponent();
            _taskService = taskService;
            
        }

        private void LoadTasks()
        {
            _allTasks = _taskService.GetTasks();
            TaskListView.ItemsSource = _allTasks;
        }

       
        private void OnSearchClicked(object sender, EventArgs e)
        {
            if (int.TryParse(EmployeeIdEntry.Text, out int employeeId))
            {
                var completedTasks = _taskService.GetCompletedTasksByEmployeeId(employeeId);

                if (completedTasks.Any())
                {
                    TaskListView.ItemsSource = completedTasks;
                }
                else
                {
                    DisplayAlert("No Results", $"No completed tasks found for Employee ID: {employeeId}", "OK");
                    TaskListView.ItemsSource = null;
                }
            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter a valid numeric Employee ID.", "OK");
            }
        }


      
        private async void OnExportToPdfClicked(object sender, EventArgs e)
        {
            GlobalFontSettings.FontResolver = CustomFontResolver.Instance;

            var tasksToExport = TaskListView.ItemsSource.Cast<Tasks>().ToList();
            if (tasksToExport.Count == 0)
            {
                await DisplayAlert("Export Failed", "No tasks available to export.", "OK");
                return;
            }

            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12, XFontStyle.Regular);

            double yPoint = 40;
            gfx.DrawString("Task List", new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                           new XRect(0, yPoint, page.Width, 20), XStringFormats.TopCenter);
            yPoint += 40;

            foreach (var task in tasksToExport)
            {
                string line = $"ID: {task.Id} | EmpID: {task.EmployeeId} | Desc: {task.TaskDescription} | " +
                              $"Status: {task.Status} | Deadline: {task.Deadline} | Completed: {task.CompletedDate}";

                gfx.DrawString(line, font, XBrushes.Black, new XRect(40, yPoint, page.Width - 80, page.Height), XStringFormats.TopLeft);
                yPoint += 20;

                if (yPoint > page.Height - 40)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPoint = 40;
                }
            }

            var fileName = $"TaskList_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            using (var stream = File.Create(filePath))
            {
                document.Save(stream);
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Exported Task List",
                File = new ShareFile(filePath)
            });
        }

    }
}
