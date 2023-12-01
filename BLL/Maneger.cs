using BLL.Repository;
using DLL;
using DLL.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Maneger
    {
        public RaceManager RaceManager;
        public IRepository repo;
        public Maneger()
        {
            repo = new SaveCenter();
            RaceManager = new RaceManager();
        }

        public void Sendsave()
        {
            Bookmaker bookmaker = new Bookmaker();
            repo.SendData(RaceManager, bookmaker.Bets);
        }

        public SaveCenter GetSave()
        {
           SaveCenter data = repo.LoadData();
            if(data == null)
            {
               SaveCenter autodata = new SaveCenter();

                Track Bunker = new Track("Bunker", 20);
                Rat Timmy = new Rat("Timmy", 2, 1);
                Rat Lucas = new Rat("Lucas", 3, 0);
                autodata.Rats.Add(Timmy);
                autodata.Rats.Add(Lucas);
                autodata.Tracks.Add(Bunker);

                return autodata;
            }
            return data;
        }
    }
}
