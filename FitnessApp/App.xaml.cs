using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Fitness.ClassLibrary;
using System.Linq;
using Fitness.ClassLibrary.DBAccess;

namespace FitnessApp;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

	public App()
	{
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddSingleton<MainWindow>();
				services.AddScoped<IMuscleDA, MuscleDA>();
				services.AddScoped<IExerciseDA, ExerciseDA>();
				services.AddScoped<IRoutineDA, RoutineDA>();
            }).Build();
	}

	protected override async void OnStartup(StartupEventArgs e)
	{
		await AppHost!.StartAsync();

		var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
		startupForm.Show();
		
		
		
		base.OnStartup(e);
	}

	protected override async void OnExit(ExitEventArgs e)
	{
		await AppHost!.StopAsync();

		base.OnExit(e);
	}
}
