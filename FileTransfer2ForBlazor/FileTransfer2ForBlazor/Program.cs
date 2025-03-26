using System;
using System.Windows.Forms;
using FileTransfer2ForBlazor.Models;
using FileTransfer2ForBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace FileTransfer2ForBlazor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize DI container
            var serviceProvider = ConfigureServices;

            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
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

                // Radzen専用サービスの登録
                services.AddRadzenComponents();
                services.AddScoped<DialogService>();
                services.AddScoped<NotificationService>();
                services.AddScoped<TooltipService>();
                services.AddScoped<ContextMenuService>();

                // Register services
                services.AddSingleton<Form1>();
                services.AddSingleton<DBConnection>();
                services.AddSingleton<SettingService>();
                services.AddSingleton<FolderPickerService>();
                services.AddSingleton<FileTransferService>();
                services.AddSingleton<SharedService>();



                services.AddSingleton<IndexService>();
                services.AddSingleton<NinusiInfoService>();
                services.AddSingleton<FileTransferHistoryService>();
                services.AddSingleton<FileTransferLogService>();
                services.AddSingleton<ArchiveService>();
                services.AddSingleton<MotherboardIDService>();
                services.AddSingleton<ThereforeLogService>();
                services.AddSingleton<ImportLegacyFilesService>();
                services.AddSingleton<FileRegistryService>();
                services.AddSingleton<DocumentIndexService>();

                
                // Build and return the ServiceProvider
                return services.BuildServiceProvider();
            }
        }
    }
}