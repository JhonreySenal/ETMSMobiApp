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

            double margin = 40;
            double yPoint = margin;
            double contentWidth = page.Width - 2 * margin;

            // Title
            gfx.DrawString($"Task List - {DateTime.Now:M-d-yy}",
                new XFont("Verdana", 14, XFontStyle.Bold),
                XBrushes.Black,
                new XRect(margin, yPoint, contentWidth, 20),
                XStringFormats.TopLeft);
            yPoint += 40;

            foreach (var task in tasksToExport)
            {
                string fullText = $"ID: {task.Id} | EmpID: {task.EmployeeId} | Desc: {task.TaskDescription} | " +
                                  $"Status: {task.Status} | Deadline: {task.Deadline} | Completed: {task.CompletedDate}";

                var wrappedLines = PdfHelper.WrapText(gfx, fullText, font, contentWidth);
                foreach (var line in wrappedLines)
                {
                    gfx.DrawString(line, font, XBrushes.Black,
                        new XRect(margin, yPoint, contentWidth, page.Height),
                        XStringFormats.TopLeft);
                    yPoint += 20;

                    if (yPoint > page.Height - margin)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPoint = margin;
                    }
                }

                yPoint += 10; // Extra space between tasks
            }

            // 📂 Save to app data directory (works on Android, iOS, Windows, etc.)
            var fileName = $"TaskList_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            using (var stream = File.Create(filePath))
            {
                document.Save(stream);
            }

            // ✅ Optional: Show saved path and offer sharing
            await DisplayAlert("Success", $"PDF saved to:\n{filePath}", "OK");

            // 🔄 Optionally share the file
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Exported Task List",
                File = new ShareFile(filePath)
            });
        }

    }

}
