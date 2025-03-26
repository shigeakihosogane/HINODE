using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using FileBrowser.Components;
using System.Diagnostics.Metrics;
using System;

namespace FileBrowser
{
    public partial class Form1 : Form
    {
        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            blazorWebView1.HostPage = "wwwroot/index.html";
            blazorWebView1.Services = serviceProvider;
            blazorWebView1.RootComponents.Add<Main>("#app");

        }
    }
}
