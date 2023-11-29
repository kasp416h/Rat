using Rat;
using Rat.repository;
using RatRaceWeb.services;

namespace RatRaceWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        SaveCenterMongoDB save = new SaveCenterMongoDB("RatRace", "mongodb+srv://kasp416h:xhy72pvvKM@cluster0.mrfn8h3.mongodb.net/?retryWrites=true&w=majority");
        SaveCenter saveData = save.LoadData();

        RaceManager raceManager = new RaceManager
        (
            saveData.Tracks,
            saveData.Players,
            saveData.Races,
            saveData.Rats
        );
        Bookmaker bookmaker = new Bookmaker();

        builder.Services.AddSingleton(new RaceManagerService(raceManager, bookmaker, save));
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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "login",
            pattern: "{controller=Home}/{action=Login}/{id?}");

        app.Run();
    }
}

