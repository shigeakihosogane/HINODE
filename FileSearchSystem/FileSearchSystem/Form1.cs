using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using FileSearchSystem.Components;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using System;
using FileSearchSystem.Services;
using FileSearchSystem.Models;

namespace FileSearchSystem
{
    public partial class Form1 : Form
    {        
        private readonly IServiceProvider _serviceProvider;

        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            blazorWebView1.HostPage = "wwwroot/index.html";
            blazorWebView1.Services = serviceProvider;
            blazorWebView1.RootComponents.Add<Main>("#app");


        }

        public void OpenForm2(List<FileRegistry> fileRegistrys)
        {
            Form2 form2 = new Form2(_serviceProvider, fileRegistrys);
            form2.Show();
        }






        private void ResizeForm(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Console.WriteLine($"Form size changed to: {width}x{height}");
        }


        



    }
}
