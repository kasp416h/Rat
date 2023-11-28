using BLL.Repository;
using DLL;
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

        public void GetSave()
        {
            repo.LoadData();
        }
    }
}
