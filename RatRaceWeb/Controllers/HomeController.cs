using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RatRaceWeb.services;
using RatRaceWeb.Models;
using RatRaceWeb.Models.ViewModels;

namespace RatRaceWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RaceManagerService _raceManagerService;

    public HomeController(ILogger<HomeController> logger, RaceManagerService raceManagerService)
    {
        _logger = logger;
        _raceManagerService = raceManagerService;
    }

    public IActionResult Index()
    {
        if (_raceManagerService.CurrentPlayer != null)
        {
            return RedirectToAction("Options");
        }
        else
        {
            return View();
        }
    }

    public IActionResult Options()
    {
        if (_raceManagerService.CurrentPlayer != null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        Rat.Player player = _raceManagerService.Login(model.Name, model.Password);

        if (player != null)
        {
            return RedirectToAction("Options");
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

