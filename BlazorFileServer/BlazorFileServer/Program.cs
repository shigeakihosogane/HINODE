using BlazorFileServer.Components;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Net;
using Syncfusion.Blazor;
using Radzen;
using BlazorFileServer.Models;
using BlazorFileServer.Services;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY5MTgyMEAzMjM4MmUzMDJlMzBkajhJNXQxZDQ5OStkN3Z0ZDd6dWhaUkxFd2k5RjVKdzhzSG1UbUJBenNnPQ==");

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{    
    options.Listen(IPAddress.Any, 5000, listenOptions =>
    {
        listenOptions.UseHttps("certs/server.pfx", "Hinode8739");
    });
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddControllers();

builder.Services.AddRadzenComponents();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


string sharedFolderPath = @"C:\Users\Hinode24\Documents\Test\Fax受信\FAX一時保管";

app.UseStaticFiles(new StaticFileOptions  //静的ファイルの提供を有効化
{
    FileProvider = new PhysicalFileProvider(sharedFolderPath),
    RequestPath = "/files",
    ContentTypeProvider = new FileExtensionContentTypeProvider()
    {
        Mappings = { [".pdf"] = "application/pdf", [".jpg"] = "image/jpeg" } // 必要な MIME タイプを追加
    }
}); 

app.UseRouting();     //ルーティングを追加
app.MapControllers();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
