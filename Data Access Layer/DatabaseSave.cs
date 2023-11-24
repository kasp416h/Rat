using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Xml.Linq;

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
                new { });
            }
        }
    }
}
