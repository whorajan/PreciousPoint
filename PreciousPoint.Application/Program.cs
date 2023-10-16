﻿using PreciousPoint.Application.Extensions;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddIdentityServices(builder.Configuration);

    builder.Services.AddControllersWithViews();

    var app = builder.Build();
    app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("https://localhost:7227"));

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();


    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");
    app.Run();
  }
}