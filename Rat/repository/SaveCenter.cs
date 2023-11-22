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
            JsonHandler Json = new JsonHandler();
            string basePath = "/Users/kasperhog/Projects/Rat/Data Access Layer";

            Json.SetFilePath(basePath + "/rats.json");
            List<Rat> rats = Json.Read<Rat>();

            Json.SetFilePath(basePath + "/players.json");
            List<Player> players = Json.Read<Player>();

            Json.SetFilePath(basePath + "/tracks.json");
            List<Track> tracks = Json.Read<Track>();

            Json.SetFilePath(basePath + "/races.json");
            List<Race> races = Json.Read<Race>();

            Json.SetFilePath(basePath + "/bets.json");
            List<Bet> bets = Json.Read<Bet>();

            SaveCenter save = new SaveCenter(rats, players, tracks, races, bets);

            return save;
        }

        public void SendSave(SaveCenter save)
        {
            JsonHandler Json = new JsonHandler();
            string basePath = "/Users/kasperhog/Projects/Rat/Data Access Layer";

            Json.SetFilePath(basePath + "/rats.json");
            List<Rat> rats = save.Rats;
            Json.Write(rats);

            Json.SetFilePath(basePath + "/players.json");
            List<Player> players = save.Players;
            Json.Write(players);

            Json.SetFilePath(basePath + "/tracks.json");
            List<Track> tracks = save.Tracks;
            Json.Write(tracks);

            Json.SetFilePath(basePath + "/races.json");
            List<Race> races = save.Races;
            Json.Write(races);

            Json.SetFilePath(basePath + "/bets.json");
            List<Bet> bets = save.Bets;
            Json.Write(bets);
        }

    }
}
