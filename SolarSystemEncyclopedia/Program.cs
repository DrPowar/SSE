using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SolarSystemEncyclopedia.Data;
using SolarSystemEncyclopedia.Models;
using System.Globalization;

namespace SolarSystemEncyclopedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SolarSystemContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("SolarSystemContext"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SolarSystemContext"))));

            //Elasticsearch config
            var cloudId = builder.Configuration["Elasticsearch:CloudId"];
            var apiKey = builder.Configuration["Elasticsearch:ApiKey"];

            if (string.IsNullOrWhiteSpace(cloudId) || string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("CloudId and ApiKey missed or incorrect.");
            }

            // Init Elasticsearch client
            var elasticClient = new ElasticsearchClient(cloudId, new ApiKey(apiKey));

            // Register client as singleton-service
            builder.Services.AddSingleton(elasticClient);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Encyclopedia}/{action=Index}/{id?}");

            app.Run();
        }

    }
}
