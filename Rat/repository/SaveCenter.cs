using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Data_Access_Layer;

namespace Rat.repository
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

        public void CreateSave()
        {
            RaceManager manager = new RaceManager();
            Bookmaker book = new Bookmaker();
            SaveCenter save = new SaveCenter(manager.Rats, manager.Players, manager.Tracks, manager.Races, book.Bets);
            SendSave(save);
        }

        public void SendSave(object save)
        {
            JsonHandler Json = new JsonHandler();
            Json.Write(save);
        }
    }
}
