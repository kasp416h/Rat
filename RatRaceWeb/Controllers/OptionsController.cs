using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RatRaceWeb.services;
using RatRaceWeb.Models;
using RatRaceWeb.Models.ViewModels;
using Rat;

namespace RatRaceWeb.Controllers
{
    public class OptionsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RaceManagerService _raceManagerService;

        public OptionsController(ILogger<HomeController> logger, RaceManagerService raceManagerService)
        {
            _logger = logger;
            _raceManagerService = raceManagerService;
        }

        public IActionResult CreateRace()
        {
            ViewData["AvailableRats"] = _raceManagerService.RaceManager.Rats;
            ViewData["AvailableTracks"] = _raceManagerService.RaceManager.Tracks;

            return View();
        }

        public IActionResult CreateTrack()
        {
            return View();
        }

        public IActionResult CreateRat()
        {
            return View();
        }

        public IActionResult PickRace()
        {
            ViewData["AvailableRaces"] = _raceManagerService.RaceManager.Races;
            ViewData["PlayerMoney"] = _raceManagerService.CurrentPlayer.Money;

            return View();
        }

        public IActionResult Race(Race selectedRace)
        {
            _raceManagerService.RaceManager.ConductRace(selectedRace);

            string raceReport = _raceManagerService.RaceManager.ViewRaceReport(selectedRace);
            ViewData["RaceReport"] = raceReport;

            Rat.Rat winner = selectedRace.GetWinner();
            ViewData["Winner"] = winner;
            
            _raceManagerService.Bookmaker.PayOutWinnings(winner);

            return View();
        }


        [HttpPost]
        public IActionResult CreateRace(CreateRaceViewModel model)
        {
            int raceID = 1;
            if (_raceManagerService.RaceManager.Races.Count > 0)
            {
                Race lastRace = _raceManagerService.RaceManager.Races.Last();
                raceID = lastRace.RaceID + 1;
            }

            List<string> selectedRatNames = model.SelectedRats;
            List<Rat.Rat> selectedRats = _raceManagerService.RaceManager.Rats.FindAll(rat => selectedRatNames.Contains(rat.Name));

            if (selectedRats.Count < 1)
            {
                selectedRats.Add(_raceManagerService.RaceManager.Rats[0]);
            }

            string selectedTrackName = model.SelectedTrack;
            Track selectedTrack = _raceManagerService.RaceManager.Tracks.FirstOrDefault(track => track.Name == selectedTrackName);

            if (selectedTrack == null)
            {
                selectedTrack = _raceManagerService.RaceManager.Tracks[0];
            }

            Race createdRace = _raceManagerService.RaceManager.CreateRace(raceID, selectedRats, selectedTrack);
            _raceManagerService.RaceManager.Races.Add(createdRace);

            return RedirectToAction("Options", "Home");
        }

        [HttpPost]
        public IActionResult CreateTrack(CreateTrackViewModel model)
        {
            string trackName = model.TrackName;
            int trackLength = model.TrackLength;

            if (trackName.Trim() == "" || trackLength < 1)
            {
                return RedirectToAction("CreateTrack");
            }

            Track createdTrack = _raceManagerService.RaceManager.CreateTrack(trackName, trackLength);
            _raceManagerService.RaceManager.Tracks.Add(createdTrack);

            return RedirectToAction("Options", "Home");
        }

        [HttpPost]
        public IActionResult CreateRat(CreateRatViewModel model)
        {
            string ratName = model.RatName;
            int lower = model.Lower;
            int upper = model.Upper;

            if (ratName.Trim() == "" || lower < 1 || upper < 1 || upper < lower)
            {
                return RedirectToAction("CreateRat");
            }

            Rat.Rat createdRat = _raceManagerService.RaceManager.CreateRat(ratName, upper, lower);
            _raceManagerService.RaceManager.Rats.Add(createdRat);

            return RedirectToAction("Options", "Home");
        }

        [HttpPost]
        public IActionResult SaveGame()
        {
            _raceManagerService.Save.CreateSave(_raceManagerService.RaceManager, _raceManagerService.Bookmaker);
            return RedirectToAction("Options", "Home");
        }

        [HttpPost]
        public IActionResult ExitGame()
        {
            _raceManagerService.LogOut();
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult PlaceBet(PlaceBet placebet)
        {
            int raceId = Int32.Parse(placebet.SelectedRace);
            Race selectedRace = _raceManagerService.RaceManager.Races.FirstOrDefault(race => race.RaceID == raceId);

            string selectedRatName = placebet.SelectedRat;
            Rat.Rat selectedRat = _raceManagerService.RaceManager.Rats.FirstOrDefault(rat => rat.Name == selectedRatName);

            int betAmount = placebet.money;

            if (selectedRace == null || selectedRat == null || betAmount < 1 || betAmount > _raceManagerService.CurrentPlayer.Money)
            {
                return RedirectToAction("PlaceBet");
            }

            Bookmaker bookmaker = _raceManagerService.Bookmaker;

            bookmaker.SetRace(selectedRace);
            Bet bet = bookmaker.PlaceBet(selectedRace, selectedRat, _raceManagerService.CurrentPlayer, betAmount);
            bookmaker.Bets.Add(bet);

            return RedirectToAction("Race", selectedRace);
        }

        [HttpGet]
        public IActionResult GetRatsForRace(int raceId)
        {
            Race selectedRace = _raceManagerService.RaceManager.Races.FirstOrDefault(race => race.RaceID == raceId);
            List<string> ratNames = selectedRace.Rats.Select(rat => rat.Name).ToList();
            return Json(ratNames);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

