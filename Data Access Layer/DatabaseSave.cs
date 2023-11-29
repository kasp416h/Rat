using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Xml.Linq;
using DLL.Modles;
using DLL;

namespace Data_Access_Layer
{
    public class DatabaseSave
    {
        public void SaveRat(string name, int position, int upper, int lower)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                connection.Query("dbo.SetRat @RatName, @Position, @Upper, @Lower",
                new {RatName = name, Position = position, Upper = upper, Lower = lower });
            }
        } 
        
        public void SavePlayer(string name, string password, bool logdin, int money)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                connection.Query("dbo.SetPlayer @PlayerName, @PlayerPassword, @Logdin, @PlayerMoney",
                new { PlayerName = name, PlayerPassword = password, Logdin = logdin, PlayerMoney = money });
            }
        }

        public void SaveTrack(string name, int length)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                connection.Query("dbo.SetTrack @TrackName, @TrackLength",
                new { TrackName = name, TrackLength = length});
            }
        }

        public void SaveBet(int money)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                connection.Query("dbo.SetBet @BetMoney",
                new {BetMoney = money });
            }
        }

        public List<Rat> GetRats()
        {
            List<Rat> rats= new List<Rat>();

            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
            rats = connection.Query<Rat>("dbo.GetAllRats", new {}).ToList();
            }

            return rats;
        }

        public List<Track> GetTracks()
        {
            List<Track> tracks = new List<Track>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                tracks = connection.Query<Track>("dbo.GetAllTracks", new { }).ToList();
            }

            return tracks;
        }

        public List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                players = connection.Query<Player>("dbo.GetAllPlayers", new { }).ToList();
            }

            return players;
        }

        public List<Race> GetRaces()
        {
            List<Race> races = new List<Race>();
            List<int> raceIds = new List<int>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                raceIds = connection.Query<int>("dbo.GetAllRaceId", new { }).ToList();

                foreach(int i in raceIds)
                {
                    Track tempuraltrack = (Track)connection.Query("dbo.GetRaceTrack", new { i });

                    List<Rat> tempuralrats = connection.Query<Rat>("dbo.GetRaceRats", new { i }).ToList();

                    Race newrace = new Race(i, tempuralrats, tempuraltrack);

                    races.Add(newrace);
                }
            }

            return races;
        }

        public List<Bet> GetBets()
        {
            List<Bet> Bets = new List<Bet>();
            List<Race> AllRaces = GetRaces();

            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"))
            {
                List<int> BetIds = connection.Query<int>("dbo.GetAllBetId", new {}).ToList();

                foreach(int i in BetIds)
                {
                    Player tempuralplayer = (Player)connection.Query("dbo.GetBetPlayer", new {i});

                    Rat tempuralrat = (Rat)connection.Query("dbo.GatBetRat", new {i});

                    int BetMoney = Convert.ToInt32(connection.Query("dbo.GetBetMoney", new {i}));

                    int tempuralRaceId = Convert.ToInt32(connection.Query("dbo.GetBetRaceId", new {i}));

                    Race tempuralrace = (Race)AllRaces.Where(r => r.RaceID == tempuralRaceId);

                    Bet newbet = new Bet(BetMoney, tempuralplayer, tempuralrace, tempuralrat);

                    Bets.Add(newbet);
                }
            }

            return Bets;
        }
    }
}
