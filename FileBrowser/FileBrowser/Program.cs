using FileBrowser.Services;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System.Data.Common;

namespace FileBrowser
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY5MTgyMEAzMjM4MmUzMDJlMzBkajhJNXQxZDQ5OStkN3Z0ZDd6dWhaUkxFd2k5RjVKdzhzSG1UbUJBenNnPQ==");

            var serviceProvider = ConfigureServices;

            var mainForm = serviceProvider.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }
        private static IServiceProvider ConfigureServices
        {
            get
            {
                // Create a new ServiceCollection
                var services = new ServiceCollection();
                services.AddSingleton<Form1>();
                services.AddWindowsFormsBlazorWebView();

                
                services.AddMemoryCache();
                services.AddSyncfusionBlazor();



                // Register services
                services.AddSingleton<DbConnectionService>();
                services.AddSingleton<RootDirectoryService>();
                services.AddSingleton<CustomerService>();
                services.AddSingleton<FileRegistryService>();


                




                // Build and return the ServiceProvider
                return services.BuildServiceProvider();
            }

        }
    }
}