using FileSearchSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace FileSearchSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
                services.AddWindowsFormsBlazorWebView();
                services.AddSingleton<Form1>();
                //services.AddSingleton<Form2>();

                // Radzen専用サービスの登録
                //services.AddRadzenComponents();
                //services.AddScoped<DialogService>();
                //services.AddScoped<NotificationService>();
                //services.AddScoped<TooltipService>();
                //services.AddScoped<ContextMenuService>();

                // Register services
                services.AddSingleton<DbConnectionService>();
                services.AddSingleton<FormInteractionService>();
                services.AddSingleton<FileTransferHistoryService>();
                services.AddSingleton<FileRegistryService>();



                // Build and return the ServiceProvider
                return services.BuildServiceProvider();
            }

        }
    }
}