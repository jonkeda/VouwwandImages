using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using VouwwandImages.Database;
using VouwwandImages.ViewModels;

namespace VouwwandImages;

public partial class App
{
    private static ServiceProvider? _serviceProvider;

    public App()
    {
        ServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        UpdateDatabaseWithEnums();
    }

    private void UpdateDatabaseWithEnums()
    {
        EnumUpdater updater = App.GetService<EnumUpdater>();

        updater.Update();

    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddDbContext<VouwwandenDbContext>(options => { options.UseSqlite("Data Source = Employee.db"); });
        services.AddSingleton<MainWindow>();

        services.AddSingleton<DataViewModel>();
        services.AddSingleton<TranslatorViewModel>();
        services.AddSingleton<ImagesViewModel>();
        services.AddSingleton<PdfViewModel>();
        services.AddSingleton<EnumUpdater>();

    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
    }

    public static T GetService<T>()
    {
        return App._serviceProvider.GetService<T>();
    }
}

