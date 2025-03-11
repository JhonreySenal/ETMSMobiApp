using EmployeeManagementSystem.Database;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Pages;
using EmployeeManagementSystem.Services;
using Microsoft.Extensions.Logging;
using System;

namespace EmployeeManagementSystem;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "EmployeeManagement.db");
        var dbContext = new DbContext(dbPath);
        var authService = new AuthService(dbContext);

        builder.Services.AddSingleton(authService);
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<User>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
