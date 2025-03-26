using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using FileTransfer2ForBlazor.Components;
using System;


namespace FileTransfer2ForBlazor
{
    public partial class Form1 : Form
    {
        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            blazorWebView1.HostPage = "wwwroot/index.html";
            blazorWebView1.Services = serviceProvider;
            blazorWebView1.RootComponents.Add<MainLayout>("#app");

            //var services = new ServiceCollection();
            //services.AddWindowsFormsBlazorWebView();
            //blazorWebView1.HostPage = "wwwroot/index.html";
            //blazorWebView1.Services = services.BuildServiceProvider();
            //blazorWebView1.RootComponents.Add<MainLayout>("#app");

        }

    }
}
