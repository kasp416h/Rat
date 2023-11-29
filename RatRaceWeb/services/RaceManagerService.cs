using System;
using Microsoft.AspNetCore.Identity;
using Rat;
using Rat.repository;

namespace RatRaceWeb.services
{
    public class RaceManagerService
    {
        public RaceManager RaceManager { get; }

        public Bookmaker Bookmaker { get; }

        public SaveCenterMongoDB Save { get; }

        public Player? CurrentPlayer { get; private set; }

        public RaceManagerService(RaceManager raceManager, Bookmaker bookmaker, SaveCenterMongoDB save)
        {
            RaceManager = raceManager;
            Bookmaker = bookmaker;
            Save = save;
        }

        public Player? Login(string name, string password)
        {
            foreach (Player player in RaceManager.Players)
            {
                if (name.ToLower().Trim() != player.Name.ToLower() || password.Trim() != player.Password)
                {
                    Console.WriteLine("Enten navn eller password er forkert");
                }
                else
                {
                    Console.WriteLine("Du er nu logget ind");
                    CurrentPlayer = player;
                    return player;
                }
            }

            return null;
        }

        public void LogOut()
        {
            CurrentPlayer = null;
        }
    }
}

