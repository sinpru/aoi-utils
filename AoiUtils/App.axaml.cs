using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AoiUtils.ViewModels;
using AoiUtils.Views;
using AoiUtils.Core.Services;
using AoiUtils.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AoiUtils;

public partial class App : Application
{
    public IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();

        // Core Services
        services.AddSingleton<SystemRunner>();
        services.AddSingleton<PackageManagerService>();
        services.AddSingleton<SettingsService>();

        // UI Services
        services.AddSingleton<LocalizationManager>();

        // ViewModels
        services.AddTransient<MainWindowViewModel>();

        Services = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
