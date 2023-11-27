using Data_Access_Layer;
using DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class SaveCenter
    {
        public List<Rat> Rats = new List<Rat>();
        public List<Player> Players = new List<Player>();
        public List<Track> Tracks = new List<Track>();
        public List<Race> Races = new List<Race>();
        public List<Bet> Bets = new List<Bet>();

        public SaveCenter(List<Rat> rats, List<Player> players, List<Track> tracks, List<Race> races, List<Bet> bets)
        {
            Rats = rats;
            Players = players;
            Tracks = tracks;
            Races = races;
            Bets = bets;
        }

        public SaveCenter()
        {

        }

        public void CreateSave(RaceManager raceManager, Bookmaker bookmaker)
        {
            SaveCenter save = new SaveCenter(raceManager.Rats, raceManager.Players, raceManager.Tracks, raceManager.Races, bookmaker.Bets);
            SendSave(save);
        }
        public SaveCenter LoadData()
        {

            SaveCenter save = new SaveCenter();

            return save;
        }

        public void SendSave(SaveCenter save)
        {
            DatabaseSave DBsave = new DatabaseSave();

            foreach (Rat rat in save.Rats)
            {
                DBsave.SaveRat(rat.Name, rat.Posistion, rat.Upper, rat.Lower);
            }
            foreach (Player player in save.Players)
            {
                DBsave.SavePlayer(player.Name, player.Password, player.LoggedIn, player.Money);
            }
            foreach (Track track in save.Tracks)
            {
                DBsave.SaveTrack(track.Name, track.TrackLength);
            }
            foreach (Bet bet in save.Bets)
            {
                DBsave.SaveBet(bet.Money);
            }
        }

    }
}
