﻿using Data_Access_Layer;
using DLL;
using DLL.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class SaveCenter : IRepository
    {
        public List<Rat> Rats = new List<Rat>();
        public List<Player> Players = new List<Player>();
        public List<Track> Tracks = new List<Track>();
        public List<Race> Races = new List<Race>();
        public List<Bet> Bets = new List<Bet>();

        public SaveCenter(RaceManager raceManeger, List<Bet> bets)
        {
            Rats = raceManeger.Rats;
            Players = raceManeger.Players;
            Tracks = raceManeger.Tracks;
            Races = raceManeger.Races;
            Bets = bets;
        }

        public SaveCenter()
        {

        }

        public SaveCenter LoadData()
        {

            SaveCenter save = new SaveCenter();

            return save;
        }

        public void SendData(RaceManager raceManager, List<Bet> Bets)
        {
            DatabaseSave DBsave = new DatabaseSave();

            foreach (Rat rat in Rats)
            {
                DBsave.SaveRat(rat.Name, rat.Posistion, rat.Upper, rat.Lower);
            }
            foreach (Player player in Players)
            {
                DBsave.SavePlayer(player.Name, player.Password, player.LoggedIn, player.Money);
            }
            foreach (Track track in Tracks)
            {
                DBsave.SaveTrack(track.Name, track.TrackLength);
            }
            foreach (Bet bet in Bets)
            {
                DBsave.SaveBet(bet.Money);
            }
        }

    }
}
