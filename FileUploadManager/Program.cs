using Azure.Storage.Blobs;
using FileUploadManager.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FileUploadManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddSingleton(options =>
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return config;
            });

            builder.Services.AddSingleton(options =>
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string connectionString = config.GetSection("AzureSettings")["ConnectionString"];
                string storageName = config.GetSection("AzureSettings")["BlobName"];
                BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
                var container = serviceClient.GetBlobContainerClient(storageName);
                container.CreateIfNotExists();

                return container;
            });

            builder.Services.AddSingleton(options =>
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                string connectionString = config.GetSection("AzureSettings")["ConnetionString"];
                string storageName = config.GetSection("AzureSettings")["BlobName"];
                FileUploadService service = new FileUploadService(new BlobContainerClient(connectionString, storageName));

                return service;
            });

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = config.GetSection("AzureSettings")["ConnetionString"];
            string storageName = config.GetSection("AzureSettings")["BlobName"];

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}