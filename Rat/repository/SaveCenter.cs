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

        public void CreateSave()
        {
            RaceManager manager = new RaceManager();
            Bookmaker book = new Bookmaker();
            SaveCenter save = new SaveCenter(manager.Rats, manager.Players, manager.Tracks, manager.Races, book.Bets);
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

            Json.SetFilePath("/rats.json");
            List<Rat> rats = save.Rats;
            Json.Write(rats);

            Json.SetFilePath("/players.json");
            List<Player> players = save.Players;
            Json.Write(players);

            Json.SetFilePath("/tracks.json");
            List<Track> tracks = save.Tracks;
            Json.Write(tracks);

            Json.SetFilePath("/races.json");
            List<Race> races = save.Races;
            Json.Write(races);

            Json.SetFilePath("/bets.json");
            List<Bet> bets = save.Bets;
            Json.Write(bets);
        }

    }
}
